using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab0_Task1_Programming.Controls
{
    /// <summary>
    /// Поиск стационарных точек: f'(x)=0 методом Ньютона, с защитой границ
    /// и численной оценкой производных. Золотое сечение не используется.
    /// </summary>
    internal static class NewtonExtremumSolver
    {
        internal sealed record Iteration(int Number, double X, double Y, double First,
            double Second, double Left, double Right, string Action);

        internal sealed record Result(double X, double Y, bool IsMinimum,
            IReadOnlyList<Iteration> History, double Left, double Right,
            string Note, int CandidatesFound);

        private const int ScanCount = 512;
        private const int MaxIterations = 120;

        private static double Eval(Func<double, double> function, double x)
        {
            double value = function(x);
            if (!double.IsFinite(value))
                throw new ArithmeticException($"Функция не определена при x = {x:G12}.");
            return value;
        }

        // Пятиточечные центральные разности, адаптивный шаг.
        // h достаточно велик, чтобы ограничить погрешность округления во второй производной.
        private static (double First, double Second) Derivatives(Func<double, double> f,
            double x, double left, double right)
        {
            double h = Math.Max(1e-5, 1e-4 * Math.Max(1.0, Math.Abs(x)));
            h = Math.Min(h, Math.Max(1e-12, (right - left) / 2000.0));
            if (h <= 0 || !double.IsFinite(h))
                throw new ArithmeticException("Невозможно выбрать шаг численного дифференцирования.");

            // Центральная схема допускает вычисления чуть вне пользовательского интервала;
            // вблизи ограниченной области определения корректно прерываем расчёт.
            double fm2 = Eval(f, x - 2 * h);
            double fm1 = Eval(f, x - h);
            double f0 = Eval(f, x);
            double fp1 = Eval(f, x + h);
            double fp2 = Eval(f, x + 2 * h);
            double d1 = (fm2 - 8 * fm1 + 8 * fp1 - fp2) / (12 * h);
            double d2 = (-fm2 + 16 * fm1 - 30 * f0 + 16 * fp1 - fp2) / (12 * h * h);
            if (!double.IsFinite(d1) || !double.IsFinite(d2))
                throw new ArithmeticException("Невозможно надёжно вычислить производные.");
            return (d1, d2);
        }

        private sealed record Bracket(double Left, double Right, bool Minimum);

        private static List<Bracket> FindBrackets(Func<double, double> f, double a, double b)
        {
            var candidates = new List<Bracket>();
            double step = (b - a) / ScanCount;
            double? oldDerivative = null;
            double oldX = a;
            for (int i = 0; i <= ScanCount; i++)
            {
                double x = a + (b - a) * i / ScanCount;
                try
                {
                    double d = Derivatives(f, x, a, b).First;
                    if (oldDerivative.HasValue)
                    {
                        bool minimum = oldDerivative.Value < 0 && d >= 0;
                        bool maximum = oldDerivative.Value > 0 && d <= 0;
                        if (minimum || maximum)
                        {
                            // Защита от повторного обнаружения одной точки при шуме.
                            if (candidates.Count == 0 ||
                                x - candidates[^1].Right > step * .45 ||
                                candidates[^1].Minimum != minimum)
                                candidates.Add(new Bracket(oldX, x, minimum));
                        }
                    }
                    oldDerivative = d;
                }
                catch (ArithmeticException)
                {
                    // OverflowException наследуется от ArithmeticException.
                    // Один обработчик охватывает обе ситуации.
                    oldDerivative = null;
                }
                oldX = x;
            }
            return candidates;
        }

        private static Result? SolveBracket(Func<double, double> f, double a, double b,
            double e, Bracket bracket, int count, double? initial)
        {
            double left = bracket.Left;
            double right = bracket.Right;
            double x = initial.HasValue && initial.Value > left && initial.Value < right
                ? initial.Value : (left + right) / 2;
            var history = new List<Iteration>();
            bool min = bracket.Minimum;

            for (int iteration = 0; iteration < MaxIterations; iteration++)
            {
                (double d1, double d2) = Derivatives(f, x, a, b);
                double y = Eval(f, x);
                double next = double.NaN;
                string action = "Шаг Ньютона";
                if (Math.Abs(d2) > 1e-12)
                    next = x - d1 / d2;
                double borderAllowance = Math.Max(1e-11 * (1 + Math.Abs(x)), e * 1e-4);
                if (!double.IsFinite(next) || next < left - borderAllowance || next > right + borderAllowance ||
                    Math.Abs(next - x) > (right - left) * 0.9)
                {
                    next = (left + right) / 2;
                    action = "Половинный защитный шаг";
                }
                else
                {
                    // Численное дифференцирование может дать x* на 1e-12 за границей.
                    next = Math.Clamp(next, left, right);
                }
                history.Add(new Iteration(iteration, x, y, d1, d2, left, right, action));

                double derivativeAtNext = Derivatives(f, next, a, b).First;
                if (min)
                {
                    if (derivativeAtNext < 0) left = next;
                    else right = next;
                }
                else
                {
                    if (derivativeAtNext > 0) left = next;
                    else right = next;
                }

                if (right - left <= 2 * e || (Math.Abs(next - x) <= e && Math.Abs(derivativeAtNext) < 1e-7))
                {
                    x = next;
                    break;
                }
                // Гарантия уменьшения диапазона даже если метод Ньютона неподвижен.
                x = next;
            }

            // Самая близкая к стационарной точка среди текущей и середины оставшегося диапазона.
            double midpoint = (left + right) / 2;
            double midSlope = Math.Abs(Derivatives(f, midpoint, a, b).First);
            double finalSlope = Math.Abs(Derivatives(f, x, a, b).First);
            if (midSlope < finalSlope) x = midpoint;

            (double slope, double curvature) = Derivatives(f, x, a, b);
            double yFinal = Eval(f, x);
            // Тип экстремума определяется сменой знака f', а не только численным f''.
            history.Add(new Iteration(history.Count, x, yFinal, slope, curvature, left, right, "Найденная точка"));
            return new Result(x, yFinal, min, history, left, right,
                history.Count >= MaxIterations ? "Достигнут лимит шагов; точность может быть недостаточной." :
                "Стационарная точка подтверждена сменой знака первой производной.", count);
        }

        /// <summary>
        /// Ищет внутренние экстремумы, а не минимальное значение на границах отрезка.
        /// Если есть несколько локальных экстремумов, выбирает наилучший для режима.
        /// При заданном начальном приближении x0 выбирает ближайший экстремум.
        /// </summary>
        internal static Result? Find(Func<double, double> f, double a, double b, double e,
            bool minimum, double? x0, out string diagnostics)
        {
            if (!(a < b) || e <= 0 || !double.IsFinite(a) || !double.IsFinite(b) || !double.IsFinite(e))
                throw new ArgumentException("Проверьте a, b, e: требуется a < b, e > 0.");
            var brackets = FindBrackets(f, a, b).Where(p => p.Minimum == minimum).ToList();
            if (brackets.Count == 0)
            {
                diagnostics = "Внутренний " + (minimum ? "минимум" : "максимум") +
                    " не обнаружен. Измените интервал, проверьте непрерывность и начальные данные.";
                return null;
            }
            var solutions = new List<Result>();
            foreach (Bracket bracket in brackets)
            {
                try
                {
                    Result? result = SolveBracket(f, a, b, e, bracket, brackets.Count, x0);
                    if (result != null && double.IsFinite(result.X) && double.IsFinite(result.Y))
                        if (!solutions.Any(s => Math.Abs(s.X - result.X) < Math.Max(e * 2, (b - a) / ScanCount * 0.3)))
                            solutions.Add(result);
                }
                catch (ArithmeticException) { /* В том числе переполнение: пропускаем диапазон. */ }
            }
            if (solutions.Count == 0)
            {
                diagnostics = "Численный метод не смог подтвердить экстремум. Попробуйте уменьшить интервал.";
                return null;
            }
            Result selected = x0.HasValue
                ? solutions.OrderBy(p => Math.Abs(p.X - x0.Value)).First()
                : (minimum ? solutions.OrderBy(p => p.Y).First() : solutions.OrderByDescending(p => p.Y).First());
            diagnostics = solutions.Count > 1
                ? $"Обнаружено {solutions.Count} { (minimum ? "локальных минимумов" : "локальных максимумов")}. " +
                  (x0.HasValue ? "Выбран ближайший к x0. " : "Выбран лучший по значению f(x). ") +
                  "Чтобы изолировать другой экстремум, сузьте интервал или укажите x0."
                : "Обнаружен один внутренний экстремум выбранного типа.";
            return selected;
        }
    }
}
