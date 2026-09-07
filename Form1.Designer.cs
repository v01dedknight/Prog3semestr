namespace Lab0_Task1_Programming
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// true if managed resources should be disposed; otherwise, false.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            labelDensity = new Label();
            labelRadius = new Label();
            labelMass = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            buttonCalculate = new Button();
            labelResult = new Label();

            SuspendLayout();

            // 
            // labelDensity
            // 
            labelDensity.AutoSize = true;
            labelDensity.Location = new Point(119, 93);
            labelDensity.Name = "labelDensity";
            labelDensity.Size = new Size(141, 15);
            labelDensity.TabIndex = 0;
            labelDensity.Text = "Плотность материала ρ:";

            // 
            // labelRadius
            // 
            labelRadius.AutoSize = true;
            labelRadius.Location = new Point(119, 145);
            labelRadius.Name = "labelRadius";
            labelRadius.Size = new Size(89, 15);
            labelRadius.TabIndex = 1;
            labelRadius.Text = "Радиус диска r:";

            // 
            // labelMass
            // 
            labelMass.AutoSize = true;
            labelMass.Location = new Point(119, 195);
            labelMass.Name = "labelMass";
            labelMass.Size = new Size(93, 15);
            labelMass.TabIndex = 2;
            labelMass.Text = "Масса диска m:";

            // 
            // textBox1
            // 
            textBox1.Location = new Point(269, 90);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(130, 23);
            textBox1.TabIndex = 3;

            // 
            // textBox2
            // 
            textBox2.Location = new Point(269, 142);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(130, 23);
            textBox2.TabIndex = 4;

            // 
            // textBox3
            // 
            textBox3.Location = new Point(269, 192);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(130, 23);
            textBox3.TabIndex = 5;

            // 
            // buttonCalculate
            // 
            buttonCalculate.Location = new Point(119, 245);
            buttonCalculate.Name = "buttonCalculate";
            buttonCalculate.Size = new Size(110, 30);
            buttonCalculate.TabIndex = 6;
            buttonCalculate.Text = "Рассчитать";
            buttonCalculate.UseVisualStyleBackColor = true;
            buttonCalculate.Click += buttonCalculate_Click;

            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Location = new Point(269, 253);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(116, 15);
            labelResult.TabIndex = 7;
            labelResult.Text = "Толщина диска h =";

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 360);

            Controls.Add(labelResult);
            Controls.Add(buttonCalculate);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(labelMass);
            Controls.Add(labelRadius);
            Controls.Add(labelDensity);

            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Расчет толщины диска";

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelDensity;
        private Label labelRadius;
        private Label labelMass;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button buttonCalculate;
        private Label labelResult;
    }
}