#nullable enable

namespace Lab0_Task1_Programming.Controls
{
    partial class QuadraticEquationControl
    {
        private System.ComponentModel.IContainer? components = null;
        private TableLayoutPanel rootLayout = null!;
        private MenuStrip menuStrip = null!;
        private ToolStripMenuItem calculationMenu = null!;
        private ToolStripMenuItem solveMenuItem = null!;
        private ToolStripMenuItem clearMenuItem = null!;
        private ToolStripMenuItem examplesMenu = null!;
        private ToolStripMenuItem example1MenuItem = null!;
        private ToolStripMenuItem example2MenuItem = null!;
        private ToolStripMenuItem example3MenuItem = null!;
        private ToolStripMenuItem example4MenuItem = null!;
        private ToolStripMenuItem example5MenuItem = null!;
        private TableLayoutPanel mainLayout = null!;
        private GroupBox inputGroup = null!;
        private FlowLayoutPanel inputFlow = null!;
        private Label aLabel = null!;
        private Label bLabel = null!;
        private Label cLabel = null!;
        private TextBox aTextBox = null!;
        private TextBox bTextBox = null!;
        private TextBox cTextBox = null!;
        private Label inputHintLabel = null!;
        private Label statusLabel = null!;
        private TableLayoutPanel resultAndGraphLayout = null!;
        private TableLayoutPanel resultLayout = null!;
        private GroupBox discriminantGroup = null!;
        private GroupBox vietaGroup = null!;
        private TextBox discriminantTextBox = null!;
        private TextBox vietaTextBox = null!;
        private GroupBox plotGroup = null!;
        private QuadraticPlotPanel quadraticPlotPanel = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            menuStrip = new MenuStrip();
            calculationMenu = new ToolStripMenuItem();
            solveMenuItem = new ToolStripMenuItem();
            clearMenuItem = new ToolStripMenuItem();
            examplesMenu = new ToolStripMenuItem();
            example1MenuItem = new ToolStripMenuItem();
            example2MenuItem = new ToolStripMenuItem();
            example3MenuItem = new ToolStripMenuItem();
            example4MenuItem = new ToolStripMenuItem();
            example5MenuItem = new ToolStripMenuItem();
            mainLayout = new TableLayoutPanel();
            inputGroup = new GroupBox();
            inputFlow = new FlowLayoutPanel();
            aLabel = new Label();
            bLabel = new Label();
            cLabel = new Label();
            aTextBox = new TextBox();
            bTextBox = new TextBox();
            cTextBox = new TextBox();
            inputHintLabel = new Label();
            statusLabel = new Label();
            resultAndGraphLayout = new TableLayoutPanel();
            resultLayout = new TableLayoutPanel();
            discriminantGroup = new GroupBox();
            vietaGroup = new GroupBox();
            discriminantTextBox = new TextBox();
            vietaTextBox = new TextBox();
            plotGroup = new GroupBox();
            quadraticPlotPanel = new QuadraticPlotPanel();
            SuspendLayout();

            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Name = "rootLayout";
            rootLayout.Margin = new Padding(0);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.RowCount = 2;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(menuStrip, 0, 0);
            rootLayout.Controls.Add(mainLayout, 0, 1);

            menuStrip.Name = "menuStrip";
            menuStrip.Dock = DockStyle.Fill;
            menuStrip.Items.AddRange(new ToolStripItem[] { calculationMenu, examplesMenu });
            calculationMenu.Name = "calculationMenu";
            calculationMenu.Text = "Расчёт";
            calculationMenu.DropDownItems.AddRange(new ToolStripItem[] { solveMenuItem, clearMenuItem });
            solveMenuItem.Name = "solveMenuItem";
            solveMenuItem.Text = "Решить";
            solveMenuItem.ShortcutKeys = Keys.Control | Keys.Enter;
            solveMenuItem.Click += SolveMenuItem_Click;
            clearMenuItem.Name = "clearMenuItem";
            clearMenuItem.Text = "Очистить";
            clearMenuItem.Click += ClearMenuItem_Click;

            examplesMenu.Name = "examplesMenu";
            examplesMenu.Text = "Примеры";
            examplesMenu.DropDownItems.AddRange(new ToolStripItem[] {
                example1MenuItem, example2MenuItem, example3MenuItem,
                example4MenuItem, example5MenuItem
            });
            example1MenuItem.Text = "Два корня (1; -5; 6)";
            example1MenuItem.Click += Example1MenuItem_Click;
            example2MenuItem.Text = "Один корень (1; -2; 1)";
            example2MenuItem.Click += Example2MenuItem_Click;
            example3MenuItem.Text = "Нет действительных корней (1; 2; 5)";
            example3MenuItem.Click += Example3MenuItem_Click;
            example4MenuItem.Text = "Дробные коэффициенты (0,5; -1,5; 1)";
            example4MenuItem.Click += Example4MenuItem_Click;
            example5MenuItem.Text = "Линейный случай (0; 2; -4)";
            example5MenuItem.Click += Example5MenuItem_Click;

            mainLayout.Name = "mainLayout";
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(6);
            mainLayout.Margin = new Padding(0);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(inputGroup, 0, 0);
            mainLayout.Controls.Add(resultAndGraphLayout, 0, 1);

            inputGroup.Text = "Коэффициенты квадратного уравнения ax² + bx + c = 0";
            inputGroup.Name = "inputGroup";
            inputGroup.Dock = DockStyle.Fill;
            inputGroup.Padding = new Padding(12, 10, 12, 7);
            inputGroup.Margin = new Padding(4);
            inputGroup.Controls.Add(inputFlow);
            inputGroup.Controls.Add(inputHintLabel);
            inputGroup.Controls.Add(statusLabel);

            inputFlow.Name = "inputFlow";
            inputFlow.Dock = DockStyle.Top;
            inputFlow.Height = 34;
            inputFlow.WrapContents = false;
            inputFlow.AutoScroll = true;
            inputFlow.Margin = new Padding(0);
            inputFlow.Controls.Add(aLabel);
            inputFlow.Controls.Add(aTextBox);
            inputFlow.Controls.Add(bLabel);
            inputFlow.Controls.Add(bTextBox);
            inputFlow.Controls.Add(cLabel);
            inputFlow.Controls.Add(cTextBox);

            aLabel.Name = "aLabel";
            aLabel.Text = "a =";
            aLabel.Width = 34;
            aLabel.Height = 25;
            aLabel.TextAlign = ContentAlignment.MiddleRight;
            aLabel.Margin = new Padding(0, 2, 4, 0);
            bLabel.Name = "bLabel";
            bLabel.Text = "b =";
            bLabel.Width = 34;
            bLabel.Height = 25;
            bLabel.TextAlign = ContentAlignment.MiddleRight;
            bLabel.Margin = new Padding(12, 2, 4, 0);
            cLabel.Name = "cLabel";
            cLabel.Text = "c =";
            cLabel.Width = 34;
            cLabel.Height = 25;
            cLabel.TextAlign = ContentAlignment.MiddleRight;
            cLabel.Margin = new Padding(12, 2, 4, 0);

            aTextBox.Name = "aTextBox";
            aTextBox.Text = "1";
            aTextBox.Width = 125;
            aTextBox.Margin = new Padding(0, 2, 0, 0);
            aTextBox.KeyDown += CoefficientTextBox_KeyDown;
            bTextBox.Name = "bTextBox";
            bTextBox.Text = "-5";
            bTextBox.Width = 125;
            bTextBox.Margin = new Padding(0, 2, 0, 0);
            bTextBox.KeyDown += CoefficientTextBox_KeyDown;
            cTextBox.Name = "cTextBox";
            cTextBox.Text = "6";
            cTextBox.Width = 125;
            cTextBox.Margin = new Padding(0, 2, 0, 0);
            cTextBox.KeyDown += CoefficientTextBox_KeyDown;

            inputHintLabel.Name = "inputHintLabel";
            inputHintLabel.Text = "Введите числа (допускаются точка, запятая, отрицательные и дробные значения). Enter — решить.";
            inputHintLabel.Dock = DockStyle.Top;
            inputHintLabel.Height = 23;
            inputHintLabel.ForeColor = Color.DimGray;

            statusLabel.Name = "statusLabel";
            statusLabel.Text = "Готово к расчёту";
            statusLabel.Dock = DockStyle.Bottom;
            statusLabel.Height = 24;
            statusLabel.AutoEllipsis = true;
            statusLabel.ForeColor = Color.FromArgb(35, 90, 117);

            resultAndGraphLayout.Name = "resultAndGraphLayout";
            resultAndGraphLayout.Dock = DockStyle.Fill;
            resultAndGraphLayout.Margin = new Padding(0);
            resultAndGraphLayout.ColumnCount = 2;
            resultAndGraphLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            resultAndGraphLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57F));
            resultAndGraphLayout.RowCount = 1;
            resultAndGraphLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            resultAndGraphLayout.Controls.Add(resultLayout, 0, 0);
            resultAndGraphLayout.Controls.Add(plotGroup, 1, 0);

            resultLayout.Name = "resultLayout";
            resultLayout.Dock = DockStyle.Fill;
            resultLayout.Margin = new Padding(0);
            resultLayout.ColumnCount = 1;
            resultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            resultLayout.RowCount = 2;
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            resultLayout.Controls.Add(discriminantGroup, 0, 0);
            resultLayout.Controls.Add(vietaGroup, 0, 1);

            discriminantGroup.Name = "discriminantGroup";
            discriminantGroup.Text = "Способ 1. Решение через дискриминант";
            discriminantGroup.Dock = DockStyle.Fill;
            discriminantGroup.Margin = new Padding(4);
            discriminantGroup.Padding = new Padding(8);
            discriminantGroup.Controls.Add(discriminantTextBox);
            discriminantTextBox.Name = "discriminantTextBox";
            discriminantTextBox.Dock = DockStyle.Fill;
            discriminantTextBox.Multiline = true;
            discriminantTextBox.ReadOnly = true;
            discriminantTextBox.ScrollBars = ScrollBars.Vertical;
            discriminantTextBox.Font = new Font("Consolas", 10F);
            discriminantTextBox.BackColor = Color.White;
            discriminantTextBox.WordWrap = true;

            vietaGroup.Name = "vietaGroup";
            vietaGroup.Text = "Способ 2. Проверка по теореме Виета";
            vietaGroup.Dock = DockStyle.Fill;
            vietaGroup.Margin = new Padding(4);
            vietaGroup.Padding = new Padding(8);
            vietaGroup.Controls.Add(vietaTextBox);
            vietaTextBox.Name = "vietaTextBox";
            vietaTextBox.Dock = DockStyle.Fill;
            vietaTextBox.Multiline = true;
            vietaTextBox.ReadOnly = true;
            vietaTextBox.ScrollBars = ScrollBars.Vertical;
            vietaTextBox.Font = new Font("Consolas", 10F);
            vietaTextBox.BackColor = Color.White;
            vietaTextBox.WordWrap = true;

            plotGroup.Name = "plotGroup";
            plotGroup.Text = "График функции y = ax² + bx + c";
            plotGroup.Dock = DockStyle.Fill;
            plotGroup.Margin = new Padding(4);
            plotGroup.Padding = new Padding(8);
            plotGroup.Controls.Add(quadraticPlotPanel);
            quadraticPlotPanel.Name = "quadraticPlotPanel";
            quadraticPlotPanel.Dock = DockStyle.Fill;
            quadraticPlotPanel.BackColor = Color.White;

            Controls.Add(rootLayout);
            Name = "QuadraticEquationControl";
            Size = new Size(1144, 498);
            MinimumSize = new Size(760, 390);
            ResumeLayout(false);
        }
    }
}
