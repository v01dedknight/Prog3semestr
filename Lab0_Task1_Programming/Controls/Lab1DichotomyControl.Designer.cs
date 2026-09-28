namespace Lab0_Task1_Programming.Controls
{
    partial class Lab1DichotomyControl
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
            menuStrip = new MenuStrip();
            calculationMenu = new ToolStripMenuItem();
            calculateMenuItem = new ToolStripMenuItem();
            clearMenuItem = new ToolStripMenuItem();
            graphMenu = new ToolStripMenuItem();
            buildGraphMenuItem = new ToolStripMenuItem();
            mainLayout = new TableLayoutPanel();
            inputGroupBox = new GroupBox();
            inputLayout = new TableLayoutPanel();
            modeLabel = new Label();
            modeComboBox = new ComboBox();
            functionLabel = new Label();
            functionTextBox = new TextBox();
            aLabel = new Label();
            aTextBox = new TextBox();
            bLabel = new Label();
            bTextBox = new TextBox();
            epsilonLabel = new Label();
            epsilonTextBox = new TextBox();
            hintLabel = new Label();
            resultsGroupBox = new GroupBox();
            resultsLayout = new TableLayoutPanel();
            answerLabel = new Label();
            iterationsLabel = new Label();
            warningLabel = new Label();
            calculationsTextBox = new TextBox();
            plotGroupBox = new GroupBox();
            functionPlotPanel = new FunctionPlotPanel();
            menuStrip.SuspendLayout();
            mainLayout.SuspendLayout();
            inputGroupBox.SuspendLayout();
            inputLayout.SuspendLayout();
            resultsGroupBox.SuspendLayout();
            resultsLayout.SuspendLayout();
            plotGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { calculationMenu, graphMenu });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1100, 24);
            menuStrip.TabIndex = 0;
            // 
            // calculationMenu
            // 
            calculationMenu.DropDownItems.AddRange(new ToolStripItem[] { calculateMenuItem, clearMenuItem });
            calculationMenu.Name = "calculationMenu";
            calculationMenu.Size = new Size(59, 20);
            calculationMenu.Text = "Расчёт";
            // 
            // calculateMenuItem
            // 
            calculateMenuItem.Name = "calculateMenuItem";
            calculateMenuItem.ShortcutKeys = Keys.Control | Keys.Enter;
            calculateMenuItem.Size = new Size(205, 22);
            calculateMenuItem.Text = "Рассчитать";
            calculateMenuItem.Click += CalculateMenuItem_Click;
            // 
            // clearMenuItem
            // 
            clearMenuItem.Name = "clearMenuItem";
            clearMenuItem.Size = new Size(205, 22);
            clearMenuItem.Text = "Очистить результат";
            clearMenuItem.Click += ClearMenuItem_Click;
            // 
            // graphMenu
            // 
            graphMenu.DropDownItems.AddRange(new ToolStripItem[] { buildGraphMenuItem });
            graphMenu.Name = "graphMenu";
            graphMenu.Size = new Size(61, 20);
            graphMenu.Text = "График";
            // 
            // buildGraphMenuItem
            // 
            buildGraphMenuItem.Name = "buildGraphMenuItem";
            buildGraphMenuItem.ShortcutKeys = Keys.Control | Keys.G;
            buildGraphMenuItem.Size = new Size(207, 22);
            buildGraphMenuItem.Text = "Построить график";
            buildGraphMenuItem.Click += BuildGraphMenuItem_Click;
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(inputGroupBox, 0, 0);
            mainLayout.Controls.Add(resultsGroupBox, 0, 1);
            mainLayout.Controls.Add(plotGroupBox, 1, 0);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 24);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(8);
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(1100, 576);
            mainLayout.TabIndex = 1;
            // 
            // inputGroupBox
            // 
            inputGroupBox.Controls.Add(inputLayout);
            inputGroupBox.Dock = DockStyle.Fill;
            inputGroupBox.Location = new Point(11, 11);
            inputGroupBox.Name = "inputGroupBox";
            inputGroupBox.Padding = new Padding(10);
            inputGroupBox.Size = new Size(354, 244);
            inputGroupBox.TabIndex = 0;
            inputGroupBox.TabStop = false;
            inputGroupBox.Text = "Входные данные";
            // 
            // inputLayout
            // 
            inputLayout.ColumnCount = 2;
            inputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 94F));
            inputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            inputLayout.Controls.Add(modeLabel, 0, 0);
            inputLayout.Controls.Add(modeComboBox, 1, 0);
            inputLayout.Controls.Add(functionLabel, 0, 1);
            inputLayout.Controls.Add(functionTextBox, 1, 1);
            inputLayout.Controls.Add(aLabel, 0, 2);
            inputLayout.Controls.Add(aTextBox, 1, 2);
            inputLayout.Controls.Add(bLabel, 0, 3);
            inputLayout.Controls.Add(bTextBox, 1, 3);
            inputLayout.Controls.Add(epsilonLabel, 0, 4);
            inputLayout.Controls.Add(epsilonTextBox, 1, 4);
            inputLayout.Controls.Add(hintLabel, 0, 5);
            inputLayout.Dock = DockStyle.Fill;
            inputLayout.Location = new Point(10, 26);
            inputLayout.Name = "inputLayout";
            inputLayout.RowCount = 6;
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            inputLayout.Size = new Size(334, 208);
            inputLayout.TabIndex = 0;
            // 
            // modeLabel
            // 
            modeLabel.Anchor = AnchorStyles.Left;
            modeLabel.AutoSize = true;
            modeLabel.Location = new Point(3, 10);
            modeLabel.Name = "modeLabel";
            modeLabel.Size = new Size(47, 15);
            modeLabel.TabIndex = 0;
            modeLabel.Text = "Режим:";
            // 
            // modeComboBox
            // 
            modeComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            modeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            modeComboBox.FormattingEnabled = true;
            modeComboBox.Items.AddRange(new object[] { "Минимум (по заданию)", "Корень (по брифингу)" });
            modeComboBox.Location = new Point(97, 6);
            modeComboBox.Name = "modeComboBox";
            modeComboBox.Size = new Size(234, 23);
            modeComboBox.TabIndex = 1;
            // 
            // functionLabel
            // 
            functionLabel.Anchor = AnchorStyles.Left;
            functionLabel.AutoSize = true;
            functionLabel.Location = new Point(3, 46);
            functionLabel.Name = "functionLabel";
            functionLabel.Size = new Size(30, 15);
            functionLabel.TabIndex = 2;
            functionLabel.Text = "f(x):";
            // 
            // functionTextBox
            // 
            functionTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            functionTextBox.Location = new Point(97, 42);
            functionTextBox.Name = "functionTextBox";
            functionTextBox.Size = new Size(234, 23);
            functionTextBox.TabIndex = 3;
            functionTextBox.Text = "x^2 - 4*x + 5";
            // 
            // aLabel
            // 
            aLabel.Anchor = AnchorStyles.Left;
            aLabel.AutoSize = true;
            aLabel.Location = new Point(3, 80);
            aLabel.Name = "aLabel";
            aLabel.Size = new Size(26, 15);
            aLabel.TabIndex = 4;
            aLabel.Text = "a =";
            // 
            // aTextBox
            // 
            aTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            aTextBox.Location = new Point(97, 78);
            aTextBox.Name = "aTextBox";
            aTextBox.Size = new Size(234, 23);
            aTextBox.TabIndex = 5;
            aTextBox.Text = "-2";
            // 
            // bLabel
            // 
            bLabel.Anchor = AnchorStyles.Left;
            bLabel.AutoSize = true;
            bLabel.Location = new Point(3, 112);
            bLabel.Name = "bLabel";
            bLabel.Size = new Size(27, 15);
            bLabel.TabIndex = 6;
            bLabel.Text = "b =";
            // 
            // bTextBox
            // 
            bTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            bTextBox.Location = new Point(97, 110);
            bTextBox.Name = "bTextBox";
            bTextBox.Size = new Size(234, 23);
            bTextBox.TabIndex = 7;
            bTextBox.Text = "6";
            // 
            // epsilonLabel
            // 
            epsilonLabel.Anchor = AnchorStyles.Left;
            epsilonLabel.AutoSize = true;
            epsilonLabel.Location = new Point(3, 144);
            epsilonLabel.Name = "epsilonLabel";
            epsilonLabel.Size = new Size(28, 15);
            epsilonLabel.TabIndex = 8;
            epsilonLabel.Text = "e =";
            // 
            // epsilonTextBox
            // 
            epsilonTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            epsilonTextBox.Location = new Point(97, 142);
            epsilonTextBox.Name = "epsilonTextBox";
            epsilonTextBox.Size = new Size(234, 23);
            epsilonTextBox.TabIndex = 9;
            epsilonTextBox.Text = "0,0001";
            // 
            // hintLabel
            // 
            hintLabel.AutoSize = true;
            inputLayout.SetColumnSpan(hintLabel, 2);
            hintLabel.Dock = DockStyle.Fill;
            hintLabel.ForeColor = SystemColors.GrayText;
            hintLabel.Location = new Point(3, 168);
            hintLabel.Name = "hintLabel";
            hintLabel.Padding = new Padding(0, 8, 0, 0);
            hintLabel.Size = new Size(328, 40);
            hintLabel.TabIndex = 10;
            hintLabel.Text = "Поддерживаются +, -, *, /, ^, скобки и функции sin, cos, tan, sqrt, abs, exp, ln/log. Пример: sin(x) + x^2.";
            // 
            // resultsGroupBox
            // 
            resultsGroupBox.Controls.Add(resultsLayout);
            resultsGroupBox.Dock = DockStyle.Fill;
            resultsGroupBox.Location = new Point(11, 261);
            resultsGroupBox.Name = "resultsGroupBox";
            resultsGroupBox.Padding = new Padding(10);
            resultsGroupBox.Size = new Size(354, 304);
            resultsGroupBox.TabIndex = 1;
            resultsGroupBox.TabStop = false;
            resultsGroupBox.Text = "Расчёты и ответ";
            // 
            // resultsLayout
            // 
            resultsLayout.ColumnCount = 1;
            resultsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            resultsLayout.Controls.Add(answerLabel, 0, 0);
            resultsLayout.Controls.Add(iterationsLabel, 0, 1);
            resultsLayout.Controls.Add(warningLabel, 0, 2);
            resultsLayout.Controls.Add(calculationsTextBox, 0, 3);
            resultsLayout.Dock = DockStyle.Fill;
            resultsLayout.Location = new Point(10, 26);
            resultsLayout.Name = "resultsLayout";
            resultsLayout.RowCount = 4;
            resultsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            resultsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            resultsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            resultsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            resultsLayout.Size = new Size(334, 268);
            resultsLayout.TabIndex = 0;
            // 
            // answerLabel
            // 
            answerLabel.Dock = DockStyle.Fill;
            answerLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            answerLabel.Location = new Point(3, 0);
            answerLabel.Name = "answerLabel";
            answerLabel.Size = new Size(328, 42);
            answerLabel.TabIndex = 0;
            answerLabel.Text = "Ответ появится после расчёта.";
            answerLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // iterationsLabel
            // 
            iterationsLabel.Dock = DockStyle.Fill;
            iterationsLabel.Location = new Point(3, 42);
            iterationsLabel.Name = "iterationsLabel";
            iterationsLabel.Size = new Size(328, 28);
            iterationsLabel.TabIndex = 1;
            iterationsLabel.Text = "Итераций: —";
            iterationsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // warningLabel
            // 
            warningLabel.Dock = DockStyle.Fill;
            warningLabel.ForeColor = SystemColors.GrayText;
            warningLabel.Location = new Point(3, 70);
            warningLabel.Name = "warningLabel";
            warningLabel.Size = new Size(328, 54);
            warningLabel.TabIndex = 2;
            warningLabel.Text = "Если на интервале несколько корней/минимумов или разрыв функции, сузьте интервал.";
            // 
            // calculationsTextBox
            // 
            calculationsTextBox.Dock = DockStyle.Fill;
            calculationsTextBox.Location = new Point(3, 127);
            calculationsTextBox.Multiline = true;
            calculationsTextBox.Name = "calculationsTextBox";
            calculationsTextBox.ReadOnly = true;
            calculationsTextBox.ScrollBars = ScrollBars.Both;
            calculationsTextBox.Size = new Size(328, 138);
            calculationsTextBox.TabIndex = 3;
            calculationsTextBox.WordWrap = false;
            // 
            // plotGroupBox
            // 
            mainLayout.SetRowSpan(plotGroupBox, 2);
            plotGroupBox.Controls.Add(functionPlotPanel);
            plotGroupBox.Dock = DockStyle.Fill;
            plotGroupBox.Location = new Point(371, 11);
            plotGroupBox.Name = "plotGroupBox";
            plotGroupBox.Padding = new Padding(10);
            plotGroupBox.Size = new Size(718, 554);
            plotGroupBox.TabIndex = 2;
            plotGroupBox.TabStop = false;
            plotGroupBox.Text = "График функции";
            // 
            // functionPlotPanel
            // 
            functionPlotPanel.Dock = DockStyle.Fill;
            functionPlotPanel.Location = new Point(10, 26);
            functionPlotPanel.Name = "functionPlotPanel";
            functionPlotPanel.Size = new Size(698, 518);
            functionPlotPanel.TabIndex = 0;
            // 
            // Lab1DichotomyControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainLayout);
            Controls.Add(menuStrip);
            Name = "Lab1DichotomyControl";
            Size = new Size(1100, 600);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            mainLayout.ResumeLayout(false);
            inputGroupBox.ResumeLayout(false);
            inputLayout.ResumeLayout(false);
            inputLayout.PerformLayout();
            resultsGroupBox.ResumeLayout(false);
            resultsLayout.ResumeLayout(false);
            resultsLayout.PerformLayout();
            plotGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem calculationMenu;
        private ToolStripMenuItem calculateMenuItem;
        private ToolStripMenuItem clearMenuItem;
        private ToolStripMenuItem graphMenu;
        private ToolStripMenuItem buildGraphMenuItem;
        private TableLayoutPanel mainLayout;
        private GroupBox inputGroupBox;
        private TableLayoutPanel inputLayout;
        private Label modeLabel;
        private ComboBox modeComboBox;
        private Label functionLabel;
        private TextBox functionTextBox;
        private Label aLabel;
        private TextBox aTextBox;
        private Label bLabel;
        private TextBox bTextBox;
        private Label epsilonLabel;
        private TextBox epsilonTextBox;
        private Label hintLabel;
        private GroupBox resultsGroupBox;
        private TableLayoutPanel resultsLayout;
        private Label answerLabel;
        private Label iterationsLabel;
        private Label warningLabel;
        private TextBox calculationsTextBox;
        private GroupBox plotGroupBox;
        private FunctionPlotPanel functionPlotPanel;
    }
}
