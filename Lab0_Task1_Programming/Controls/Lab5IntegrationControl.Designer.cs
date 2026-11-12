#nullable enable

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab0_Task1_Programming.Controls
{
    partial class Lab5IntegrationControl
    {
        private System.ComponentModel.IContainer? components = null;
        private TableLayoutPanel rootLayout = null!;
        private MenuStrip integrationMenu = null!;
        private ToolStripMenuItem calculateMenu = null!;
        private ToolStripMenuItem calculateMenuItem = null!;
        private ToolStripMenuItem graphMenuItem = null!;
        private ToolStripMenuItem enlargedGraphMenuItem = null!;
        private ToolStripMenuItem clearMenuItem = null!;
        private ToolStripMenuItem generateMenuItem = null!;
        private ToolStripMenuItem exampleMenu = null!;
        private ToolStripMenuItem examplePolynomial = null!;
        private ToolStripMenuItem exampleSine = null!;
        private ToolStripMenuItem exampleExponential = null!;
        private FlowLayoutPanel inputPanel = null!;
        private FlowLayoutPanel methodsPanel = null!;
        private TextBox formulaTextBox = null!;
        private TextBox aTextBox = null!;
        private TextBox bTextBox = null!;
        private TextBox nTextBox = null!;
        private TextBox epsilonTextBox = null!;
        private CheckBox leftCheckBox = null!;
        private CheckBox rightCheckBox = null!;
        private CheckBox middleCheckBox = null!;
        private CheckBox trapezoidCheckBox = null!;
        private CheckBox simpsonCheckBox = null!;
        private Label statusLabel = null!;
        private TableLayoutPanel bodyLayout = null!;
        private TableLayoutPanel resultsLayout = null!;
        private TableLayoutPanel graphLayout = null!;
        private GroupBox resultGroupBox = null!;
        private GroupBox historyGroupBox = null!;
        private GroupBox graphGroupBox = null!;
        private DataGridView resultsGrid = null!;
        private DataGridView historyGrid = null!;
        private IntegralPlotPanel plotPanel = null!;
        private TrackBar historyTrackBar = null!;
        private Label stepLabel = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private static Label InputLabel(string text, int width)
        {
            return new Label
            {
                Text = text, TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false, Size = new Size(width, 25),
                Margin = new Padding(3, 2, 3, 2)
            };
        }

        private static TextBox InputTextBox(string name, string defaultValue, int width)
        {
            return new TextBox
            {
                Name = name, Text = defaultValue, Width = width,
                Margin = new Padding(1, 2, 12, 2)
            };
        }

        private static CheckBox MethodCheckBox(string name, string text, bool selected, int width)
        {
            return new CheckBox
            {
                Name = name, Text = text, Checked = selected,
                AutoSize = false, Size = new Size(width, 26),
                Margin = new Padding(3, 1, 8, 1)
            };
        }

        private static void SetupGrid(DataGridView grid)
        {
            grid.Dock = DockStyle.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.ColumnHeadersHeight = 29;
            grid.RowTemplate.Height = 25;
        }

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            integrationMenu = new MenuStrip();
            calculateMenu = new ToolStripMenuItem("Расчёт");
            calculateMenuItem = new ToolStripMenuItem("Рассчитать выбранные методы");
            graphMenuItem = new ToolStripMenuItem("Построить график");
            enlargedGraphMenuItem = new ToolStripMenuItem("Увеличить график (отдельное окно)");
            clearMenuItem = new ToolStripMenuItem("Очистить результаты");
            generateMenuItem = new ToolStripMenuItem("Сгенерировать данные");
            exampleMenu = new ToolStripMenuItem("Примеры");
            examplePolynomial = new ToolStripMenuItem("x² на [0; 1]");
            exampleSine = new ToolStripMenuItem("sin(x) на [0; π]");
            exampleExponential = new ToolStripMenuItem("exp(x) на [0; 1]");
            inputPanel = new FlowLayoutPanel();
            methodsPanel = new FlowLayoutPanel();
            formulaTextBox = InputTextBox("formulaTextBox", "x^2", 275);
            aTextBox = InputTextBox("aTextBox", "0", 65);
            bTextBox = InputTextBox("bTextBox", "1", 65);
            nTextBox = InputTextBox("nTextBox", "8", 65);
            epsilonTextBox = InputTextBox("epsilonTextBox", "0,0001", 90);
            leftCheckBox = MethodCheckBox("leftCheckBox", "Левые прямоугольники", true, 178);
            rightCheckBox = MethodCheckBox("rightCheckBox", "Правые прямоугольники", true, 185);
            middleCheckBox = MethodCheckBox("middleCheckBox", "Средние прямоугольники", true, 189);
            trapezoidCheckBox = MethodCheckBox("trapezoidCheckBox", "Трапеции", true, 103);
            simpsonCheckBox = MethodCheckBox("simpsonCheckBox", "Симпсон", true, 104);
            statusLabel = new Label();
            bodyLayout = new TableLayoutPanel();
            resultsLayout = new TableLayoutPanel();
            graphLayout = new TableLayoutPanel();
            resultGroupBox = new GroupBox();
            historyGroupBox = new GroupBox();
            graphGroupBox = new GroupBox();
            resultsGrid = new DataGridView();
            historyGrid = new DataGridView();
            plotPanel = new IntegralPlotPanel();
            historyTrackBar = new TrackBar();
            stepLabel = new Label();

            SuspendLayout();
            rootLayout.SuspendLayout();
            inputPanel.SuspendLayout();
            methodsPanel.SuspendLayout();
            bodyLayout.SuspendLayout();
            resultsLayout.SuspendLayout();
            graphLayout.SuspendLayout();

            integrationMenu.Items.AddRange(new ToolStripItem[] { calculateMenu, exampleMenu });
            integrationMenu.Dock = DockStyle.Fill;
            integrationMenu.GripStyle = ToolStripGripStyle.Hidden;
            calculateMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                calculateMenuItem, graphMenuItem, enlargedGraphMenuItem, generateMenuItem, clearMenuItem
            });
            calculateMenuItem.Click += CalculateMenuItem_Click;
            graphMenuItem.Click += GraphMenuItem_Click;
            enlargedGraphMenuItem.Click += EnlargedGraphMenuItem_Click;
            clearMenuItem.Click += ClearMenuItem_Click;
            generateMenuItem.Click += GenerateMenuItem_Click;
            calculateMenuItem.ShortcutKeys = Keys.Control | Keys.Enter;
            graphMenuItem.ShortcutKeys = Keys.Control | Keys.G;
            exampleMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                examplePolynomial, exampleSine, exampleExponential
            });
            examplePolynomial.Click += PolynomialExample_Click;
            exampleSine.Click += SineExample_Click;
            exampleExponential.Click += ExponentialExample_Click;

            rootLayout.Name = "rootLayout";
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Padding = new Padding(6, 3, 6, 4);
            rootLayout.Margin = new Padding(0);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.RowCount = 5;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(integrationMenu, 0, 0);
            rootLayout.Controls.Add(inputPanel, 0, 1);
            rootLayout.Controls.Add(methodsPanel, 0, 2);
            rootLayout.Controls.Add(statusLabel, 0, 3);
            rootLayout.Controls.Add(bodyLayout, 0, 4);

            inputPanel.Name = "inputPanel";
            inputPanel.Dock = DockStyle.Fill;
            inputPanel.WrapContents = false;
            inputPanel.AutoScroll = true;
            inputPanel.Padding = new Padding(2, 5, 0, 0);
            inputPanel.Controls.Add(InputLabel("f(x):", 37));
            inputPanel.Controls.Add(formulaTextBox);
            inputPanel.Controls.Add(InputLabel("a:", 18));
            inputPanel.Controls.Add(aTextBox);
            inputPanel.Controls.Add(InputLabel("b:", 18));
            inputPanel.Controls.Add(bTextBox);
            inputPanel.Controls.Add(InputLabel("n:", 18));
            inputPanel.Controls.Add(nTextBox);
            inputPanel.Controls.Add(InputLabel("e:", 17));
            inputPanel.Controls.Add(epsilonTextBox);
            foreach (TextBox input in new[] { formulaTextBox, aTextBox, bTextBox, nTextBox, epsilonTextBox })
                input.KeyDown += Input_KeyDown;

            methodsPanel.Name = "methodsPanel";
            methodsPanel.Dock = DockStyle.Fill;
            methodsPanel.WrapContents = false;
            methodsPanel.AutoScroll = true;
            methodsPanel.Padding = new Padding(2, 4, 0, 0);
            methodsPanel.Controls.Add(InputLabel("Методы:", 59));
            methodsPanel.Controls.Add(leftCheckBox);
            methodsPanel.Controls.Add(rightCheckBox);
            methodsPanel.Controls.Add(middleCheckBox);
            methodsPanel.Controls.Add(trapezoidCheckBox);
            methodsPanel.Controls.Add(simpsonCheckBox);
            foreach (CheckBox item in new[]
                { leftCheckBox, rightCheckBox, middleCheckBox, trapezoidCheckBox, simpsonCheckBox })
                item.CheckedChanged += MethodCheckBox_CheckedChanged;

            statusLabel.Name = "statusLabel";
            statusLabel.Text = "Выберите один или несколько методов. n — начальное число разбиений, e — требуемая точность.";
            statusLabel.Dock = DockStyle.Fill;
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            statusLabel.ForeColor = Color.FromArgb(65, 82, 107);
            statusLabel.Padding = new Padding(8, 1, 8, 0);
            statusLabel.AutoEllipsis = true;

            bodyLayout.Name = "bodyLayout";
            bodyLayout.Dock = DockStyle.Fill;
            bodyLayout.Margin = new Padding(2, 0, 2, 0);
            bodyLayout.ColumnCount = 2;
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49F));
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51F));
            bodyLayout.RowCount = 1;
            bodyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            bodyLayout.Controls.Add(resultsLayout, 0, 0);
            bodyLayout.Controls.Add(graphLayout, 1, 0);

            resultsLayout.Name = "resultsLayout";
            resultsLayout.Dock = DockStyle.Fill;
            resultsLayout.Margin = new Padding(0, 0, 6, 0);
            resultsLayout.ColumnCount = 1;
            resultsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            resultsLayout.RowCount = 2;
            resultsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            resultsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            resultsLayout.Controls.Add(resultGroupBox, 0, 0);
            resultsLayout.Controls.Add(historyGroupBox, 0, 1);

            resultGroupBox.Name = "resultGroupBox";
            resultGroupBox.Text = "Результаты выбранных методов (выберите строку)";
            resultGroupBox.Dock = DockStyle.Fill;
            resultGroupBox.Padding = new Padding(7, 5, 7, 7);
            resultGroupBox.Controls.Add(resultsGrid);
            historyGroupBox.Name = "historyGroupBox";
            historyGroupBox.Text = "История уточнения n (выберите шаг)";
            historyGroupBox.Dock = DockStyle.Fill;
            historyGroupBox.Padding = new Padding(7, 5, 7, 7);
            historyGroupBox.Controls.Add(historyGrid);

            SetupGrid(resultsGrid);
            resultsGrid.Name = "resultsGrid";
            resultsGrid.Columns.Add("method", "Метод");
            resultsGrid.Columns.Add("n", "n");
            resultsGrid.Columns.Add("answer", "Интеграл");
            resultsGrid.Columns.Add("error", "Оценка");
            resultsGrid.Columns.Add("state", "Точность");
            resultsGrid.Columns[0].FillWeight = 165;
            resultsGrid.Columns[1].FillWeight = 54;
            resultsGrid.Columns[2].FillWeight = 102;
            resultsGrid.Columns[3].FillWeight = 90;
            resultsGrid.Columns[4].FillWeight = 82;
            resultsGrid.SelectionChanged += ResultsGrid_SelectionChanged;

            SetupGrid(historyGrid);
            historyGrid.Name = "historyGrid";
            historyGrid.Columns.Add("step", "Шаг");
            historyGrid.Columns.Add("partitions", "Разбиений n");
            historyGrid.Columns.Add("value", "Значение интеграла");
            historyGrid.Columns.Add("estimate", "Оценка погрешности");
            historyGrid.Columns[0].FillWeight = 45;
            historyGrid.Columns[1].FillWeight = 85;
            historyGrid.Columns[2].FillWeight = 135;
            historyGrid.Columns[3].FillWeight = 125;
            historyGrid.SelectionChanged += HistoryGrid_SelectionChanged;

            graphLayout.Name = "graphLayout";
            graphLayout.Dock = DockStyle.Fill;
            graphLayout.Margin = new Padding(5, 0, 0, 0);
            graphLayout.ColumnCount = 1;
            graphLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            graphLayout.RowCount = 3;
            graphLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            graphLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            graphLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            graphLayout.Controls.Add(graphGroupBox, 0, 0);
            graphLayout.Controls.Add(historyTrackBar, 0, 1);
            graphLayout.Controls.Add(stepLabel, 0, 2);

            graphGroupBox.Name = "graphGroupBox";
            graphGroupBox.Text = "Синяя линия — f(x); зелёная область — разбиения";
            graphGroupBox.Dock = DockStyle.Fill;
            graphGroupBox.Padding = new Padding(4, 4, 4, 4);
            graphGroupBox.Controls.Add(plotPanel);
            plotPanel.Name = "plotPanel";
            plotPanel.Dock = DockStyle.Fill;

            historyTrackBar.Name = "historyTrackBar";
            historyTrackBar.Dock = DockStyle.Fill;
            historyTrackBar.Minimum = 0;
            historyTrackBar.Maximum = 0;
            historyTrackBar.Value = 0;
            historyTrackBar.TickStyle = TickStyle.None;
            historyTrackBar.Enabled = false;
            historyTrackBar.ValueChanged += HistoryTrackBar_ValueChanged;
            stepLabel.Name = "stepLabel";
            stepLabel.Text = "Шаги уточнения будут доступны после расчёта.";
            stepLabel.TextAlign = ContentAlignment.MiddleCenter;
            stepLabel.Dock = DockStyle.Fill;
            stepLabel.AutoEllipsis = true;

            Name = "Lab5IntegrationControl";
            AutoScaleMode = AutoScaleMode.Font;
            Size = new Size(1144, 498);
            BackColor = Color.FromArgb(249, 250, 252);
            Controls.Add(rootLayout);
            rootLayout.ResumeLayout(false);
            inputPanel.ResumeLayout(false);
            inputPanel.PerformLayout();
            methodsPanel.ResumeLayout(false);
            bodyLayout.ResumeLayout(false);
            resultsLayout.ResumeLayout(false);
            graphLayout.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
