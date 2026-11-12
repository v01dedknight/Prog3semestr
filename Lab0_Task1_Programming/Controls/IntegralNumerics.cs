using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lab0_Task1_Programming.Controls
{
    // Numerical integration: left/right/midpoint rectangles, trapezoids and Simpson's rule.
    internal enum IntegrationMethod
    {
        LeftRectangles,
        RightRectangles,
        MidpointRectangles,
        Trapezoids,
        Simpson
    }

    internal static class IntegrationMethodInfo
    {
        public static string Name(IntegrationMethod method) => method switch
        {
            IntegrationMethod.LeftRectangles => "Левые прямоугольники",
            IntegrationMethod.RightRectangles => "Правые прямоугольники",
            IntegrationMethod.MidpointRectangles => "Средние прямоугольники",
            IntegrationMethod.Trapezoids => "Трапеции",
            IntegrationMethod.Simpson => "Симпсон (параболы)",
            _ => "Неизвестный метод"
        };

        // Order of the principal truncation error for sufficiently smooth functions.
        public static int Order(IntegrationMethod method) => method switch
        {
            IntegrationMethod.LeftRectangles or IntegrationMethod.RightRectangles => 1,
            IntegrationMethod.MidpointRectangles or IntegrationMethod.Trapezoids => 2,
            IntegrationMethod.Simpson => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(method))
        };
    }

    internal sealed class IntegrationStep
    {
        public int Number { get; }
        public int Partitions { get; }
        public double Value { get; }
        public double? ErrorEstimate { get; }

        public IntegrationStep(int number, int partitions, double value, double? errorEstimate)
        {
            Number = number;
            Partitions = partitions;
            Value = value;
            ErrorEstimate = errorEstimate;
        }
    }

    internal sealed class IntegrationRun
    {
        public IntegrationMethod Method { get; }
        public List<IntegrationStep> Steps { get; } = new();
        public bool Converged { get; set; }
        public string Message { get; set; } = "";
        public IntegrationStep? LastStep => Steps.Count == 0 ? null : Steps[Steps.Count - 1];

        public IntegrationRun(IntegrationMethod method) => Method = method;
    }

    internal static class IntegralNumerics
    {
        public const int MaximumPartitions = 262144;
        public const int MaximumRefinements = 24;

        // Finds a practical n on the sequence n, 2n, 4n, ... using Runge's estimate.
        // The estimate is not a mathematical guarantee for discontinuous or singular f(x).
        public static IntegrationRun Refine(
            Func<double, double> function,
            double a,
            double b,
            int initialN,
            double epsilon,
            IntegrationMethod method)
        {
            if (function == null) throw new ArgumentNullException(nameof(function));
            if (!double.IsFinite(a) || !double.IsFinite(b) || !double.IsFinite(b - a))
                throw new ArgumentException("Границы и длина интервала должны быть конечными числами.");
            if (!double.IsFinite(epsilon) || epsilon <= 0)
                throw new ArgumentException("Точность e должна быть положительным конечным числом.");
            if (initialN < 1 || initialN > MaximumPartitions)
                throw new ArgumentException($"Число разбиений n должно быть от 1 до {MaximumPartitions}.");

            var result = new IntegrationRun(method);
            int n = method == IntegrationMethod.Simpson && initialN % 2 != 0
                ? initialN + 1
                : initialN;

            if (n > MaximumPartitions)
            {
                result.Message = "Для метода Симпсона требуется чётное n, но лимит разбиений превышен.";
                return result;
            }

            if (a == b)
            {
                result.Steps.Add(new IntegrationStep(0, n, 0.0, 0.0));
                result.Converged = true;
                result.Message = "Границы совпадают: интеграл равен нулю.";
                return result;
            }

            try
            {
                double previous = Integrate(function, a, b, n, method);
                result.Steps.Add(new IntegrationStep(0, n, previous, null));

                int p = IntegrationMethodInfo.Order(method);
                double denominator = Math.Pow(2.0, p) - 1.0;

                for (int step = 1; step <= MaximumRefinements; step++)
                {
                    if (n > MaximumPartitions / 2)
                    {
                        result.Message = $"Точность e не подтверждена: достигнут предел {MaximumPartitions} разбиений.";
                        return result;
                    }

                    n *= 2;
                    double current = Integrate(function, a, b, n, method);
                    double estimatedError = Math.Abs(current - previous) / denominator;
                    if (!double.IsFinite(estimatedError))
                        throw new ArithmeticException("Не удалось корректно вычислить оценку погрешности.");

                    result.Steps.Add(new IntegrationStep(step, n, current, estimatedError));

                    if (estimatedError <= epsilon)
                    {
                        result.Converged = true;
                        result.Message = "Подобрано n: оценка Рунге не превышает e.";
                        return result;
                    }
                    previous = current;
                }

                result.Message = "Лимит уточнений достигнут: необходимая точность не подтверждена.";
            }
            catch (ArithmeticException ex)
            {
                result.Message = "Расчёт прерван: " + ex.Message;
            }
            catch (ArgumentException ex)
            {
                result.Message = "Расчёт прерван: " + ex.Message;
            }

            return result;
        }

        // Composite quadrature. Kahan summation reduces the error of accumulated additions.
        public static double Integrate(
            Func<double, double> function,
            double a,
            double b,
            int n,
            IntegrationMethod method)
        {
            if (function == null) throw new ArgumentNullException(nameof(function));
            if (n < 1 || n > MaximumPartitions) throw new ArgumentOutOfRangeException(nameof(n));
            if (method == IntegrationMethod.Simpson && (n & 1) == 1)
                throw new ArgumentException("Для метода Симпсона число разбиений n должно быть чётным.");

            if (a == b) return 0.0;
            double h = (b - a) / n;
            if (!double.IsFinite(h) || h == 0)
                throw new ArithmeticException("Шаг разбиения невозможно представить числом double.");

            double sum = 0;
            double correction = 0;
            void Add(double value)
            {
                double y = value - correction;
                double t = sum + y;
                correction = (t - sum) - y;
                sum = t;
            }

            double F(double x)
            {
                double y = function(x);
                if (!double.IsFinite(y))
                    throw new ArithmeticException("Функция не определена или бесконечна в одной из точек разбиения. Измените границы либо метод.");
                return y;
            }

            switch (method)
            {
                case IntegrationMethod.LeftRectangles:
                    for (int i = 0; i < n; i++) Add(F(a + h * i));
                    break;
                case IntegrationMethod.RightRectangles:
                    for (int i = 1; i <= n; i++) Add(F(i == n ? b : a + h * i));
                    break;
                case IntegrationMethod.MidpointRectangles:
                    for (int i = 0; i < n; i++) Add(F(a + h * (i + 0.5)));
                    break;
                case IntegrationMethod.Trapezoids:
                    Add(0.5 * F(a));
                    for (int i = 1; i < n; i++) Add(F(a + h * i));
                    Add(0.5 * F(b));
                    break;
                case IntegrationMethod.Simpson:
                    Add(F(a));
                    for (int i = 1; i < n; i++) Add(((i & 1) == 1 ? 4.0 : 2.0) * F(a + h * i));
                    Add(F(b));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method));
            }

            double scale = method == IntegrationMethod.Trapezoids ? h
                : method == IntegrationMethod.Simpson ? h / 3.0 : h;
            double answer = scale * sum;
            if (!double.IsFinite(answer))
                throw new ArithmeticException("Результат интегрирования вышел за диапазон double.");
            return answer;
        }

        public static string Format(double value, int digits = 12) =>
            value.ToString("G" + digits.ToString(CultureInfo.InvariantCulture), CultureInfo.GetCultureInfo("ru-RU"));
    }
}
