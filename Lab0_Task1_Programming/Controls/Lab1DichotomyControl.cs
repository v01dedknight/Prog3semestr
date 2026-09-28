using System.Globalization;
using System.Text;

namespace Lab0_Task1_Programming.Controls
{
    public partial class Lab1DichotomyControl : UserControl
    {
        private double? _lastPointX;
        private string _lastPointLabel = string.Empty;
        private string _lastFormula = string.Empty;
        private double _lastA;
        private double _lastB;

        public Lab1DichotomyControl()
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

                if (modeComboBox.SelectedIndex == 1)
                {
                    CalculateRoot(function, formula, a, b, epsilon);
                }
                else
                {
                    CalculateMinimum(function, formula, a, b, epsilon);
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

                double? marker = null;
                string markerText = string.Empty;
                if (_lastPointX.HasValue &&
                    string.Equals(_lastFormula, formula, StringComparison.Ordinal) &&
                    NearlyEqual(_lastA, a) &&
                    NearlyEqual(_lastB, b))
                {
                    marker = _lastPointX;
                    markerText = _lastPointLabel;
                }

                functionPlotPanel.SetPlot(function, a, b, marker, markerText);
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
            warningLabel.Text = "Если на интервале несколько корней/минимумов или разрыв функции, сузьте интервал.";
            calculationsTextBox.Clear();
            functionPlotPanel.ClearPlot();
            _lastPointX = null;
            _lastPointLabel = string.Empty;
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
                MessageBox.Show(this,
                    "Погрешность e должна быть меньше длины интервала b - a.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void CalculateMinimum(
            Func<double, double> function,
            string formula,
            double a,
            double b,
            double epsilon)
        {
            const int maxIterations = 1_000_000;
            const int maxDisplayedIterations = 500;

            var log = new StringBuilder();
            log.AppendLine("Метод дихотомии для поиска локального минимума");
            log.AppendLine($"f(x) = {formula}");
            log.AppendLine($"Начальный интервал: [{Format(a)}; {Format(b)}]");
            log.AppendLine($"e = {Format(epsilon)}");
            log.AppendLine();

            double left = a;
            double right = b;
            double delta = epsilon / 4.0;
            int iterations = 0;

            ValidateFunctionOnInterval(function, a, b, out string diagnostic);

            while (right - left > epsilon)
            {
                if (++iterations > maxIterations)
                {
                    throw new InvalidOperationException("Превышено допустимое число итераций. Проверьте функцию и погрешность.");
                }

                double middle = (left + right) / 2.0;
                double x1 = middle - delta;
                double x2 = middle + delta;
                double f1 = EvaluateFinite(function, x1);
                double f2 = EvaluateFinite(function, x2);

                if (iterations <= maxDisplayedIterations)
                {
                    log.AppendLine(
                        $"{iterations,3}: [{Format(left)}; {Format(right)}]  " +
                        $"x1={Format(x1)}, f1={Format(f1)}; x2={Format(x2)}, f2={Format(f2)}");
                }

                if (f1 <= f2)
                {
                    right = x2;
                }
                else
                {
                    left = x1;
                }
            }

            if (iterations > maxDisplayedIterations)
            {
                log.AppendLine($"... показаны первые {maxDisplayedIterations} итераций из {iterations}.");
            }

            double xMin = (left + right) / 2.0;
            double yMin = EvaluateFinite(function, xMin);

            answerLabel.Text = $"Минимум: x ≈ {Format(xMin)}, f(x) ≈ {Format(yMin)}";
            iterationsLabel.Text = $"Итераций: {iterations}; конечный интервал: [{Format(left)}; {Format(right)}]";
            calculationsTextBox.Text = log.ToString();

            warningLabel.ForeColor = SystemColors.GrayText;
            warningLabel.Text = string.IsNullOrWhiteSpace(diagnostic)
                ? "Результат относится к выбранному интервалу [a, b]."
                : diagnostic;

            SaveLastResult(formula, a, b, xMin, $"min: x={Format(xMin)}, f={Format(yMin)}");
            functionPlotPanel.SetPlot(function, a, b, xMin, _lastPointLabel);
        }

        private void CalculateRoot(
            Func<double, double> function,
            string formula,
            double a,
            double b,
            double epsilon)
        {
            const int maxIterations = 1_000_000;
            const int maxDisplayedIterations = 500;

            double left = a;
            double right = b;
            double fLeft = EvaluateFinite(function, left);
            double fRight = EvaluateFinite(function, right);

            var diagnosticParts = new List<string>();
            AnalyzeRootInterval(function, a, b, diagnosticParts);

            var log = new StringBuilder();
            log.AppendLine("Метод половинного деления для поиска корня");
            log.AppendLine($"f(x) = {formula}");
            log.AppendLine($"Начальный интервал: [{Format(a)}; {Format(b)}]");
            log.AppendLine($"f(a) = {Format(fLeft)}, f(b) = {Format(fRight)}");
            log.AppendLine($"e = {Format(epsilon)}");
            log.AppendLine();

            if (Math.Abs(fLeft) <= epsilon)
            {
                FinishRoot(function, formula, a, b, epsilon, left, fLeft, 0, log, diagnosticParts);
                return;
            }

            if (Math.Abs(fRight) <= epsilon)
            {
                FinishRoot(function, formula, a, b, epsilon, right, fRight, 0, log, diagnosticParts);
                return;
            }

            if (Math.Sign(fLeft) == Math.Sign(fRight))
            {
                warningLabel.ForeColor = Color.DarkRed;
                string extraDiagnostic = diagnosticParts.Count > 0
                    ? " " + string.Join(" ", diagnosticParts)
                    : string.Empty;
                warningLabel.Text =
                    "На концах интервала функция имеет одинаковый знак. Метод половинного деления не гарантирует корень. " +
                    "Измените интервал так, чтобы f(a) и f(b) имели разные знаки." + extraDiagnostic;
                calculationsTextBox.Text = log.ToString();
                answerLabel.Text = "Корень не найден: нет гарантированной смены знака на концах интервала.";
                iterationsLabel.Text = "Итераций: 0";
                functionPlotPanel.SetPlot(function, a, b);
                _lastPointX = null;
                return;
            }

            int iterations = 0;
            double middle = (left + right) / 2.0;
            double fMiddle = EvaluateFinite(function, middle);

            while (right - left > epsilon && Math.Abs(fMiddle) > epsilon)
            {
                if (++iterations > maxIterations)
                {
                    throw new InvalidOperationException("Превышено допустимое число итераций. Проверьте функцию и погрешность.");
                }

                middle = (left + right) / 2.0;
                fMiddle = EvaluateFinite(function, middle);

                if (iterations <= maxDisplayedIterations)
                {
                    log.AppendLine(
                        $"{iterations,3}: [{Format(left)}; {Format(right)}]  " +
                        $"c={Format(middle)}, f(c)={Format(fMiddle)}");
                }

                if (Math.Abs(fMiddle) <= epsilon)
                {
                    break;
                }

                if (Math.Sign(fLeft) != Math.Sign(fMiddle))
                {
                    right = middle;
                    fRight = fMiddle;
                }
                else
                {
                    left = middle;
                    fLeft = fMiddle;
                }
            }

            if (iterations > maxDisplayedIterations)
            {
                log.AppendLine($"... показаны первые {maxDisplayedIterations} итераций из {iterations}.");
            }

            middle = (left + right) / 2.0;
            fMiddle = EvaluateFinite(function, middle);
            FinishRoot(function, formula, a, b, epsilon, middle, fMiddle, iterations, log, diagnosticParts);
        }

        private void FinishRoot(
            Func<double, double> function,
            string formula,
            double a,
            double b,
            double epsilon,
            double root,
            double fRoot,
            int iterations,
            StringBuilder log,
            List<string> diagnosticParts)
        {
            answerLabel.Text = $"Корень: x ≈ {Format(root)}, f(x) ≈ {Format(fRoot)}";
            iterationsLabel.Text = $"Итераций: {iterations}; заданная точность e = {Format(epsilon)}";
            calculationsTextBox.Text = log.ToString();

            warningLabel.ForeColor = diagnosticParts.Count > 0 ? Color.DarkOrange : SystemColors.GrayText;
            warningLabel.Text = diagnosticParts.Count > 0
                ? string.Join(" ", diagnosticParts)
                : "Смена знака на интервале найдена. Результат относится к выбранному интервалу.";

            SaveLastResult(formula, a, b, root, $"root: x={Format(root)}, f={Format(fRoot)}");
            functionPlotPanel.SetPlot(function, a, b, root, _lastPointLabel);
        }

        private void AnalyzeRootInterval(
            Func<double, double> function,
            double a,
            double b,
            List<string> diagnostics)
        {
            const int samples = 300;
            int signChanges = 0;
            int invalidPoints = 0;
            double? previous = null;

            for (int i = 0; i <= samples; i++)
            {
                double x = a + (b - a) * i / samples;
                try
                {
                    double y = function(x);
                    if (!double.IsFinite(y) || Math.Abs(y) > 1e12)
                    {
                        invalidPoints++;
                        previous = null;
                        continue;
                    }

                    if (previous.HasValue && y != 0 && previous.Value != 0 && Math.Sign(y) != Math.Sign(previous.Value))
                    {
                        signChanges++;
                    }
                    previous = y;
                }
                catch
                {
                    invalidPoints++;
                    previous = null;
                }
            }

            if (signChanges > 1)
            {
                diagnostics.Add(
                    $"На интервале обнаружено несколько смен знака ({signChanges}). Лучше сузить [a, b], чтобы изолировать один корень.");
            }

            if (invalidPoints > 0)
            {
                diagnostics.Add(
                    "На интервале есть точки, где функция не определена или имеет очень большой скачок. Возможен разрыв; измените интервал.");
            }
        }

        private void ValidateFunctionOnInterval(
            Func<double, double> function,
            double a,
            double b,
            out string diagnostic)
        {
            const int samples = 200;
            int invalidPoints = 0;
            int detectedValleys = 0;
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

                    if (yBeforePrevious.HasValue && yPrevious.HasValue &&
                        yPrevious.Value < yBeforePrevious.Value && yPrevious.Value < y)
                    {
                        detectedValleys++;
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
                messages.Add("На интервале обнаружены неопределённые значения/разрыв. Результат может быть некорректным — измените интервал.");
            }
            if (detectedValleys > 1)
            {
                messages.Add($"На графике обнаружено несколько возможных локальных минимумов ({detectedValleys}). Для метода дихотомии лучше сузить интервал до одного минимума.");
            }

            diagnostic = string.Join(" ", messages);
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

        private void SaveLastResult(string formula, double a, double b, double x, string label)
        {
            _lastFormula = formula;
            _lastA = a;
            _lastB = b;
            _lastPointX = x;
            _lastPointLabel = label;
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

                // Ранний контроль синтаксиса.
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
                    if (Match('+'))
                    {
                        value += ParseTerm();
                    }
                    else if (Match('-'))
                    {
                        value -= ParseTerm();
                    }
                    else
                    {
                        return value;
                    }
                }
            }

            private double ParseTerm()
            {
                double value = ParseUnary();
                while (true)
                {
                    SkipSpaces();
                    if (Match('*'))
                    {
                        value *= ParseUnary();
                    }
                    else if (Match('/'))
                    {
                        double divisor = ParseUnary();
                        value /= divisor;
                    }
                    else
                    {
                        return value;
                    }
                }
            }

            private double ParseUnary()
            {
                SkipSpaces();
                if (Match('+'))
                {
                    return ParseUnary();
                }
                if (Match('-'))
                {
                    return -ParseUnary();
                }
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
