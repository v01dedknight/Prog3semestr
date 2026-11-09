
using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Lab0_Task1_Programming.Controls
{
    // Отвечает за ввод коэффициентов, вывод результатов и графика
    public partial class QuadraticEquationControl : UserControl
    {
        // Инициализирует интерфейс и запускает первоначальный расчёт
        public QuadraticEquationControl()
        {
            InitializeComponent();
            SolveCurrentEquation();
        }

        // Обработчик команды "Решить" из меню
        private void SolveMenuItem_Click(object? sender, EventArgs e)
            => SolveCurrentEquation();

        // Очищает все поля ввода, результаты и график
        private void ClearMenuItem_Click(object? sender, EventArgs e)
        {
            aTextBox.Clear();
            bTextBox.Clear();
            cTextBox.Clear();

            discriminantTextBox.Clear();
            vietaTextBox.Clear();

            statusLabel.Text =
                "Введите коэффициенты a, b, c и выберите Расчёт -> Решить.";

            quadraticPlotPanel.ClearGraph();
            aTextBox.Focus();
        }

        // Готовые примеры для демонстрации программы
        // 1: два различных действительных корня
        private void Example1MenuItem_Click(object? sender, EventArgs e)
            => SetExample("1", "-5", "6");

        // 2: один двукратный корень
        private void Example2MenuItem_Click(object? sender, EventArgs e)
            => SetExample("1", "-2", "1");

        // 3: комплексные корни
        private void Example3MenuItem_Click(object? sender, EventArgs e)
            => SetExample("1", "2", "5");

        // 4: дробные коэффициенты
        private void Example4MenuItem_Click(object? sender, EventArgs e)
            => SetExample("0,5", "-1,5", "1");

        // 5: линейное уравнение при a = 0
        private void Example5MenuItem_Click(object? sender, EventArgs e)
            => SetExample("0", "2", "-4");

        // Подставляет коэффициенты выбранного примера, и автоматически запускает решение
        private void SetExample(string a, string b, string c)
        {
            aTextBox.Text = a;
            bTextBox.Text = b;
            cTextBox.Text = c;

            SolveCurrentEquation();
        }

        // Позволяет запускать расчёт клавишей Enter
        private void CoefficientTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SolveCurrentEquation();
            }
        }

        // Преобразует введённый коэффициент в число double, сохраняет его корректное значение
        private static bool TryParseCoefficient(
            string text,
            out double result)
        {
            result = 0;

            if (string.IsNullOrWhiteSpace(text))
                return false;

            string input = text.Trim().Replace(',', '.');

            return double.TryParse(
                input,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out result)
                && double.IsFinite(result);
        }

        // Проверяет введённые данные, вызывает решатель, выводит результаты и обновляет график
        private void SolveCurrentEquation()
        {
            // Проверка коэффициента a
            if (!TryParseCoefficient(aTextBox.Text, out double a))
            {
                ShowInputError(
                    "Введите корректный конечный коэффициент a.",
                    aTextBox);
                return;
            }

            // Проверка коэффициента b
            if (!TryParseCoefficient(bTextBox.Text, out double b))
            {
                ShowInputError(
                    "Введите корректный конечный коэффициент b.",
                    bTextBox);
                return;
            }

            // Проверка коэффициента c
            if (!TryParseCoefficient(cTextBox.Text, out double c))
            {
                ShowInputError(
                    "Введите корректный конечный коэффициент c.",
                    cTextBox);
                return;
            }

            try
            {
                // Выполнение математического решения уравнения
                QuadraticEquationSolution result =
                    QuadraticEquationSolver.Solve(a, b, c);

                // Формирование пошагового решения через дискриминант
                discriminantTextBox.Text =
                    BuildDiscriminantExplanation(result);

                // Формирование проверки найденных корней по Виету
                vietaTextBox.Text =
                    BuildVietaExplanation(result);

                // Передача результатов компоненту построения графика
                quadraticPlotPanel.SetEquation(result);

                // Вывод сообщения в зависимости от типа уравнения
                statusLabel.Text = result.Kind switch
                {
                    QuadraticEquationKind.TwoReal =>
                        "Два различных действительных корня. Результат проверки по Виету приведён ниже.",

                    QuadraticEquationKind.OneReal =>
                        "Один действительный корень (двукратный). Результат проверки по Виету приведён ниже.",

                    QuadraticEquationKind.TwoComplex =>
                        "Действительных корней нет; комплексные корни и проверка по Виету показаны ниже.",

                    QuadraticEquationKind.Linear =>
                        "a = 0: уравнение линейное; теорема Виета для квадратного уравнения неприменима.",

                    QuadraticEquationKind.NoSolutions =>
                        "a = b = 0, c ≠ 0: решений нет.",

                    _ =>
                        "0 = 0: любое действительное число является решением."
                };
            }
            // Обработка ошибок математических вычислений
            catch (ArithmeticException ex)
            {
                ShowCalculationError(ex.Message);
            }
            // Обработка недопустимых аргументов
            catch (ArgumentException ex)
            {
                ShowCalculationError(ex.Message);
            }
        }

        // Вывод сообщения об ошибке пользовательского ввода, очистка предыдущих результатов и выделение проблемного поля
        private void ShowInputError(string message, TextBox target)
        {
            statusLabel.Text = message;

            discriminantTextBox.Clear();
            vietaTextBox.Clear();
            quadraticPlotPanel.ClearGraph();

            // Диалог скрывается, чтобы не создавать помеху вводу
            target.Focus();
            target.SelectAll();
        }

        // Обработка ошибок, возникших при вычислении, сброс результатов и графика
        private void ShowCalculationError(string message)
        {
            statusLabel.Text = message;

            discriminantTextBox.Clear();
            vietaTextBox.Clear();
            quadraticPlotPanel.ClearGraph();
        }

        // Преобразует число в строку для вывода пользователю
        private static string N(double number)
        {
            if (number == 0)
                return "0";

            return number.ToString(
                "G12",
                CultureInfo.GetCultureInfo("ru-RU"));
        }

        // Формирование подробное решение через дискриминант
        private static string BuildDiscriminantExplanation(
            QuadraticEquationSolution r)
        {
            var sb = new StringBuilder();

            // Запись исходного уравнения
            sb.AppendLine("Шаг 1. Записываем уравнение:");
            sb.AppendLine(
                $"({N(r.A)})x^2 + ({N(r.B)})x + ({N(r.C)}) = 0");
            sb.AppendLine();

            // Если a = 0, уравнение не является квадратным
            if (r.A == 0)
            {
                sb.AppendLine("a = 0. Это не квадратное уравнение.");

                if (r.Kind == QuadraticEquationKind.Linear)
                {
                    // Решение линейного уравнения bx + c = 0.
                    sb.AppendLine(
                        $"Линейное уравнение: ({N(r.B)})x + ({N(r.C)}) = 0");

                    sb.AppendLine(
                        $"x = -c/b = -({N(r.C)})/({N(r.B)}) = {N(r.Root1Real)}");
                }
                else if (r.Kind == QuadraticEquationKind.AllRealNumbers)
                {
                    // Все коэффициенты равны нулю
                    sb.AppendLine(
                        "Равенство 0 = 0 верно при любом x.");
                }
                else
                {
                    // Получено противоречивое равенство
                    sb.AppendLine(
                        "Получено неверное равенство c = 0. Решений нет.");
                }

                return sb.ToString();
            }

            // Демонстрация вычисления дискриминанта
            sb.AppendLine("Шаг 2. Находим дискриминант:");
            sb.AppendLine("D = b^2 - 4ac");

            sb.AppendLine(
                $"D = ({N(r.B)})² - 4 * ({N(r.A)}) * ({N(r.C)}) = {N(r.Discriminant)}");

            sb.AppendLine();
            sb.AppendLine("Шаг 3. Находим корни:");

            // Выбор способа отображения корней, в зависимости от значения дискриминанта
            switch (r.Kind)
            {
                // D > 0: два различных действительных корня
                case QuadraticEquationKind.TwoReal:
                    sb.AppendLine(
                        "D > 0, два различных действительных корня.");

                    sb.AppendLine("x1,2 = (-b ± √D) / (2a)");

                    sb.AppendLine(
                        $"√D = {N(Math.Sqrt(r.Discriminant))}");

                    sb.AppendLine($"x1 = {N(r.Root1Real)}");
                    sb.AppendLine($"x2 = {N(r.Root2Real)}");
                    break;

                // D = 0: один корень кратности два
                case QuadraticEquationKind.OneReal:
                    sb.AppendLine(
                        "D = 0, один действительный корень кратности 2.");

                    sb.AppendLine("x = -b / (2a)");

                    sb.AppendLine(
                        $"x1 = x2 = {N(r.Root1Real)}");
                    break;

                // D < 0: действительных корней нет
                case QuadraticEquationKind.TwoComplex:
                    sb.AppendLine(
                        "D < 0, действительных корней нет.");

                    sb.AppendLine("В комплексных числах:");

                    sb.AppendLine(
                        $"x1 = {N(r.Root1Real)} + {N(r.Root1Imaginary)}i");

                    sb.AppendLine(
                        $"x2 = {N(r.Root2Real)} - {N(Math.Abs(r.Root2Imaginary))}i");
                    break;
            }

            // Возвращается сформированный текст решения
            return sb.ToString();
        }

        // Сравнение двух вещественных чисел с учётом погрешности, чтобы избежать ошибок округления
        private static bool ApproximatelyEqual(
            double actual,
            double expected)
        {
            if (actual == expected)
                return true;

            if (!double.IsFinite(actual) ||
                !double.IsFinite(expected))
                return false;

            return Math.Abs(actual - expected)
                <= 1e-9 * Math.Max(
                    1.0,
                    Math.Max(Math.Abs(actual), Math.Abs(expected)));
        }

        // Формирование проверки найденных корней по теореме Виета
        private static string BuildVietaExplanation(
            QuadraticEquationSolution r)
        {
            var sb = new StringBuilder();

            // Теорема Виета неприменима, если a = 0
            if (r.A == 0)
            {
                sb.AppendLine(
                    "Теорема Виета применяется только при a ≠ 0.");

                sb.AppendLine(
                    "Для линейного или вырожденного уравнения проверка не проводится.");

                return sb.ToString();
            }

            // Вычисление ожидаемой суммы и произведения по коэффициентам квадратного уравнения
            double expectedSum = -(r.B / r.A);
            double expectedProduct = r.C / r.A;

            // Вычисление фактической суммы найденных корней
            double actualSum = r.Root1Real + r.Root2Real;

            double actualProduct;

            // Для комплексных корней учитываются мнимые части
            if (r.Kind == QuadraticEquationKind.TwoComplex)
            {
                actualProduct =
                    r.Root1Real * r.Root2Real
                    - r.Root1Imaginary * r.Root2Imaginary;
            }
            else
            {
                // Для действительных корней обычное произведение
                actualProduct = r.Root1Real * r.Root2Real;
            }

            // Вывод основных формул теоремы Виета
            sb.AppendLine("Теорема Виета:");
            sb.AppendLine("x1 + x2 = -b/a");
            sb.AppendLine("x1 × x2 = c/a");
            sb.AppendLine();

            // Проверка суммы корней
            sb.AppendLine("1. Проверяем сумму корней:");
            sb.AppendLine($"x1 + x2 = {N(actualSum)}");
            sb.AppendLine($"-b/a = {N(expectedSum)}");

            bool sumMatches =
                ApproximatelyEqual(actualSum, expectedSum);

            sb.AppendLine(
                $"Итог: {(sumMatches ? "совпадает" : "не совпадает")}");

            sb.AppendLine();

            // Проверка произведения корней
            sb.AppendLine("2. Проверяем произведение корней:");
            sb.AppendLine($"x1 × x2 = {N(actualProduct)}");
            sb.AppendLine($"c/a = {N(expectedProduct)}");

            bool productMatches =
                ApproximatelyEqual(actualProduct, expectedProduct);

            sb.AppendLine(
                $"Итог: {(productMatches ? "совпадает" : "не совпадает")}");

            sb.AppendLine();

            // Дополнительное пояснение для комплексных корней
            if (r.Kind == QuadraticEquationKind.TwoComplex)
            {
                sb.AppendLine(
                    "Для комплексных сопряжённых корней сумма и произведение вещественны.");
            }

            // Общий результат проверки по теореме Виета
            sb.AppendLine(
                sumMatches && productMatches
                    ? "Проверка пройдена: корни удовлетворяют теореме Виета."
                    : "Проверка не пройдена или ограничена точностью double. Уменьшите масштаб коэффициентов.");

            return sb.ToString();
        }
    }
}
