namespace Lab0_Task1_Programming.Controls
{
    partial class Lab0Task1Point3Control
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
            densityLabel = new Label();
            densityTextBox = new TextBox();
            radiusLabel = new Label();
            radiusTextBox = new TextBox();
            massLabel = new Label();
            massTextBox = new TextBox();
            calculateButton = new Button();
            resultLabel = new Label();
            formulaLabel = new Label();
            layout.SuspendLayout();
            SuspendLayout();
            // 
            // layout
            // 
            layout.AutoSize = true;
            layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F));
            layout.Controls.Add(densityLabel, 0, 0);
            layout.Controls.Add(densityTextBox, 1, 0);
            layout.Controls.Add(radiusLabel, 0, 1);
            layout.Controls.Add(radiusTextBox, 1, 1);
            layout.Controls.Add(massLabel, 0, 2);
            layout.Controls.Add(massTextBox, 1, 2);
            layout.Controls.Add(calculateButton, 0, 3);
            layout.Controls.Add(resultLabel, 0, 4);
            layout.Controls.Add(formulaLabel, 0, 5);
            layout.Location = new Point(20, 20);
            layout.Name = "layout";
            layout.RowCount = 6;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            layout.Size = new Size(460, 274);
            layout.TabIndex = 0;
            // 
            // densityLabel
            // 
            densityLabel.Anchor = AnchorStyles.Left;
            densityLabel.AutoSize = true;
            densityLabel.Location = new Point(3, 15);
            densityLabel.Name = "densityLabel";
            densityLabel.Size = new Size(141, 15);
            densityLabel.TabIndex = 0;
            densityLabel.Text = "Плотность материала ρ:";
            // 
            // densityTextBox
            // 
            densityTextBox.Anchor = AnchorStyles.Left;
            densityTextBox.Location = new Point(223, 11);
            densityTextBox.Name = "densityTextBox";
            densityTextBox.Size = new Size(180, 23);
            densityTextBox.TabIndex = 1;
            // 
            // radiusLabel
            // 
            radiusLabel.Anchor = AnchorStyles.Left;
            radiusLabel.AutoSize = true;
            radiusLabel.Location = new Point(3, 61);
            radiusLabel.Name = "radiusLabel";
            radiusLabel.Size = new Size(89, 15);
            radiusLabel.TabIndex = 2;
            radiusLabel.Text = "Радиус диска r:";
            // 
            // radiusTextBox
            // 
            radiusTextBox.Anchor = AnchorStyles.Left;
            radiusTextBox.Location = new Point(223, 57);
            radiusTextBox.Name = "radiusTextBox";
            radiusTextBox.Size = new Size(180, 23);
            radiusTextBox.TabIndex = 3;
            // 
            // massLabel
            // 
            massLabel.Anchor = AnchorStyles.Left;
            massLabel.AutoSize = true;
            massLabel.Location = new Point(3, 107);
            massLabel.Name = "massLabel";
            massLabel.Size = new Size(93, 15);
            massLabel.TabIndex = 4;
            massLabel.Text = "Масса диска m:";
            // 
            // massTextBox
            // 
            massTextBox.Anchor = AnchorStyles.Left;
            massTextBox.Location = new Point(223, 103);
            massTextBox.Name = "massTextBox";
            massTextBox.Size = new Size(180, 23);
            massTextBox.TabIndex = 5;
            // 
            // calculateButton
            // 
            calculateButton.Location = new Point(3, 141);
            calculateButton.Name = "calculateButton";
            calculateButton.Size = new Size(125, 32);
            calculateButton.TabIndex = 6;
            calculateButton.Text = "Рассчитать";
            calculateButton.UseVisualStyleBackColor = true;
            calculateButton.Click += CalculateButton_Click;
            layout.SetColumnSpan(calculateButton, 2);
            // 
            // resultLabel
            // 
            resultLabel.Anchor = AnchorStyles.Left;
            resultLabel.AutoSize = true;
            resultLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            resultLabel.Location = new Point(3, 203);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(114, 15);
            resultLabel.TabIndex = 7;
            resultLabel.Text = "Толщина диска h =";
            layout.SetColumnSpan(resultLabel, 2);
            // 
            // formulaLabel
            // 
            formulaLabel.Anchor = AnchorStyles.Left;
            formulaLabel.AutoSize = true;
            formulaLabel.Location = new Point(3, 245);
            formulaLabel.Name = "formulaLabel";
            formulaLabel.Size = new Size(185, 15);
            formulaLabel.TabIndex = 8;
            formulaLabel.Text = "Формула: h = m / (ρ * π * r²)";
            layout.SetColumnSpan(formulaLabel, 2);
            // 
            // Lab0Task1Point3Control
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(layout);
            Name = "Lab0Task1Point3Control";
            Padding = new Padding(20);
            Size = new Size(700, 420);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel layout;
        private Label densityLabel;
        private TextBox densityTextBox;
        private Label radiusLabel;
        private TextBox radiusTextBox;
        private Label massLabel;
        private TextBox massTextBox;
        private Button calculateButton;
        private Label resultLabel;
        private Label formulaLabel;
    }
}
