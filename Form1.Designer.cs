namespace Lab0_Task1_Programming
{
    partial class labelDensity
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            labelRadius = new Label();
            labelMass = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            buttonCalculate = new Button();
            labelResult = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(119, 93);
            label1.Name = "label1";
            label1.Size = new Size(141, 15);
            label1.TabIndex = 0;
            label1.Text = "Плотность материала ρ:";
            label1.Click += label1_Click;
            // 
            // labelRadius
            // 
            labelRadius.AutoSize = true;
            labelRadius.Location = new Point(119, 145);
            labelRadius.Name = "labelRadius";
            labelRadius.Size = new Size(89, 15);
            labelRadius.TabIndex = 1;
            labelRadius.Text = "Радиус диска r:";
            labelRadius.Click += label2_Click;
            // 
            // labelMass
            // 
            labelMass.AutoSize = true;
            labelMass.Location = new Point(119, 195);
            labelMass.Name = "labelMass";
            labelMass.Size = new Size(93, 15);
            labelMass.TabIndex = 2;
            labelMass.Text = "Масса диска m:";
            labelMass.Click += label3_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(269, 93);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(269, 137);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 4;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(269, 187);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 5;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // buttonCalculate
            // 
            buttonCalculate.Location = new Point(119, 243);
            buttonCalculate.Name = "buttonCalculate";
            buttonCalculate.Size = new Size(89, 23);
            buttonCalculate.TabIndex = 6;
            buttonCalculate.Text = "Рассчитать";
            buttonCalculate.UseVisualStyleBackColor = true;
            buttonCalculate.Click += button1_Click;
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Location = new Point(269, 251);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(107, 15);
            labelResult.TabIndex = 7;
            labelResult.Text = "____________________";
            labelResult.Click += label4_Click;
            // 
            // labelDensity
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelResult);
            Controls.Add(buttonCalculate);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(labelMass);
            Controls.Add(labelRadius);
            Controls.Add(label1);
            Name = "labelDensity";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label labelRadius;
        private Label labelMass;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button buttonCalculate;
        private Label labelResult;
    }
}
