namespace Lab0_Task1_Programming
{
    partial class Form1
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            labMenuPanel = new FlowLayoutPanel();
            labMenuCaption = new Label();
            lab0Button = new Button();
            lab1Button = new Button();
            lab2Button = new Button();
            lab3Button = new Button();
            lab4Button = new Button();
            lab5Button = new Button();
            lab6Button = new Button();
            lab7Button = new Button();
            lab8Button = new Button();
            taskMenuPanel = new FlowLayoutPanel();
            taskMenuCaption = new Label();
            task1Button = new Button();
            task2Button = new Button();
            headerPanel = new Panel();
            taskDescriptionLabel = new Label();
            taskTitleLabel = new Label();
            contentPanel = new Panel();
            emptyLabLabel = new Label();
            lab0Task2Point3Control = new Lab0_Task1_Programming.Controls.Lab0Task2Point3Control();
            lab0Task1Point3Control = new Lab0_Task1_Programming.Controls.Lab0Task1Point3Control();
            rootLayout.SuspendLayout();
            labMenuPanel.SuspendLayout();
            taskMenuPanel.SuspendLayout();
            headerPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(labMenuPanel, 0, 0);
            rootLayout.Controls.Add(taskMenuPanel, 0, 1);
            rootLayout.Controls.Add(headerPanel, 0, 2);
            rootLayout.Controls.Add(contentPanel, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1180, 760);
            rootLayout.TabIndex = 0;
            // 
            // labMenuPanel
            // 
            labMenuPanel.AutoScroll = true;
            labMenuPanel.Controls.Add(labMenuCaption);
            labMenuPanel.Controls.Add(lab0Button);
            labMenuPanel.Controls.Add(lab1Button);
            labMenuPanel.Controls.Add(lab2Button);
            labMenuPanel.Controls.Add(lab3Button);
            labMenuPanel.Controls.Add(lab4Button);
            labMenuPanel.Controls.Add(lab5Button);
            labMenuPanel.Controls.Add(lab6Button);
            labMenuPanel.Controls.Add(lab7Button);
            labMenuPanel.Controls.Add(lab8Button);
            labMenuPanel.Dock = DockStyle.Fill;
            labMenuPanel.Location = new Point(0, 0);
            labMenuPanel.Margin = new Padding(0);
            labMenuPanel.Name = "labMenuPanel";
            labMenuPanel.Padding = new Padding(12, 12, 12, 8);
            labMenuPanel.Size = new Size(1180, 60);
            labMenuPanel.TabIndex = 0;
            labMenuPanel.WrapContents = false;
            // 
            // labMenuCaption
            // 
            labMenuCaption.AutoSize = true;
            labMenuCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labMenuCaption.Location = new Point(15, 21);
            labMenuCaption.Margin = new Padding(3, 9, 12, 0);
            labMenuCaption.Name = "labMenuCaption";
            labMenuCaption.Size = new Size(95, 15);
            labMenuCaption.TabIndex = 0;
            labMenuCaption.Text = "Лабораторные:";
            // 
            // lab0Button
            // 
            lab0Button.Location = new Point(122, 12);
            lab0Button.Margin = new Padding(0, 0, 8, 0);
            lab0Button.Name = "lab0Button";
            lab0Button.Size = new Size(82, 34);
            lab0Button.TabIndex = 1;
            lab0Button.Tag = 0;
            lab0Button.Text = "Лаб. 0";
            lab0Button.UseVisualStyleBackColor = true;
            lab0Button.Click += LabButton_Click;
            // 
            // lab1Button
            // 
            lab1Button.Location = new Point(212, 12);
            lab1Button.Margin = new Padding(0, 0, 8, 0);
            lab1Button.Name = "lab1Button";
            lab1Button.Size = new Size(82, 34);
            lab1Button.TabIndex = 2;
            lab1Button.Tag = 1;
            lab1Button.Text = "Лаб. 1";
            lab1Button.UseVisualStyleBackColor = true;
            lab1Button.Click += LabButton_Click;
            // 
            // lab2Button
            // 
            lab2Button.Location = new Point(302, 12);
            lab2Button.Margin = new Padding(0, 0, 8, 0);
            lab2Button.Name = "lab2Button";
            lab2Button.Size = new Size(82, 34);
            lab2Button.TabIndex = 3;
            lab2Button.Tag = 2;
            lab2Button.Text = "Лаб. 2";
            lab2Button.UseVisualStyleBackColor = true;
            lab2Button.Click += LabButton_Click;
            // 
            // lab3Button
            // 
            lab3Button.Location = new Point(392, 12);
            lab3Button.Margin = new Padding(0, 0, 8, 0);
            lab3Button.Name = "lab3Button";
            lab3Button.Size = new Size(82, 34);
            lab3Button.TabIndex = 4;
            lab3Button.Tag = 3;
            lab3Button.Text = "Лаб. 3";
            lab3Button.UseVisualStyleBackColor = true;
            lab3Button.Click += LabButton_Click;
            // 
            // lab4Button
            // 
            lab4Button.Location = new Point(482, 12);
            lab4Button.Margin = new Padding(0, 0, 8, 0);
            lab4Button.Name = "lab4Button";
            lab4Button.Size = new Size(82, 34);
            lab4Button.TabIndex = 5;
            lab4Button.Tag = 4;
            lab4Button.Text = "Лаб. 4";
            lab4Button.UseVisualStyleBackColor = true;
            lab4Button.Click += LabButton_Click;
            // 
            // lab5Button
            // 
            lab5Button.Location = new Point(572, 12);
            lab5Button.Margin = new Padding(0, 0, 8, 0);
            lab5Button.Name = "lab5Button";
            lab5Button.Size = new Size(82, 34);
            lab5Button.TabIndex = 6;
            lab5Button.Tag = 5;
            lab5Button.Text = "Лаб. 5";
            lab5Button.UseVisualStyleBackColor = true;
            lab5Button.Click += LabButton_Click;
            // 
            // lab6Button
            // 
            lab6Button.Location = new Point(662, 12);
            lab6Button.Margin = new Padding(0, 0, 8, 0);
            lab6Button.Name = "lab6Button";
            lab6Button.Size = new Size(82, 34);
            lab6Button.TabIndex = 7;
            lab6Button.Tag = 6;
            lab6Button.Text = "Лаб. 6";
            lab6Button.UseVisualStyleBackColor = true;
            lab6Button.Click += LabButton_Click;
            // 
            // lab7Button
            // 
            lab7Button.Location = new Point(752, 12);
            lab7Button.Margin = new Padding(0, 0, 8, 0);
            lab7Button.Name = "lab7Button";
            lab7Button.Size = new Size(82, 34);
            lab7Button.TabIndex = 8;
            lab7Button.Tag = 7;
            lab7Button.Text = "Лаб. 7";
            lab7Button.UseVisualStyleBackColor = true;
            lab7Button.Click += LabButton_Click;
            // 
            // lab8Button
            // 
            lab8Button.Location = new Point(842, 12);
            lab8Button.Margin = new Padding(0, 0, 8, 0);
            lab8Button.Name = "lab8Button";
            lab8Button.Size = new Size(82, 34);
            lab8Button.TabIndex = 9;
            lab8Button.Tag = 8;
            lab8Button.Text = "Лаб. 8";
            lab8Button.UseVisualStyleBackColor = true;
            lab8Button.Click += LabButton_Click;
            // 
            // taskMenuPanel
            // 
            taskMenuPanel.AutoScroll = true;
            taskMenuPanel.Controls.Add(taskMenuCaption);
            taskMenuPanel.Controls.Add(task1Button);
            taskMenuPanel.Controls.Add(task2Button);
            taskMenuPanel.Dock = DockStyle.Fill;
            taskMenuPanel.Location = new Point(0, 60);
            taskMenuPanel.Margin = new Padding(0);
            taskMenuPanel.Name = "taskMenuPanel";
            taskMenuPanel.Padding = new Padding(12, 9, 12, 7);
            taskMenuPanel.Size = new Size(1180, 54);
            taskMenuPanel.TabIndex = 1;
            taskMenuPanel.WrapContents = false;
            // 
            // taskMenuCaption
            // 
            taskMenuCaption.AutoSize = true;
            taskMenuCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            taskMenuCaption.Location = new Point(15, 18);
            taskMenuCaption.Margin = new Padding(3, 9, 12, 0);
            taskMenuCaption.Name = "taskMenuCaption";
            taskMenuCaption.Size = new Size(95, 15);
            taskMenuCaption.TabIndex = 0;
            taskMenuCaption.Text = "Лаб. 0 задания:";
            taskMenuCaption.Click += taskMenuCaption_Click;
            // 
            // task1Button
            // 
            task1Button.AutoSize = true;
            task1Button.Location = new Point(122, 9);
            task1Button.Margin = new Padding(0, 0, 8, 0);
            task1Button.MinimumSize = new Size(130, 34);
            task1Button.Name = "task1Button";
            task1Button.Size = new Size(130, 34);
            task1Button.TabIndex = 1;
            task1Button.Text = "Задание 1 п.3";
            task1Button.UseVisualStyleBackColor = true;
            task1Button.Click += Task1Button_Click;
            // 
            // task2Button
            // 
            task2Button.AutoSize = true;
            task2Button.Location = new Point(260, 9);
            task2Button.Margin = new Padding(0, 0, 8, 0);
            task2Button.MinimumSize = new Size(130, 34);
            task2Button.Name = "task2Button";
            task2Button.Size = new Size(130, 34);
            task2Button.TabIndex = 2;
            task2Button.Text = "Задание 2 п.3";
            task2Button.UseVisualStyleBackColor = true;
            task2Button.Click += Task2Button_Click;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(taskDescriptionLabel);
            headerPanel.Controls.Add(taskTitleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(0, 114);
            headerPanel.Margin = new Padding(0);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(18, 12, 18, 8);
            headerPanel.Size = new Size(1180, 112);
            headerPanel.TabIndex = 2;
            // 
            // taskDescriptionLabel
            // 
            taskDescriptionLabel.Dock = DockStyle.Fill;
            taskDescriptionLabel.Location = new Point(18, 48);
            taskDescriptionLabel.Name = "taskDescriptionLabel";
            taskDescriptionLabel.Padding = new Padding(0, 7, 0, 0);
            taskDescriptionLabel.Size = new Size(1144, 56);
            taskDescriptionLabel.TabIndex = 1;
            taskDescriptionLabel.Text = "Из материала плотностью ρ изготовлен диск радиусом r. Определить толщину диска h, при которой его масса равна m.";
            // 
            // taskTitleLabel
            // 
            taskTitleLabel.Dock = DockStyle.Top;
            taskTitleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            taskTitleLabel.Location = new Point(18, 12);
            taskTitleLabel.Name = "taskTitleLabel";
            taskTitleLabel.Size = new Size(1144, 36);
            taskTitleLabel.TabIndex = 0;
            taskTitleLabel.Text = "Лабораторная работа №0 - Задание №1, пункт 3";
            taskTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            taskTitleLabel.Click += taskTitleLabel_Click;
            // 
            // contentPanel
            // 
            contentPanel.Controls.Add(emptyLabLabel);
            contentPanel.Controls.Add(lab0Task2Point3Control);
            contentPanel.Controls.Add(lab0Task1Point3Control);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 226);
            contentPanel.Margin = new Padding(0);
            contentPanel.Name = "contentPanel";
            contentPanel.Padding = new Padding(18);
            contentPanel.Size = new Size(1180, 534);
            contentPanel.TabIndex = 3;
            // 
            // emptyLabLabel
            // 
            emptyLabLabel.Dock = DockStyle.Fill;
            emptyLabLabel.Font = new Font("Segoe UI", 11F);
            emptyLabLabel.Location = new Point(18, 18);
            emptyLabLabel.Name = "emptyLabLabel";
            emptyLabLabel.Size = new Size(1144, 498);
            emptyLabLabel.TabIndex = 2;
            emptyLabLabel.Text = "Здесь позже появятся задания этой лабораторной.";
            emptyLabLabel.Visible = false;
            // 
            // lab0Task2Point3Control
            // 
            lab0Task2Point3Control.Dock = DockStyle.Fill;
            lab0Task2Point3Control.Location = new Point(18, 18);
            lab0Task2Point3Control.Name = "lab0Task2Point3Control";
            lab0Task2Point3Control.Size = new Size(1144, 498);
            lab0Task2Point3Control.TabIndex = 1;
            lab0Task2Point3Control.Visible = false;
            // 
            // lab0Task1Point3Control
            // 
            lab0Task1Point3Control.AutoScroll = true;
            lab0Task1Point3Control.Dock = DockStyle.Fill;
            lab0Task1Point3Control.Location = new Point(18, 18);
            lab0Task1Point3Control.Name = "lab0Task1Point3Control";
            lab0Task1Point3Control.Size = new Size(1144, 498);
            lab0Task1Point3Control.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1180, 760);
            Controls.Add(rootLayout);
            MinimumSize = new Size(1000, 650);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Лабораторные работы — Программирование";
            rootLayout.ResumeLayout(false);
            labMenuPanel.ResumeLayout(false);
            labMenuPanel.PerformLayout();
            taskMenuPanel.ResumeLayout(false);
            taskMenuPanel.PerformLayout();
            headerPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private FlowLayoutPanel labMenuPanel;
        private Label labMenuCaption;
        private Button lab0Button;
        private Button lab1Button;
        private Button lab2Button;
        private Button lab3Button;
        private Button lab4Button;
        private Button lab5Button;
        private Button lab6Button;
        private Button lab7Button;
        private Button lab8Button;
        private FlowLayoutPanel taskMenuPanel;
        private Label taskMenuCaption;
        private Button task1Button;
        private Button task2Button;
        private Panel headerPanel;
        private Label taskTitleLabel;
        private Label taskDescriptionLabel;
        private Panel contentPanel;
        private Controls.Lab0Task1Point3Control lab0Task1Point3Control;
        private Controls.Lab0Task2Point3Control lab0Task2Point3Control;
        private Label emptyLabLabel;
    }
}
