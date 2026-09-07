namespace Lab0_Task1_Programming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SelectLab(0);
        }

        private void LabButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is int labNumber)
            {
                SelectLab(labNumber);
            }
        }

        private void SelectLab(int labNumber)
        {
            SetSelectedLabButton(labNumber);

            bool isLab0 = labNumber == 0;

            task1Button.Visible = isLab0;
            task2Button.Visible = isLab0;

            taskMenuCaption.Text = $"Лаб. {labNumber} задания:";

            lab0Task1Point3Control.Visible = false;
            lab0Task2Point3Control.Visible = false;
            emptyLabLabel.Visible = false;

            if (isLab0)
            {
                ShowTask1();
            }
            else
            {
                taskTitleLabel.Text = $"Лабораторная работа №{labNumber}";
                taskDescriptionLabel.Text = "Задания для этой лабораторной пока не добавлены.";
                emptyLabLabel.Text = "Здесь позже появятся задания этой лабораторной.";
                emptyLabLabel.Visible = true;
                emptyLabLabel.BringToFront();
            }
        }

        private void Task1Button_Click(object? sender, EventArgs e)
        {
            ShowTask1();
        }

        private void Task2Button_Click(object? sender, EventArgs e)
        {
            ShowTask2();
        }

        private void ShowTask1()
        {
            taskTitleLabel.Text = "Лабораторная работа №0 — Задание №1, пункт 3";
            taskDescriptionLabel.Text =
                "Из материала плотностью ρ изготовлен диск радиусом r. " +
                "Определить толщину диска h, при которой его масса равна m.";

            lab0Task2Point3Control.Visible = false;
            emptyLabLabel.Visible = false;
            lab0Task1Point3Control.Visible = true;
            lab0Task1Point3Control.BringToFront();

            SetSelectedTaskButton(1);
        }

        private void ShowTask2()
        {
            taskTitleLabel.Text = "Лабораторная работа №0 — Задание №2, пункт 3";
            taskDescriptionLabel.Text =
                "Данные о группе людей: фамилия, имя, пол и рост. " +
                "Сгенерировать данные, сохранить их в CSV, считать файл, " +
                "вычислить средний рост мужчин и женщин и найти самых высоких.";

            lab0Task1Point3Control.Visible = false;
            emptyLabLabel.Visible = false;
            lab0Task2Point3Control.Visible = true;
            lab0Task2Point3Control.BringToFront();

            SetSelectedTaskButton(2);
        }

        private void SetSelectedLabButton(int selectedLab)
        {
            foreach (Control control in labMenuPanel.Controls)
            {
                if (control is Button button && button.Tag is int labNumber)
                {
                    button.Font = new Font(button.Font, labNumber == selectedLab
                        ? FontStyle.Bold
                        : FontStyle.Regular);
                }
            }
        }

        private void SetSelectedTaskButton(int selectedTask)
        {
            task1Button.Font = new Font(task1Button.Font,
                selectedTask == 1 ? FontStyle.Bold : FontStyle.Regular);
            task2Button.Font = new Font(task2Button.Font,
                selectedTask == 2 ? FontStyle.Bold : FontStyle.Regular);
        }

        private void taskTitleLabel_Click(object sender, EventArgs e)
        {

        }

        private void taskMenuCaption_Click(object sender, EventArgs e)
        {

        }
    }
}
