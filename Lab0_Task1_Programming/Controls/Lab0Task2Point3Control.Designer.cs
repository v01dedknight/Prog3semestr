namespace Lab0_Task1_Programming.Controls
{
    partial class Lab0Task2Point3Control
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            layout = new TableLayoutPanel();
            executeButton = new Button();
            fileLabel = new Label();
            resultTextBox = new TextBox();
            layout.SuspendLayout();
            SuspendLayout();
            // 
            // layout
            // 
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(executeButton, 0, 0);
            layout.Controls.Add(fileLabel, 0, 1);
            layout.Controls.Add(resultTextBox, 0, 2);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(20, 20);
            layout.Name = "layout";
            layout.RowCount = 3;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.Size = new Size(660, 380);
            layout.TabIndex = 0;
            // 
            // executeButton
            // 
            executeButton.Anchor = AnchorStyles.Left;
            executeButton.Location = new Point(3, 9);
            executeButton.Name = "executeButton";
            executeButton.Size = new Size(155, 34);
            executeButton.TabIndex = 0;
            executeButton.Text = "Выполнить задание";
            executeButton.UseVisualStyleBackColor = true;
            executeButton.Click += ExecuteButton_Click;
            // 
            // fileLabel
            // 
            fileLabel.Anchor = AnchorStyles.Left;
            fileLabel.AutoSize = true;
            fileLabel.Location = new Point(3, 64);
            fileLabel.Name = "fileLabel";
            fileLabel.Size = new Size(218, 15);
            fileLabel.TabIndex = 1;
            fileLabel.Text = "Данные сохраняются в файл people.csv";
            // 
            // resultTextBox
            // 
            resultTextBox.Dock = DockStyle.Fill;
            resultTextBox.Location = new Point(3, 95);
            resultTextBox.Multiline = true;
            resultTextBox.Name = "resultTextBox";
            resultTextBox.ReadOnly = true;
            resultTextBox.ScrollBars = ScrollBars.Vertical;
            resultTextBox.Size = new Size(654, 282);
            resultTextBox.TabIndex = 2;
            resultTextBox.Text = "Нажмите «Выполнить задание».";
            // 
            // Lab0Task2Point3Control
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "Lab0Task2Point3Control";
            Padding = new Padding(20);
            Size = new Size(700, 420);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layout;
        private Button executeButton;
        private Label fileLabel;
        private TextBox resultTextBox;
    }
}
