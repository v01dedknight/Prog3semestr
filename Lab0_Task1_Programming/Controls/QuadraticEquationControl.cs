using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Lab0_Task1_Programming.Controls
{
    public partial class QuadraticEquationControl : UserControl
    {
        public QuadraticEquationControl()
        {
            InitializeComponent();
            SolveCurrentEquation();
        }

        private void SolveMenuItem_Click(object? sender, EventArgs e) => SolveCurrentEquation();

        private void ClearMenuItem_Click(object? sender, EventArgs e)
        {
            aTextBox.Clear();
            bTextBox.Clear();
            cTextBox.Clear();
            discriminantTextBox.Clear();
            vietaTextBox.Clear();
            statusLabel.Text = "Введите коэффициенты a, b, c и выберите Расчёт → Решить.";
            quadraticPlotPanel.ClearGraph();
            aTextBox.Focus();
        }

        private void Example1MenuItem_Click(object? sender, EventArgs e) => SetExample("1", "-5", "6");
        private void Example2MenuItem_Click(object? sender, EventArgs e) => SetExample("1", "-2", "1");
        private void Example3MenuItem_Click(object? sender, EventArgs e) => SetExample("1", "2", "5");
        private void Example4MenuItem_Click(object? sender, EventArgs e) => SetExample("0,5", "-1,5", "1");
        private void Example5MenuItem_Click(object? sender, EventArgs e) => SetExample("0", "2", "-4");

        private void SetExample(string a, string b, string c)
        {
            aTextBox.Text = a;
            bTextBox.Text = b;
            cTextBox.Text = c;
            SolveCurrentEquation();
        }

        private void CoefficientTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SolveCurrentEquation();
            }
        }

        private static bool TryParseCoefficient(string text, out double result)
        {
            result = 0;
            if (string.IsNullOrWhiteSpace(text)) return false;
            string input = text.Trim().Replace(',', '.');
            return double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out result)
                && double.IsFinite(result);
        }

        private void SolveCurrentEquation()
        {
            if (!TryParseCoefficient(aTextBox.Text, out double a))
            {
                ShowInputError("Введите корректный конечный коэффициент a.", aTextBox);
                return;
            }
            if (!TryParseCoefficient(bTextBox.Text, out double b))
            {
                ShowInputError("Введите корректный конечный коэффициент b.", bTextBox);
                return;
            }
            if (!TryParseCoefficient(cTextBox.Text, out double c))
            {
                ShowInputError("Введите корректный конечный коэффициент c.", cTextBox);
                return;
            }

            try
            {
                QuadraticEquationSolution result = QuadraticEquationSolver.Solve(a, b, c);
                discriminantTextBox.Text = BuildDiscriminantExplanation(result);
                vietaTextBox.Text = BuildVietaExplanation(result);
                quadraticPlotPanel.SetEquation(result);
                statusLabel.Text = result.Kind switch
                {
                    QuadraticEquationKind.TwoReal => "Два различных действительных корня. Результат проверки по Виету приведён ниже.",
                    QuadraticEquationKind.OneReal => "Один действительный корень (двукратный). Результат проверки по Виету приведён ниже.",
                    QuadraticEquationKind.TwoComplex => "Действительных корней нет; комплексные корни и проверка по Виету показаны ниже.",
                    QuadraticEquationKind.Linear => "a = 0: уравнение линейное; теорема Виета для квадратного уравнения неприменима.",
                    QuadraticEquationKind.NoSolutions => "a = b = 0, c ≠ 0: решений нет.",
                    _ => "0 = 0: любое действительное число является решением."
                };
            }
            catch (ArithmeticException ex)
            {
                ShowCalculationError(ex.Message);
            }
            catch (ArgumentException ex)
            {
                ShowCalculationError(ex.Message);
            }
        }

        private void ShowInputError(string message, TextBox target)
        {
            statusLabel.Text = message;
            discriminantTextBox.Clear();
            vietaTextBox.Clear();
            quadraticPlotPanel.ClearGraph();
            // Do not pop up a modal dialog on every keystroke; the user submits with Enter/menu.
            target.Focus();
            target.SelectAll();
        }

        private void ShowCalculationError(string message)
        {
            statusLabel.Text = message;
            discriminantTextBox.Clear();
            vietaTextBox.Clear();
            quadraticPlotPanel.ClearGraph();
        }

        private static string N(double number)
        {
            if (number == 0) return "0";
            return number.ToString("G12", CultureInfo.GetCultureInfo("ru-RU"));
        }

        private static string BuildDiscriminantExplanation(QuadraticEquationSolution r)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Шаг 1. Записываем уравнение:");
            sb.AppendLine($"({N(r.A)})x² + ({N(r.B)})x + ({N(r.C)}) = 0");
            sb.AppendLine();

            if (r.A == 0)
            {
                sb.AppendLine("a = 0. Это не квадратное уравнение.");
                if (r.Kind == QuadraticEquationKind.Linear)
                {
                    sb.AppendLine($"Линейное уравнение: ({N(r.B)})x + ({N(r.C)}) = 0");
                    sb.AppendLine($"x = -c/b = -({N(r.C)})/({N(r.B)}) = {N(r.Root1Real)}");
                }
                else if (r.Kind == QuadraticEquationKind.AllRealNumbers)
                    sb.AppendLine("Равенство 0 = 0 верно при любом x.");
                else
                    sb.AppendLine("Получено неверное равенство c = 0. Решений нет.");
                return sb.ToString();
            }

            sb.AppendLine("Шаг 2. Находим дискриминант:");
            sb.AppendLine("D = b² - 4ac");
            sb.AppendLine($"D = ({N(r.B)})² - 4 × ({N(r.A)}) × ({N(r.C)}) = {N(r.Discriminant)}");
            sb.AppendLine();
            sb.AppendLine("Шаг 3. Находим корни:");

            switch (r.Kind)
            {
                case QuadraticEquationKind.TwoReal:
                    sb.AppendLine("D > 0, два различных действительных корня.");
                    sb.AppendLine("x₁,₂ = (-b ± √D) / (2a)");
                    sb.AppendLine($"√D = {N(Math.Sqrt(r.Discriminant))}");
                    sb.AppendLine($"x₁ = {N(r.Root1Real)}");
                    sb.AppendLine($"x₂ = {N(r.Root2Real)}");
                    break;
                case QuadraticEquationKind.OneReal:
                    sb.AppendLine("D = 0, один действительный корень кратности 2.");
                    sb.AppendLine("x = -b / (2a)");
                    sb.AppendLine($"x₁ = x₂ = {N(r.Root1Real)}");
                    break;
                case QuadraticEquationKind.TwoComplex:
                    sb.AppendLine("D < 0, действительных корней нет.");
                    sb.AppendLine("В комплексных числах:");
                    sb.AppendLine($"x₁ = {N(r.Root1Real)} + {N(r.Root1Imaginary)}i");
                    sb.AppendLine($"x₂ = {N(r.Root2Real)} - {N(Math.Abs(r.Root2Imaginary))}i");
                    break;
            }
            return sb.ToString();
        }

        private static bool ApproximatelyEqual(double actual, double expected)
        {
            if (actual == expected) return true;
            if (!double.IsFinite(actual) || !double.IsFinite(expected)) return false;
            return Math.Abs(actual - expected) <= 1e-9 * Math.Max(1.0, Math.Max(Math.Abs(actual), Math.Abs(expected)));
        }

        private static string BuildVietaExplanation(QuadraticEquationSolution r)
        {
            var sb = new StringBuilder();
            if (r.A == 0)
            {
                sb.AppendLine("Теорема Виета применяется только при a ≠ 0.");
                sb.AppendLine("Для линейного или вырожденного уравнения проверка не проводится.");
                return sb.ToString();
            }

            double expectedSum = -(r.B / r.A);
            double expectedProduct = r.C / r.A;
            double actualSum = r.Root1Real + r.Root2Real;
            double actualProduct;
            if (r.Kind == QuadraticEquationKind.TwoComplex)
                actualProduct = r.Root1Real * r.Root2Real - r.Root1Imaginary * r.Root2Imaginary;
            else
                actualProduct = r.Root1Real * r.Root2Real;

            sb.AppendLine("Теорема Виета:");
            sb.AppendLine("x₁ + x₂ = -b/a");
            sb.AppendLine("x₁ × x₂ = c/a");
            sb.AppendLine();
            sb.AppendLine("1. Проверяем сумму корней:");
            sb.AppendLine($"x₁ + x₂ = {N(actualSum)}");
            sb.AppendLine($"-b/a = {N(expectedSum)}");
            bool sumMatches = ApproximatelyEqual(actualSum, expectedSum);
            sb.AppendLine($"Итог: {(sumMatches ? "совпадает" : "не совпадает")}");
            sb.AppendLine();
            sb.AppendLine("2. Проверяем произведение корней:");
            sb.AppendLine($"x₁ × x₂ = {N(actualProduct)}");
            sb.AppendLine($"c/a = {N(expectedProduct)}");
            bool productMatches = ApproximatelyEqual(actualProduct, expectedProduct);
            sb.AppendLine($"Итог: {(productMatches ? "совпадает" : "не совпадает")}");
            sb.AppendLine();
            if (r.Kind == QuadraticEquationKind.TwoComplex)
                sb.AppendLine("Для комплексных сопряжённых корней сумма и произведение вещественны.");
            sb.AppendLine(sumMatches && productMatches
                ? "Проверка пройдена: корни удовлетворяют теореме Виета."
                : "Проверка не пройдена или ограничена точностью double. Уменьшите масштаб коэффициентов.");
            return sb.ToString();
        }
    }
}
