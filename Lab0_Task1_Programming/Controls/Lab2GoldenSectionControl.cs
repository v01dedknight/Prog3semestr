using System.Globalization;
using System.Text;

namespace Lab0_Task1_Programming.Controls
{
    public partial class Lab2GoldenSectionControl : UserControl
    {
        private const double GoldenRatio = 0.6180339887498948482;
        private const int MaxIterations = 1_000_000;
        private const int MaxDisplayedIterations = 500;

        private double? _lastMinimumX;
        private double? _lastMaximumX;
        private string _lastMinimumLabel = string.Empty;
        private string _lastMaximumLabel = string.Empty;
        private string _lastFormula = string.Empty;
        private double _lastA;
        private double _lastB;

        public Lab2GoldenSectionControl()
        {
            InitializeComponent();
            modeComboBox.SelectedIndex = 0;
        }

        private void CalculateMenuItem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryReadInputs(out string formula, out double a, out double b, out double epsilon))
                {
                    return;
                }

                var expression = new ExpressionParser(formula);
                Func<double, double> function = expression.Evaluate;

                string diagnostic = AnalyzeFunction(function, a, b);
                var log = new StringBuilder();

                _lastMinimumX = null;
                _lastMaximumX = null;
                _lastMinimumLabel = string.Empty;
                _lastMaximumLabel = string.Empty;

                switch (modeComboBox.SelectedIndex)
                {
                    case 1:
                        CalculateMaximum(function, formula, a, b, epsilon, diagnostic, log);
                        break;
                    case 2:
                        CalculateBoth(function, formula, a, b, epsilon, diagnostic, log);
                        break;
                    default:
                        CalculateMinimum(function, formula, a, b, epsilon, diagnostic, log);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Ошибка расчёта",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BuildGraphMenuItem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!TryReadInputs(out string formula, out double a, out double b, out _))
                {
                    return;
                }

                var expression = new ExpressionParser(formula);
                Func<double, double> function = expression.Evaluate;

                bool sameInput =
                    string.Equals(_lastFormula, formula, StringComparison.Ordinal) &&
                    NearlyEqual(_lastA, a) &&
                    NearlyEqual(_lastB, b);

                goldenSectionPlotPanel.SetPlot(
                    function,
                    a,
                    b,
                    sameInput ? _lastMinimumX : null,
                    sameInput ? _lastMinimumLabel : null,
                    sameInput ? _lastMaximumX : null,
                    sameInput ? _lastMaximumLabel : null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Не удалось построить график: " + ex.Message,
                    "График",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearMenuItem_Click(object? sender, EventArgs e)
        {
            answerLabel.Text = "Ответ появится после расчёта.";
            iterationsLabel.Text = "Итераций: —";
            warningLabel.ForeColor = SystemColors.GrayText;
            warningLabel.Text = "Метод золотого сечения рассчитан на один искомый экстремум на выбранном интервале.";
            calculationsTextBox.Clear();
            goldenSectionPlotPanel.ClearPlot();

            _lastMinimumX = null;
            _lastMaximumX = null;
            _lastMinimumLabel = string.Empty;
            _lastMaximumLabel = string.Empty;
            _lastFormula = string.Empty;
        }

        private bool TryReadInputs(out string formula, out double a, out double b, out double epsilon)
        {
            formula = functionTextBox.Text.Trim();
            a = 0;
            b = 0;
            epsilon = 0;

            if (string.IsNullOrWhiteSpace(formula))
            {
                ShowInputError("Введите функцию f(x).", functionTextBox);
                return false;
            }

            if (!TryParseNumber(aTextBox.Text, out a))
            {
                ShowInputError("Параметр a должен быть вещественным числом.", aTextBox);
                return false;
            }

            if (!TryParseNumber(bTextBox.Text, out b))
            {
                ShowInputError("Параметр b должен быть вещественным числом.", bTextBox);
                return false;
            }

            if (!TryParseNumber(epsilonTextBox.Text, out epsilon))
            {
                ShowInputError("Погрешность e должна быть вещественным числом.", epsilonTextBox);
                return false;
            }

            if (!double.IsFinite(a) || !double.IsFinite(b) || !double.IsFinite(epsilon))
            {
                MessageBox.Show(this, "Параметры a, b и e должны быть конечными числами.", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (a >= b)
            {
                MessageBox.Show(this, "Левая граница a должна быть меньше правой границы b.", "Ошибка интервала",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (epsilon <= 0)
            {
                MessageBox.Show(this, "Погрешность e должна быть больше нуля.", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (epsilon >= b - a)
            {
                MessageBox.Show(this, "Погрешность e должна быть меньше длины интервала b - a.", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void CalculateMinimum(
            Func<double, double> function,
            string formula,
            double a,
            double b,
            double epsilon,
            string diagnostic,
            StringBuilder log)
        {
            AppendHeader(log, "Поиск минимума методом золотого сечения", formula, a, b, epsilon);
            SearchResult result = SearchExtremum(function, a, b, epsilon, findMaximum: false, log);

            answerLabel.Text = $"Минимум: x ≈ {Format(result.X)}, f(x) ≈ {Format(result.Y)}";
            iterationsLabel.Text = $"Итераций: {result.Iterations}; конечный интервал: [{Format(result.Left)}; {Format(result.Right)}]";
            calculationsTextBox.Text = log.ToString();
            SetDiagnostic(diagnostic);

            _lastMinimumX = result.X;
            _lastMinimumLabel = $"min: x={Format(result.X)}, f={Format(result.Y)}";
            SaveInput(formula, a, b);
            goldenSectionPlotPanel.SetPlot(function, a, b, result.X, _lastMinimumLabel);
        }

        private void CalculateMaximum(
            Func<double, double> function,
            string formula,
            double a,
            double b,
            double epsilon,
            string diagnostic,
            StringBuilder log)
        {
            AppendHeader(log, "Поиск максимума методом золотого сечения", formula, a, b, epsilon);
            SearchResult result = SearchExtremum(function, a, b, epsilon, findMaximum: true, log);

            answerLabel.Text = $"Максимум: x ≈ {Format(result.X)}, f(x) ≈ {Format(result.Y)}";
            iterationsLabel.Text = $"Итераций: {result.Iterations}; конечный интервал: [{Format(result.Left)}; {Format(result.Right)}]";
            calculationsTextBox.Text = log.ToString();
            SetDiagnostic(diagnostic);

            _lastMaximumX = result.X;
            _lastMaximumLabel = $"max: x={Format(result.X)}, f={Format(result.Y)}";
            SaveInput(formula, a, b);
            goldenSectionPlotPanel.SetPlot(function, a, b, null, null, result.X, _lastMaximumLabel);
        }

        private void CalculateBoth(
            Func<double, double> function,
            string formula,
            double a,
            double b,
            double epsilon,
            string diagnostic,
            StringBuilder log)
        {
            AppendHeader(log, "Поиск минимума и максимума методом золотого сечения", formula, a, b, epsilon);

            log.AppendLine("--- Минимум ---");
            SearchResult minimum = SearchExtremum(function, a, b, epsilon, findMaximum: false, log);
            log.AppendLine();
            log.AppendLine("--- Максимум ---");
            SearchResult maximum = SearchExtremum(function, a, b, epsilon, findMaximum: true, log);

            answerLabel.Text =
                $"min: x ≈ {Format(minimum.X)}, f ≈ {Format(minimum.Y)}    |    " +
                $"max: x ≈ {Format(maximum.X)}, f ≈ {Format(maximum.Y)}";
            iterationsLabel.Text = $"Итераций: минимум — {minimum.Iterations}; максимум — {maximum.Iterations}";
            calculationsTextBox.Text = log.ToString();
            SetDiagnostic(diagnostic);

            _lastMinimumX = minimum.X;
            _lastMaximumX = maximum.X;
            _lastMinimumLabel = $"min: x={Format(minimum.X)}, f={Format(minimum.Y)}";
            _lastMaximumLabel = $"max: x={Format(maximum.X)}, f={Format(maximum.Y)}";
            SaveInput(formula, a, b);

            goldenSectionPlotPanel.SetPlot(
                function,
                a,
                b,
                minimum.X,
                _lastMinimumLabel,
                maximum.X,
                _lastMaximumLabel);
        }

        private static SearchResult SearchExtremum(
            Func<double, double> function,
            double a,
            double b,
            double epsilon,
            bool findMaximum,
            StringBuilder log)
        {
            double left = a;
            double right = b;

            double x1 = right - GoldenRatio * (right - left);
            double x2 = left + GoldenRatio * (right - left);
            double f1 = EvaluateFinite(function, x1);
            double f2 = EvaluateFinite(function, x2);

            int iterations = 0;

            while (right - left > epsilon)
            {
                if (++iterations > MaxIterations)
                {
                    throw new InvalidOperationException(
                        "Превышено допустимое число итераций. Проверьте функцию и значение погрешности e.");
                }

                if (iterations <= MaxDisplayedIterations)
                {
                    log.AppendLine(
                        $"{iterations,3}: [{Format(left)}; {Format(right)}]  " +
                        $"x1={Format(x1)}, f1={Format(f1)}; x2={Format(x2)}, f2={Format(f2)}");
                }

                bool keepLeftPart = findMaximum ? f1 >= f2 : f1 <= f2;

                if (keepLeftPart)
                {
                    right = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = right - GoldenRatio * (right - left);
                    f1 = EvaluateFinite(function, x1);
                }
                else
                {
                    left = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = left + GoldenRatio * (right - left);
                    f2 = EvaluateFinite(function, x2);
                }
            }

            if (iterations > MaxDisplayedIterations)
            {
                log.AppendLine($"... показаны первые {MaxDisplayedIterations} итераций из {iterations}.");
            }

            double candidateX = (left + right) / 2.0;
            double candidateY = EvaluateFinite(function, candidateX);

            // Учитываем экстремум на границе исходного отрезка.
            double aY = EvaluateFinite(function, a);
            double bY = EvaluateFinite(function, b);

            (double X, double Y) best = (candidateX, candidateY);
            if (IsBetter(aY, best.Y, findMaximum))
            {
                best = (a, aY);
            }
            if (IsBetter(bY, best.Y, findMaximum))
            {
                best = (b, bY);
            }

            return new SearchResult(best.X, best.Y, iterations, left, right);
        }

        private static bool IsBetter(double candidate, double current, bool findMaximum)
        {
            return findMaximum ? candidate > current : candidate < current;
        }

        private static void AppendHeader(
            StringBuilder log,
            string title,
            string formula,
            double a,
            double b,
            double epsilon)
        {
            log.AppendLine(title);
            log.AppendLine($"f(x) = {formula}");
            log.AppendLine($"Начальный интервал: [{Format(a)}; {Format(b)}]");
            log.AppendLine($"e = {Format(epsilon)}");
            log.AppendLine($"φ = (√5 - 1) / 2 ≈ {Format(GoldenRatio)}");
            log.AppendLine();
        }

        private static string AnalyzeFunction(Func<double, double> function, double a, double b)
        {
            const int samples = 400;
            int invalidPoints = 0;
            int localMinima = 0;
            int localMaxima = 0;
            double? yBeforePrevious = null;
            double? yPrevious = null;

            for (int i = 0; i <= samples; i++)
            {
                double x = a + (b - a) * i / samples;
                try
                {
                    double y = function(x);
                    if (!double.IsFinite(y) || Math.Abs(y) > 1e12)
                    {
                        invalidPoints++;
                        yBeforePrevious = null;
                        yPrevious = null;
                        continue;
                    }

                    if (yBeforePrevious.HasValue && yPrevious.HasValue)
                    {
                        if (yPrevious.Value < yBeforePrevious.Value && yPrevious.Value < y)
                        {
                            localMinima++;
                        }
                        if (yPrevious.Value > yBeforePrevious.Value && yPrevious.Value > y)
                        {
                            localMaxima++;
                        }
                    }

                    yBeforePrevious = yPrevious;
                    yPrevious = y;
                }
                catch
                {
                    invalidPoints++;
                    yBeforePrevious = null;
                    yPrevious = null;
                }
            }

            var messages = new List<string>();
            if (invalidPoints > 0)
            {
                messages.Add("На интервале есть неопределённые значения или разрыв функции. Измените интервал.");
            }
            if (localMinima > 1 || localMaxima > 1)
            {
                messages.Add(
                    $"Обнаружено несколько возможных экстремумов (min: {localMinima}, max: {localMaxima}). " +
                    "Золотое сечение надёжнее применять на интервале с одним искомым экстремумом.");
            }

            return string.Join(" ", messages);
        }

        private void SetDiagnostic(string diagnostic)
        {
            if (string.IsNullOrWhiteSpace(diagnostic))
            {
                warningLabel.ForeColor = SystemColors.GrayText;
                warningLabel.Text = "Расчёт выполнен для выбранного интервала. Для одного экстремума метод золотого сечения последовательно сужает [a, b].";
            }
            else
            {
                warningLabel.ForeColor = Color.DarkOrange;
                warningLabel.Text = diagnostic;
            }
        }

        private static double EvaluateFinite(Func<double, double> function, double x)
        {
            double y;
            try
            {
                y = function(x);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Функция не определена в точке x = {Format(x)}. Измените интервал. ({ex.Message})");
            }

            if (!double.IsFinite(y))
            {
                throw new InvalidOperationException(
                    $"Функция не имеет конечного значения в точке x = {Format(x)}. Измените интервал.");
            }

            return y;
        }

        private void SaveInput(string formula, double a, double b)
        {
            _lastFormula = formula;
            _lastA = a;
            _lastB = b;
        }

        private static bool TryParseNumber(string text, out double value)
        {
            text = text.Trim();

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value))
            {
                return true;
            }

            return double.TryParse(
                text.Replace(',', '.'),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out value);
        }

        private static string Format(double value)
        {
            return value.ToString("G10", CultureInfo.CurrentCulture);
        }

        private static bool NearlyEqual(double a, double b)
        {
            return Math.Abs(a - b) <= Math.Max(1e-12, Math.Max(Math.Abs(a), Math.Abs(b)) * 1e-12);
        }

        private void ShowInputError(string message, Control control)
        {
            MessageBox.Show(this, message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control.Focus();
            if (control is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        private sealed class SearchResult
        {
            public SearchResult(double x, double y, int iterations, double left, double right)
            {
                X = x;
                Y = y;
                Iterations = iterations;
                Left = left;
                Right = right;
            }

            public double X { get; }
            public double Y { get; }
            public int Iterations { get; }
            public double Left { get; }
            public double Right { get; }
        }

        private sealed class ExpressionParser
        {
            private readonly string _expression;
            private int _position;
            private double _x;

            public ExpressionParser(string expression)
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    throw new ArgumentException("Формула функции не задана.");
                }

                string normalized = expression.Trim();
                int equalsIndex = normalized.IndexOf('=');
                if (equalsIndex >= 0)
                {
                    normalized = normalized[(equalsIndex + 1)..];
                }

                normalized = normalized
                    .Replace("Math.", string.Empty, StringComparison.OrdinalIgnoreCase)
                    .Replace('×', '*')
                    .Replace('÷', '/')
                    .Replace(',', '.');

                _expression = normalized;
                _ = Evaluate(0.123456789);
            }

            public double Evaluate(double x)
            {
                _x = x;
                _position = 0;
                double result = ParseExpression();
                SkipSpaces();
                if (_position != _expression.Length)
                {
                    throw Error($"Неожиданный символ '{_expression[_position]}'.");
                }
                return result;
            }

            private double ParseExpression()
            {
                double value = ParseTerm();
                while (true)
                {
                    SkipSpaces();
                    if (Match('+')) value += ParseTerm();
                    else if (Match('-')) value -= ParseTerm();
                    else return value;
                }
            }

            private double ParseTerm()
            {
                double value = ParseUnary();
                while (true)
                {
                    SkipSpaces();
                    if (Match('*')) value *= ParseUnary();
                    else if (Match('/')) value /= ParseUnary();
                    else return value;
                }
            }

            private double ParseUnary()
            {
                SkipSpaces();
                if (Match('+')) return ParseUnary();
                if (Match('-')) return -ParseUnary();
                return ParsePower();
            }

            private double ParsePower()
            {
                double left = ParsePrimary();
                SkipSpaces();
                if (Match('^'))
                {
                    double right = ParseUnary();
                    return Math.Pow(left, right);
                }
                return left;
            }

            private double ParsePrimary()
            {
                SkipSpaces();

                if (Match('('))
                {
                    double value = ParseExpression();
                    SkipSpaces();
                    Expect(')');
                    return value;
                }

                if (_position < _expression.Length &&
                    (char.IsDigit(_expression[_position]) || _expression[_position] == '.'))
                {
                    return ParseNumber();
                }

                if (_position < _expression.Length &&
                    (char.IsLetter(_expression[_position]) || _expression[_position] == '_'))
                {
                    string identifier = ParseIdentifier().ToLowerInvariant();

                    if (identifier == "x") return _x;
                    if (identifier == "pi") return Math.PI;
                    if (identifier == "e") return Math.E;

                    SkipSpaces();
                    Expect('(');
                    double argument = ParseExpression();
                    SkipSpaces();
                    Expect(')');

                    return identifier switch
                    {
                        "sin" => Math.Sin(argument),
                        "cos" => Math.Cos(argument),
                        "tan" or "tg" => Math.Tan(argument),
                        "asin" => Math.Asin(argument),
                        "acos" => Math.Acos(argument),
                        "atan" or "arctan" => Math.Atan(argument),
                        "sqrt" => Math.Sqrt(argument),
                        "abs" => Math.Abs(argument),
                        "exp" => Math.Exp(argument),
                        "ln" or "log" => Math.Log(argument),
                        "log10" => Math.Log10(argument),
                        "floor" => Math.Floor(argument),
                        "ceil" or "ceiling" => Math.Ceiling(argument),
                        "round" => Math.Round(argument),
                        _ => throw Error($"Неизвестная функция '{identifier}'.")
                    };
                }

                throw Error("Ожидалось число, переменная x, функция или скобка.");
            }

            private double ParseNumber()
            {
                int start = _position;
                bool exponentSeen = false;

                while (_position < _expression.Length)
                {
                    char ch = _expression[_position];
                    if (char.IsDigit(ch) || ch == '.')
                    {
                        _position++;
                        continue;
                    }

                    if ((ch == 'e' || ch == 'E') && !exponentSeen)
                    {
                        exponentSeen = true;
                        _position++;
                        if (_position < _expression.Length &&
                            (_expression[_position] == '+' || _expression[_position] == '-'))
                        {
                            _position++;
                        }
                        continue;
                    }

                    break;
                }

                string token = _expression[start.._position];
                if (!double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    throw Error($"Некорректное число '{token}'.");
                }
                return value;
            }

            private string ParseIdentifier()
            {
                int start = _position;
                while (_position < _expression.Length &&
                       (char.IsLetterOrDigit(_expression[_position]) || _expression[_position] == '_'))
                {
                    _position++;
                }
                return _expression[start.._position];
            }

            private void SkipSpaces()
            {
                while (_position < _expression.Length && char.IsWhiteSpace(_expression[_position]))
                {
                    _position++;
                }
            }

            private bool Match(char expected)
            {
                if (_position < _expression.Length && _expression[_position] == expected)
                {
                    _position++;
                    return true;
                }
                return false;
            }

            private void Expect(char expected)
            {
                if (!Match(expected))
                {
                    throw Error($"Ожидался символ '{expected}'.");
                }
            }

            private FormatException Error(string message)
            {
                return new FormatException($"Ошибка формулы около позиции {_position + 1}: {message}");
            }
        }
    }
}
