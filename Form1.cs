using System;
using System.Windows.Forms;

namespace Lab0_Task1_Programming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Считываем данные из полей
                double rho = Convert.ToDouble(textBox1.Text);
                double r = Convert.ToDouble(textBox2.Text);
                double m = Convert.ToDouble(textBox3.Text);

                // Проверяем корректность значений
                if (rho <= 0 || r <= 0 || m <= 0)
                {
                    MessageBox.Show(
                        "Плотность, радиус и масса должны быть больше нуля.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                // Формула:
                // m = ρ * V
                // V = π * r^2 * h
                // h = m / (ρ * π * r^2)

                double h = m / (rho * Math.PI * Math.Pow(r, 2));

                // Вывод результата
                label4.Text = $"Толщина диска h = {h:F4}";
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Введите числовые значения во все поля.",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show(
                    "Введено слишком большое число.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}