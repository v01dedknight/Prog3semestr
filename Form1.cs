using System;
using System.Globalization;
using System.Windows.Forms;

namespace Lab0_Task1_Programming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            // Сбро старого результата
            labelResult.Text = "Толщина диска h =";

            // Проверка пустых полей
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show(
                    "Введите плотность материала.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show(
                    "Введите радиус диска.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox2.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show(
                    "Введите массу диска.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox3.Focus();
                return;
            }

            // Проверка плотности
            if (!TryParseNumber(textBox1.Text, out double rho))
            {
                MessageBox.Show(
                    "Плотность должна быть числом.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textBox1.Focus();
                textBox1.SelectAll();
                return;
            }

            // Проверка радиуса
            if (!TryParseNumber(textBox2.Text, out double r))
            {
                MessageBox.Show(
                    "Радиус должен быть числом.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textBox2.Focus();
                textBox2.SelectAll();
                return;
            }

            // Проверка массы
            if (!TryParseNumber(textBox3.Text, out double m))
            {
                MessageBox.Show(
                    "Масса должна быть числом.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textBox3.Focus();
                textBox3.SelectAll();
                return;
            }

            // Проверка положительных значений
            if (rho <= 0)
            {
                MessageBox.Show(
                    "Плотность должна быть больше нуля.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textBox1.Focus();
                textBox1.SelectAll();
                return;
            }

            if (r <= 0)
            {
                MessageBox.Show(
                    "Радиус должен быть больше нуля.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textBox2.Focus();
                textBox2.SelectAll();
                return;
            }

            if (m <= 0)
            {
                MessageBox.Show(
                    "Масса должна быть больше нуля.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                textBox3.Focus();
                textBox3.SelectAll();
                return;
            }

            // Формула

            double h = m / (rho * Math.PI * Math.Pow(r, 2));

            // Вывод результата
            labelResult.Text = $"Толщина диска h = {h:F4}";
        }

        private static bool TryParseNumber(string text, out double value)
        {
            text = text.Trim();

            // Пробуем формат, используемый Windows
            if (double.TryParse(
                text,
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out value))
            {
                return true;
            }

            // Позволяем вводить число через точку или запятую
            text = text.Replace(',', '.');

            if (double.TryParse(
                text,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out value))
            {
                return true;
            }

            return false;
        }
    }
}