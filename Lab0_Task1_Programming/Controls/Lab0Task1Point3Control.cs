using System.Globalization;

namespace Lab0_Task1_Programming.Controls
{
    public partial class Lab0Task1Point3Control : UserControl
    {
        public Lab0Task1Point3Control()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object? sender, EventArgs e)
        {
            resultLabel.Text = "Толщина диска h =";

            if (!TryReadPositiveNumber(
                    densityTextBox,
                    "Введите плотность материала.",
                    "Плотность должна быть положительным числом.",
                    out double rho))
            {
                return;
            }

            if (!TryReadPositiveNumber(
                    radiusTextBox,
                    "Введите радиус диска.",
                    "Радиус должен быть положительным числом.",
                    out double radius))
            {
                return;
            }

            if (!TryReadPositiveNumber(
                    massTextBox,
                    "Введите массу диска.",
                    "Масса должна быть положительным числом.",
                    out double mass))
            {
                return;
            }

            double thickness = mass / (rho * Math.PI * Math.Pow(radius, 2));
            resultLabel.Text = $"Толщина диска h = {thickness:F4}";
        }

        private static bool TryReadPositiveNumber(
            TextBox textBox,
            string emptyMessage,
            string invalidMessage,
            out double value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                ShowInputError(textBox, emptyMessage);
                return false;
            }

            if (!TryParseNumber(textBox.Text, out value) ||
                double.IsNaN(value) ||
                double.IsInfinity(value) ||
                value <= 0)
            {
                ShowInputError(textBox, invalidMessage);
                return false;
            }

            return true;
        }

        private static bool TryParseNumber(string text, out double value)
        {
            text = text.Trim();

            if (double.TryParse(
                text,
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out value))
            {
                return true;
            }

            text = text.Replace(',', '.');

            return double.TryParse(
                text,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out value);
        }

        private static void ShowInputError(TextBox textBox, string message)
        {
            MessageBox.Show(
                message,
                "Ошибка ввода",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            textBox.Focus();
            textBox.SelectAll();
        }
    }
}
