namespace Lab0_Task1_Programming.Controls
{
    partial class Lab4SortingControl
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
            rootLayout = new TableLayoutPanel();
            menuStrip = new MenuStrip();
            dataToolStripMenuItem = new ToolStripMenuItem();
            generateToolStripMenuItem = new ToolStripMenuItem();
            loadExcelToolStripMenuItem = new ToolStripMenuItem();
            loadGoogleToolStripMenuItem = new ToolStripMenuItem();
            dataSeparatorToolStripMenuItem = new ToolStripSeparator();
            clearToolStripMenuItem = new ToolStripMenuItem();
            sortingToolStripMenuItem = new ToolStripMenuItem();
            startSortToolStripMenuItem = new ToolStripMenuItem();
            stopSortToolStripMenuItem = new ToolStripMenuItem();
            topLayout = new TableLayoutPanel();
            inputGroupBox = new GroupBox();
            valuesDataGridView = new DataGridView();
            valueColumn = new DataGridViewTextBoxColumn();
            rightLayout = new TableLayoutPanel();
            settingsGroupBox = new GroupBox();
            settingsLayout = new TableLayoutPanel();
            countLabel = new Label();
            countNumericUpDown = new NumericUpDown();
            minLabel = new Label();
            minNumericUpDown = new NumericUpDown();
            maxLabel = new Label();
            maxNumericUpDown = new NumericUpDown();
            directionLabel = new Label();
            directionPanel = new FlowLayoutPanel();
            ascendingRadioButton = new RadioButton();
            descendingRadioButton = new RadioButton();
            delayLabel = new Label();
            delayNumericUpDown = new NumericUpDown();
            googleLabel = new Label();
            googleTableTextBox = new TextBox();
            sourceStatusLabel = new Label();
            algorithmResultLayout = new TableLayoutPanel();
            algorithmsGroupBox = new GroupBox();
            algorithmsFlowLayoutPanel = new FlowLayoutPanel();
            bubbleCheckBox = new CheckBox();
            insertionCheckBox = new CheckBox();
            shakerCheckBox = new CheckBox();
            quickCheckBox = new CheckBox();
            bogoCheckBox = new CheckBox();
            bogoIterationsLabel = new Label();
            bogoIterationsNumericUpDown = new NumericUpDown();
            resultsGroupBox = new GroupBox();
            resultsLayout = new TableLayoutPanel();
            fastestLabel = new Label();
            resultsDataGridView = new DataGridView();
            algorithmColumn = new DataGridViewTextBoxColumn();
            timeColumn = new DataGridViewTextBoxColumn();
            iterationColumn = new DataGridViewTextBoxColumn();
            statusColumn = new DataGridViewTextBoxColumn();
            visualizationGroupBox = new GroupBox();
            visualizationFlowLayoutPanel = new FlowLayoutPanel();
            rootLayout.SuspendLayout();
            menuStrip.SuspendLayout();
            topLayout.SuspendLayout();
            inputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)valuesDataGridView).BeginInit();
            rightLayout.SuspendLayout();
            settingsGroupBox.SuspendLayout();
            settingsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)countNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxNumericUpDown).BeginInit();
            directionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)delayNumericUpDown).BeginInit();
            algorithmResultLayout.SuspendLayout();
            algorithmsGroupBox.SuspendLayout();
            algorithmsFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bogoIterationsNumericUpDown).BeginInit();
            resultsGroupBox.SuspendLayout();
            resultsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resultsDataGridView).BeginInit();
            visualizationGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(menuStrip, 0, 0);
            rootLayout.Controls.Add(topLayout, 0, 1);
            rootLayout.Controls.Add(visualizationGroupBox, 0, 2);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Margin = new Padding(0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 3;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 235F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1120, 520);
            rootLayout.TabIndex = 0;
            // 
            // menuStrip
            // 
            menuStrip.Dock = DockStyle.Fill;
            menuStrip.Items.AddRange(new ToolStripItem[] { dataToolStripMenuItem, sortingToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1120, 28);
            menuStrip.TabIndex = 0;
            // 
            // dataToolStripMenuItem
            // 
            dataToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { generateToolStripMenuItem, loadExcelToolStripMenuItem, loadGoogleToolStripMenuItem, dataSeparatorToolStripMenuItem, clearToolStripMenuItem });
            dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            dataToolStripMenuItem.Size = new Size(62, 24);
            dataToolStripMenuItem.Text = "Данные";
            // 
            // generateToolStripMenuItem
            // 
            generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            generateToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.G;
            generateToolStripMenuItem.Size = new Size(246, 22);
            generateToolStripMenuItem.Text = "Сгенерировать";
            generateToolStripMenuItem.Click += GenerateToolStripMenuItem_Click;
            // 
            // loadExcelToolStripMenuItem
            // 
            loadExcelToolStripMenuItem.Name = "loadExcelToolStripMenuItem";
            loadExcelToolStripMenuItem.Size = new Size(246, 22);
            loadExcelToolStripMenuItem.Text = "Загрузить из Excel...";
            loadExcelToolStripMenuItem.Click += LoadExcelToolStripMenuItem_Click;
            // 
            // loadGoogleToolStripMenuItem
            // 
            loadGoogleToolStripMenuItem.Name = "loadGoogleToolStripMenuItem";
            loadGoogleToolStripMenuItem.Size = new Size(246, 22);
            loadGoogleToolStripMenuItem.Text = "Загрузить из Google Table";
            loadGoogleToolStripMenuItem.Click += LoadGoogleToolStripMenuItem_Click;
            // 
            // dataSeparatorToolStripMenuItem
            // 
            dataSeparatorToolStripMenuItem.Name = "dataSeparatorToolStripMenuItem";
            dataSeparatorToolStripMenuItem.Size = new Size(243, 6);
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Delete;
            clearToolStripMenuItem.Size = new Size(246, 22);
            clearToolStripMenuItem.Text = "Очистить";
            clearToolStripMenuItem.Click += ClearToolStripMenuItem_Click;
            // 
            // sortingToolStripMenuItem
            // 
            sortingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { startSortToolStripMenuItem, stopSortToolStripMenuItem });
            sortingToolStripMenuItem.Name = "sortingToolStripMenuItem";
            sortingToolStripMenuItem.Size = new Size(85, 24);
            sortingToolStripMenuItem.Text = "Сортировка";
            // 
            // startSortToolStripMenuItem
            // 
            startSortToolStripMenuItem.Name = "startSortToolStripMenuItem";
            startSortToolStripMenuItem.ShortcutKeys = Keys.F5;
            startSortToolStripMenuItem.Size = new Size(170, 22);
            startSortToolStripMenuItem.Text = "Запустить";
            startSortToolStripMenuItem.Click += StartSortToolStripMenuItem_Click;
            // 
            // stopSortToolStripMenuItem
            // 
            stopSortToolStripMenuItem.Enabled = false;
            stopSortToolStripMenuItem.Name = "stopSortToolStripMenuItem";
            stopSortToolStripMenuItem.Size = new Size(170, 22);
            stopSortToolStripMenuItem.Text = "Остановить";
            stopSortToolStripMenuItem.Click += StopSortToolStripMenuItem_Click;
            // 
            // topLayout
            // 
            topLayout.ColumnCount = 2;
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            topLayout.Controls.Add(inputGroupBox, 0, 0);
            topLayout.Controls.Add(rightLayout, 1, 0);
            topLayout.Dock = DockStyle.Fill;
            topLayout.Location = new Point(3, 31);
            topLayout.Name = "topLayout";
            topLayout.RowCount = 1;
            topLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            topLayout.Size = new Size(1114, 229);
            topLayout.TabIndex = 1;
            // 
            // inputGroupBox
            // 
            inputGroupBox.Controls.Add(valuesDataGridView);
            inputGroupBox.Dock = DockStyle.Fill;
            inputGroupBox.Location = new Point(3, 3);
            inputGroupBox.Name = "inputGroupBox";
            inputGroupBox.Padding = new Padding(8);
            inputGroupBox.Size = new Size(314, 223);
            inputGroupBox.TabIndex = 0;
            inputGroupBox.TabStop = false;
            inputGroupBox.Text = "Входные данные (DataGridView)";
            // 
            // valuesDataGridView
            // 
            valuesDataGridView.AllowUserToDeleteRows = true;
            valuesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            valuesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            valuesDataGridView.Columns.AddRange(new DataGridViewColumn[] { valueColumn });
            valuesDataGridView.Dock = DockStyle.Fill;
            valuesDataGridView.Location = new Point(8, 24);
            valuesDataGridView.Name = "valuesDataGridView";
            valuesDataGridView.RowHeadersVisible = false;
            valuesDataGridView.Size = new Size(298, 191);
            valuesDataGridView.TabIndex = 0;
            valuesDataGridView.DataError += ValuesDataGridView_DataError;
            // 
            // valueColumn
            // 
            valueColumn.HeaderText = "Число";
            valueColumn.Name = "valueColumn";
            valueColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // rightLayout
            // 
            rightLayout.ColumnCount = 2;
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 330F));
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightLayout.Controls.Add(settingsGroupBox, 0, 0);
            rightLayout.Controls.Add(algorithmResultLayout, 1, 0);
            rightLayout.Dock = DockStyle.Fill;
            rightLayout.Location = new Point(323, 3);
            rightLayout.Name = "rightLayout";
            rightLayout.RowCount = 1;
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rightLayout.Size = new Size(788, 223);
            rightLayout.TabIndex = 1;
            // 
            // settingsGroupBox
            // 
            settingsGroupBox.Controls.Add(settingsLayout);
            settingsGroupBox.Dock = DockStyle.Fill;
            settingsGroupBox.Location = new Point(3, 3);
            settingsGroupBox.Name = "settingsGroupBox";
            settingsGroupBox.Size = new Size(324, 217);
            settingsGroupBox.TabIndex = 0;
            settingsGroupBox.TabStop = false;
            settingsGroupBox.Text = "Параметры и источники";
            // 
            // settingsLayout
            // 
            settingsLayout.ColumnCount = 2;
            settingsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
            settingsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            settingsLayout.Controls.Add(countLabel, 0, 0);
            settingsLayout.Controls.Add(countNumericUpDown, 1, 0);
            settingsLayout.Controls.Add(minLabel, 0, 1);
            settingsLayout.Controls.Add(minNumericUpDown, 1, 1);
            settingsLayout.Controls.Add(maxLabel, 0, 2);
            settingsLayout.Controls.Add(maxNumericUpDown, 1, 2);
            settingsLayout.Controls.Add(directionLabel, 0, 3);
            settingsLayout.Controls.Add(directionPanel, 1, 3);
            settingsLayout.Controls.Add(delayLabel, 0, 4);
            settingsLayout.Controls.Add(delayNumericUpDown, 1, 4);
            settingsLayout.Controls.Add(googleLabel, 0, 5);
            settingsLayout.Controls.Add(googleTableTextBox, 1, 5);
            settingsLayout.Controls.Add(sourceStatusLabel, 0, 6);
            settingsLayout.Dock = DockStyle.Fill;
            settingsLayout.Location = new Point(3, 19);
            settingsLayout.Name = "settingsLayout";
            settingsLayout.RowCount = 7;
            settingsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            settingsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            settingsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            settingsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            settingsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            settingsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            settingsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            settingsLayout.Size = new Size(318, 195);
            settingsLayout.TabIndex = 0;
            // 
            // countLabel
            // 
            countLabel.Anchor = AnchorStyles.Left;
            countLabel.AutoSize = true;
            countLabel.Location = new Point(3, 6);
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(74, 15);
            countLabel.TabIndex = 0;
            countLabel.Text = "Количество:";
            // 
            // countNumericUpDown
            // 
            countNumericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            countNumericUpDown.Location = new Point(128, 2);
            countNumericUpDown.Maximum = new decimal(new int[] { 2147483647, 0, 0, 0 });
            countNumericUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            countNumericUpDown.ThousandsSeparator = true;
            countNumericUpDown.Name = "countNumericUpDown";
            countNumericUpDown.Size = new Size(187, 23);
            countNumericUpDown.TabIndex = 1;
            countNumericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // minLabel
            // 
            minLabel.Anchor = AnchorStyles.Left;
            minLabel.AutoSize = true;
            minLabel.Location = new Point(3, 33);
            minLabel.Name = "minLabel";
            minLabel.Size = new Size(32, 15);
            minLabel.TabIndex = 2;
            minLabel.Text = "Мин:";
            // 
            // minNumericUpDown
            // 
            minNumericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            minNumericUpDown.DecimalPlaces = 2;
            minNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            minNumericUpDown.Location = new Point(128, 29);
            minNumericUpDown.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            minNumericUpDown.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            minNumericUpDown.Name = "minNumericUpDown";
            minNumericUpDown.Size = new Size(187, 23);
            minNumericUpDown.TabIndex = 3;
            minNumericUpDown.Value = new decimal(new int[] { 50, 0, 0, int.MinValue });
            // 
            // maxLabel
            // 
            maxLabel.Anchor = AnchorStyles.Left;
            maxLabel.AutoSize = true;
            maxLabel.Location = new Point(3, 60);
            maxLabel.Name = "maxLabel";
            maxLabel.Size = new Size(38, 15);
            maxLabel.TabIndex = 4;
            maxLabel.Text = "Макс:";
            // 
            // maxNumericUpDown
            // 
            maxNumericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            maxNumericUpDown.DecimalPlaces = 2;
            maxNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            maxNumericUpDown.Location = new Point(128, 56);
            maxNumericUpDown.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            maxNumericUpDown.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            maxNumericUpDown.Name = "maxNumericUpDown";
            maxNumericUpDown.Size = new Size(187, 23);
            maxNumericUpDown.TabIndex = 5;
            maxNumericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // directionLabel
            // 
            directionLabel.Anchor = AnchorStyles.Left;
            directionLabel.AutoSize = true;
            directionLabel.Location = new Point(3, 89);
            directionLabel.Name = "directionLabel";
            directionLabel.Size = new Size(83, 15);
            directionLabel.TabIndex = 6;
            directionLabel.Text = "Направление:";
            // 
            // directionPanel
            // 
            directionPanel.Controls.Add(ascendingRadioButton);
            directionPanel.Controls.Add(descendingRadioButton);
            directionPanel.Dock = DockStyle.Fill;
            directionPanel.Location = new Point(125, 81);
            directionPanel.Margin = new Padding(0);
            directionPanel.Name = "directionPanel";
            directionPanel.Size = new Size(193, 31);
            directionPanel.TabIndex = 7;
            directionPanel.WrapContents = false;
            // 
            // ascendingRadioButton
            // 
            ascendingRadioButton.AutoSize = true;
            ascendingRadioButton.Checked = true;
            ascendingRadioButton.Location = new Point(3, 5);
            ascendingRadioButton.Margin = new Padding(3, 5, 8, 3);
            ascendingRadioButton.Name = "ascendingRadioButton";
            ascendingRadioButton.Size = new Size(101, 19);
            ascendingRadioButton.TabIndex = 0;
            ascendingRadioButton.TabStop = true;
            ascendingRadioButton.Text = "Возрастание";
            ascendingRadioButton.UseVisualStyleBackColor = true;
            // 
            // descendingRadioButton
            // 
            descendingRadioButton.AutoSize = true;
            descendingRadioButton.Location = new Point(115, 5);
            descendingRadioButton.Margin = new Padding(3, 5, 3, 3);
            descendingRadioButton.Name = "descendingRadioButton";
            descendingRadioButton.Size = new Size(89, 19);
            descendingRadioButton.TabIndex = 1;
            descendingRadioButton.Text = "Убывание";
            descendingRadioButton.UseVisualStyleBackColor = true;
            // 
            // delayLabel
            // 
            delayLabel.Anchor = AnchorStyles.Left;
            delayLabel.AutoSize = true;
            delayLabel.Location = new Point(3, 118);
            delayLabel.Name = "delayLabel";
            delayLabel.Size = new Size(93, 15);
            delayLabel.TabIndex = 8;
            delayLabel.Text = "Задержка, мс:";
            // 
            // delayNumericUpDown
            // 
            delayNumericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delayNumericUpDown.Location = new Point(128, 114);
            delayNumericUpDown.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            delayNumericUpDown.Name = "delayNumericUpDown";
            delayNumericUpDown.Size = new Size(187, 23);
            delayNumericUpDown.TabIndex = 9;
            delayNumericUpDown.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // googleLabel
            // 
            googleLabel.Anchor = AnchorStyles.Left;
            googleLabel.AutoSize = true;
            googleLabel.Location = new Point(3, 145);
            googleLabel.Name = "googleLabel";
            googleLabel.Size = new Size(111, 15);
            googleLabel.TabIndex = 10;
            googleLabel.Text = "Google Table URL:";
            // 
            // googleTableTextBox
            // 
            googleTableTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            googleTableTextBox.Location = new Point(128, 141);
            googleTableTextBox.Name = "googleTableTextBox";
            googleTableTextBox.PlaceholderText = "https://docs.google.com/...";
            googleTableTextBox.Size = new Size(187, 23);
            googleTableTextBox.TabIndex = 11;
            // 
            // sourceStatusLabel
            // 
            sourceStatusLabel.AutoEllipsis = true;
            settingsLayout.SetColumnSpan(sourceStatusLabel, 2);
            sourceStatusLabel.Dock = DockStyle.Fill;
            sourceStatusLabel.Location = new Point(3, 168);
            sourceStatusLabel.Name = "sourceStatusLabel";
            sourceStatusLabel.Padding = new Padding(0, 4, 0, 0);
            sourceStatusLabel.Size = new Size(312, 27);
            sourceStatusLabel.TabIndex = 12;
            sourceStatusLabel.Text = "Введите данные вручную, сгенерируйте или загрузите.";
            // 
            // algorithmResultLayout
            // 
            algorithmResultLayout.ColumnCount = 1;
            algorithmResultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            algorithmResultLayout.Controls.Add(algorithmsGroupBox, 0, 0);
            algorithmResultLayout.Controls.Add(resultsGroupBox, 0, 1);
            algorithmResultLayout.Dock = DockStyle.Fill;
            algorithmResultLayout.Location = new Point(333, 3);
            algorithmResultLayout.Name = "algorithmResultLayout";
            algorithmResultLayout.RowCount = 2;
            algorithmResultLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
            algorithmResultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            algorithmResultLayout.Size = new Size(452, 217);
            algorithmResultLayout.TabIndex = 1;
            // 
            // algorithmsGroupBox
            // 
            algorithmsGroupBox.Controls.Add(algorithmsFlowLayoutPanel);
            algorithmsGroupBox.Dock = DockStyle.Fill;
            algorithmsGroupBox.Location = new Point(3, 3);
            algorithmsGroupBox.Name = "algorithmsGroupBox";
            algorithmsGroupBox.Size = new Size(446, 99);
            algorithmsGroupBox.TabIndex = 0;
            algorithmsGroupBox.TabStop = false;
            algorithmsGroupBox.Text = "Алгоритмы (можно выбрать несколько)";
            // 
            // algorithmsFlowLayoutPanel
            // 
            algorithmsFlowLayoutPanel.Controls.Add(bubbleCheckBox);
            algorithmsFlowLayoutPanel.Controls.Add(insertionCheckBox);
            algorithmsFlowLayoutPanel.Controls.Add(shakerCheckBox);
            algorithmsFlowLayoutPanel.Controls.Add(quickCheckBox);
            algorithmsFlowLayoutPanel.Controls.Add(bogoCheckBox);
            algorithmsFlowLayoutPanel.Controls.Add(bogoIterationsLabel);
            algorithmsFlowLayoutPanel.Controls.Add(bogoIterationsNumericUpDown);
            algorithmsFlowLayoutPanel.Dock = DockStyle.Fill;
            algorithmsFlowLayoutPanel.Location = new Point(3, 19);
            algorithmsFlowLayoutPanel.Name = "algorithmsFlowLayoutPanel";
            algorithmsFlowLayoutPanel.Padding = new Padding(3, 4, 3, 3);
            algorithmsFlowLayoutPanel.Size = new Size(440, 77);
            algorithmsFlowLayoutPanel.TabIndex = 0;
            // 
            // bubbleCheckBox
            // 
            bubbleCheckBox.AutoSize = true;
            bubbleCheckBox.Checked = true;
            bubbleCheckBox.CheckState = CheckState.Checked;
            bubbleCheckBox.Location = new Point(6, 7);
            bubbleCheckBox.Name = "bubbleCheckBox";
            bubbleCheckBox.Size = new Size(98, 19);
            bubbleCheckBox.TabIndex = 0;
            bubbleCheckBox.Text = "Пузырьковая";
            bubbleCheckBox.UseVisualStyleBackColor = true;
            // 
            // insertionCheckBox
            // 
            insertionCheckBox.AutoSize = true;
            insertionCheckBox.Checked = true;
            insertionCheckBox.CheckState = CheckState.Checked;
            insertionCheckBox.Location = new Point(110, 7);
            insertionCheckBox.Name = "insertionCheckBox";
            insertionCheckBox.Size = new Size(86, 19);
            insertionCheckBox.TabIndex = 1;
            insertionCheckBox.Text = "Вставками";
            insertionCheckBox.UseVisualStyleBackColor = true;
            // 
            // shakerCheckBox
            // 
            shakerCheckBox.AutoSize = true;
            shakerCheckBox.Checked = true;
            shakerCheckBox.CheckState = CheckState.Checked;
            shakerCheckBox.Location = new Point(202, 7);
            shakerCheckBox.Name = "shakerCheckBox";
            shakerCheckBox.Size = new Size(86, 19);
            shakerCheckBox.TabIndex = 2;
            shakerCheckBox.Text = "Шейкерная";
            shakerCheckBox.UseVisualStyleBackColor = true;
            // 
            // quickCheckBox
            // 
            quickCheckBox.AutoSize = true;
            quickCheckBox.Checked = true;
            quickCheckBox.CheckState = CheckState.Checked;
            quickCheckBox.Location = new Point(294, 7);
            quickCheckBox.Name = "quickCheckBox";
            quickCheckBox.Size = new Size(69, 19);
            quickCheckBox.TabIndex = 3;
            quickCheckBox.Text = "Быстрая";
            quickCheckBox.UseVisualStyleBackColor = true;
            // 
            // bogoCheckBox
            // 
            bogoCheckBox.AutoSize = true;
            bogoCheckBox.Location = new Point(369, 7);
            bogoCheckBox.Name = "bogoCheckBox";
            bogoCheckBox.Size = new Size(59, 19);
            bogoCheckBox.TabIndex = 4;
            bogoCheckBox.Text = "BOGO";
            bogoCheckBox.UseVisualStyleBackColor = true;
            // 
            // bogoIterationsLabel
            // 
            bogoIterationsLabel.AutoSize = true;
            bogoIterationsLabel.Margin = new Padding(6, 7, 3, 3);
            bogoIterationsLabel.Name = "bogoIterationsLabel";
            bogoIterationsLabel.Size = new Size(112, 15);
            bogoIterationsLabel.TabIndex = 5;
            bogoIterationsLabel.Text = "Итераций BOGO:";
            // 
            // bogoIterationsNumericUpDown
            // 
            bogoIterationsNumericUpDown.Maximum = new decimal(new int[] { 2147483647, 0, 0, 0 });
            bogoIterationsNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            bogoIterationsNumericUpDown.Name = "bogoIterationsNumericUpDown";
            bogoIterationsNumericUpDown.Size = new Size(110, 23);
            bogoIterationsNumericUpDown.TabIndex = 6;
            bogoIterationsNumericUpDown.ThousandsSeparator = true;
            bogoIterationsNumericUpDown.Value = new decimal(new int[] { 100000, 0, 0, 0 });
            // 
            // resultsGroupBox
            // 
            resultsGroupBox.Controls.Add(resultsLayout);
            resultsGroupBox.Dock = DockStyle.Fill;
            resultsGroupBox.Location = new Point(3, 108);
            resultsGroupBox.Name = "resultsGroupBox";
            resultsGroupBox.Size = new Size(446, 106);
            resultsGroupBox.TabIndex = 1;
            resultsGroupBox.TabStop = false;
            resultsGroupBox.Text = "Результаты сортировки";
            // 
            // resultsLayout
            // 
            resultsLayout.ColumnCount = 1;
            resultsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            resultsLayout.Controls.Add(fastestLabel, 0, 0);
            resultsLayout.Controls.Add(resultsDataGridView, 0, 1);
            resultsLayout.Dock = DockStyle.Fill;
            resultsLayout.Location = new Point(3, 19);
            resultsLayout.Name = "resultsLayout";
            resultsLayout.RowCount = 2;
            resultsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            resultsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            resultsLayout.Size = new Size(440, 84);
            resultsLayout.TabIndex = 0;
            // 
            // fastestLabel
            // 
            fastestLabel.Dock = DockStyle.Fill;
            fastestLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            fastestLabel.Location = new Point(3, 0);
            fastestLabel.Name = "fastestLabel";
            fastestLabel.Size = new Size(434, 25);
            fastestLabel.TabIndex = 0;
            fastestLabel.Text = "Самый быстрый: —";
            fastestLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // resultsDataGridView
            // 
            resultsDataGridView.AllowUserToAddRows = false;
            resultsDataGridView.AllowUserToDeleteRows = false;
            resultsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            resultsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resultsDataGridView.Columns.AddRange(new DataGridViewColumn[] { algorithmColumn, timeColumn, iterationColumn, statusColumn });
            resultsDataGridView.Dock = DockStyle.Fill;
            resultsDataGridView.Location = new Point(3, 28);
            resultsDataGridView.Name = "resultsDataGridView";
            resultsDataGridView.ReadOnly = true;
            resultsDataGridView.RowHeadersVisible = false;
            resultsDataGridView.Size = new Size(434, 53);
            resultsDataGridView.TabIndex = 1;
            // 
            // algorithmColumn
            // 
            algorithmColumn.FillWeight = 34F;
            algorithmColumn.HeaderText = "Алгоритм";
            algorithmColumn.Name = "algorithmColumn";
            algorithmColumn.ReadOnly = true;
            // 
            // timeColumn
            // 
            timeColumn.FillWeight = 22F;
            timeColumn.HeaderText = "Время, мс";
            timeColumn.Name = "timeColumn";
            timeColumn.ReadOnly = true;
            // 
            // iterationColumn
            // 
            iterationColumn.FillWeight = 22F;
            iterationColumn.HeaderText = "Итерации";
            iterationColumn.Name = "iterationColumn";
            iterationColumn.ReadOnly = true;
            // 
            // statusColumn
            // 
            statusColumn.FillWeight = 22F;
            statusColumn.HeaderText = "Статус";
            statusColumn.Name = "statusColumn";
            statusColumn.ReadOnly = true;
            // 
            // visualizationGroupBox
            // 
            visualizationGroupBox.Controls.Add(visualizationFlowLayoutPanel);
            visualizationGroupBox.Dock = DockStyle.Fill;
            visualizationGroupBox.Location = new Point(3, 266);
            visualizationGroupBox.Name = "visualizationGroupBox";
            visualizationGroupBox.Padding = new Padding(8);
            visualizationGroupBox.Size = new Size(1114, 251);
            visualizationGroupBox.TabIndex = 2;
            visualizationGroupBox.TabStop = false;
            visualizationGroupBox.Text = "Одновременная визуализация выбранных алгоритмов";
            // 
            // visualizationFlowLayoutPanel
            // 
            visualizationFlowLayoutPanel.AutoScroll = true;
            visualizationFlowLayoutPanel.Dock = DockStyle.Fill;
            visualizationFlowLayoutPanel.Location = new Point(8, 24);
            visualizationFlowLayoutPanel.Name = "visualizationFlowLayoutPanel";
            visualizationFlowLayoutPanel.Padding = new Padding(3);
            visualizationFlowLayoutPanel.Size = new Size(1098, 219);
            visualizationFlowLayoutPanel.TabIndex = 0;
            visualizationFlowLayoutPanel.WrapContents = false;
            // 
            // Lab4SortingControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "Lab4SortingControl";
            Size = new Size(1120, 520);
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            topLayout.ResumeLayout(false);
            inputGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)valuesDataGridView).EndInit();
            rightLayout.ResumeLayout(false);
            settingsGroupBox.ResumeLayout(false);
            settingsLayout.ResumeLayout(false);
            settingsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)countNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)minNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxNumericUpDown).EndInit();
            directionPanel.ResumeLayout(false);
            directionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)delayNumericUpDown).EndInit();
            algorithmResultLayout.ResumeLayout(false);
            algorithmsGroupBox.ResumeLayout(false);
            algorithmsFlowLayoutPanel.ResumeLayout(false);
            algorithmsFlowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bogoIterationsNumericUpDown).EndInit();
            resultsGroupBox.ResumeLayout(false);
            resultsLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)resultsDataGridView).EndInit();
            visualizationGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private MenuStrip menuStrip;
        private ToolStripMenuItem dataToolStripMenuItem;
        private ToolStripMenuItem generateToolStripMenuItem;
        private ToolStripMenuItem loadExcelToolStripMenuItem;
        private ToolStripMenuItem loadGoogleToolStripMenuItem;
        private ToolStripSeparator dataSeparatorToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ToolStripMenuItem sortingToolStripMenuItem;
        private ToolStripMenuItem startSortToolStripMenuItem;
        private ToolStripMenuItem stopSortToolStripMenuItem;
        private TableLayoutPanel topLayout;
        private GroupBox inputGroupBox;
        private DataGridView valuesDataGridView;
        private DataGridViewTextBoxColumn valueColumn;
        private TableLayoutPanel rightLayout;
        private GroupBox settingsGroupBox;
        private TableLayoutPanel settingsLayout;
        private Label countLabel;
        private NumericUpDown countNumericUpDown;
        private Label minLabel;
        private NumericUpDown minNumericUpDown;
        private Label maxLabel;
        private NumericUpDown maxNumericUpDown;
        private Label directionLabel;
        private FlowLayoutPanel directionPanel;
        private RadioButton ascendingRadioButton;
        private RadioButton descendingRadioButton;
        private Label delayLabel;
        private NumericUpDown delayNumericUpDown;
        private Label googleLabel;
        private TextBox googleTableTextBox;
        private Label sourceStatusLabel;
        private TableLayoutPanel algorithmResultLayout;
        private GroupBox algorithmsGroupBox;
        private FlowLayoutPanel algorithmsFlowLayoutPanel;
        private CheckBox bubbleCheckBox;
        private CheckBox insertionCheckBox;
        private CheckBox shakerCheckBox;
        private CheckBox quickCheckBox;
        private CheckBox bogoCheckBox;
        private Label bogoIterationsLabel;
        private NumericUpDown bogoIterationsNumericUpDown;
        private GroupBox resultsGroupBox;
        private TableLayoutPanel resultsLayout;
        private Label fastestLabel;
        private DataGridView resultsDataGridView;
        private DataGridViewTextBoxColumn algorithmColumn;
        private DataGridViewTextBoxColumn timeColumn;
        private DataGridViewTextBoxColumn iterationColumn;
        private DataGridViewTextBoxColumn statusColumn;
        private GroupBox visualizationGroupBox;
        private FlowLayoutPanel visualizationFlowLayoutPanel;
    }
}
