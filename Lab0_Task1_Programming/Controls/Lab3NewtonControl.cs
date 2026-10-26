using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Lab0_Task1_Programming.Controls
{
    public partial class Lab3NewtonControl : UserControl
    {
        private readonly List<(string Name, NewtonExtremumSolver.Result Result)> _results = new();
        private Func<double, double>? _lastFunction;
        private double _lastA;
        private double _lastB;
        private bool _updatingGrid;

        public Lab3NewtonControl()
        {
            InitializeComponent();
            modeComboBox.SelectedIndex = 0;
            UpdateStepControls();
        }

        private bool ReadInputs(out MathExpression? expression, out double a, out double b,
            out double epsilon, out double? initial)
        {
            expression = null; a = b = epsilon = 0; initial = null;
            try
            {
                expression = new MathExpression(functionTextBox.Text);
                if (!TryNumber(aTextBox.Text, out a) || !double.IsFinite(a))
                    throw new ArgumentException("Введите корректное вещественное число a.");
                if (!TryNumber(bTextBox.Text, out b) || !double.IsFinite(b))
                    throw new ArgumentException("Введите корректное вещественное число b.");
                if (!TryNumber(epsilonTextBox.Text, out epsilon) || !double.IsFinite(epsilon))
                    throw new ArgumentException("Введите корректную точность e.");
                if (!(a < b)) throw new ArgumentException("Необходимо a < b.");
                if (epsilon <= 0 || epsilon >= b - a)
                    throw new ArgumentException("Точность должна удовлетворять 0 < e < b - a.");
                if (epsilon < 1e-10)
                    throw new ArgumentException("При численных производных используйте e не меньше 1e-10.");
                if (!string.IsNullOrWhiteSpace(initialTextBox.Text))
                {
                    if (!TryNumber(initialTextBox.Text, out double value) || !double.IsFinite(value))
                        throw new ArgumentException("Начальная точка x0 должна быть числом.");
                    if (value < a || value > b)
                        throw new ArgumentException("Начальная точка x0 должна находиться внутри [a, b].");
                    initial = value;
                }
                return true;
            }
            catch (Exception exception) when (exception is FormatException or ArgumentException)
            {
                MessageBox.Show(this, exception.Message, "Ошибка входных данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private static bool TryNumber(string text, out double value)
        {
            text = text.Trim().Replace(',', '.');
            return double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        private void CalculateMenuItem_Click(object? sender, EventArgs e)
        {
            if (!ReadInputs(out MathExpression? expression, out double a, out double b,
                out double epsilon, out double? initial) || expression == null) return;
            try
            {
                _results.Clear();
                _lastFunction = expression.Evaluate;
                _lastA = a;
                _lastB = b;
                string minDiagnostic = "";
                string maxDiagnostic = "";
                if (modeComboBox.SelectedIndex != 1)
                {
                    var min = NewtonExtremumSolver.Find(_lastFunction, a, b, epsilon, true, initial, out minDiagnostic);
                    if (min != null) _results.Add(("Минимум", min));
                }
                if (modeComboBox.SelectedIndex != 0)
                {
                    var max = NewtonExtremumSolver.Find(_lastFunction, a, b, epsilon, false, initial, out maxDiagnostic);
                    if (max != null) _results.Add(("Максимум", max));
                }

                var output = new StringBuilder();
                output.AppendLine("Метод Ньютона: f'(x) = 0");
                output.AppendLine($"Отрезок: [{a:G12}; {b:G12}], e = {epsilon:G12}");
                foreach (var (name, result) in _results)
                {
                    output.AppendLine();
                    output.AppendLine($"{name}: x ≈ {ResultPrecisionFormatter.Format(result.X, epsilonTextBox.Text)}");
                    output.AppendLine($"f(x) ≈ {ResultPrecisionFormatter.Format(result.Y, epsilonTextBox.Text)}");
                    output.AppendLine($"Итераций: {Math.Max(0, result.History.Count - 1)}");
                }
                if (!string.IsNullOrWhiteSpace(minDiagnostic)) output.AppendLine(minDiagnostic);
                if (!string.IsNullOrWhiteSpace(maxDiagnostic)) output.AppendLine(maxDiagnostic);
                if (!string.IsNullOrWhiteSpace(expression.InterpretationNote))
                    output.AppendLine(expression.InterpretationNote);
                if (_results.Count == 0)
                {
                    output.AppendLine("Метод Ньютона не обнаружил внутренний стационарный экстремум.");
                    try
                    {
                        double ya = _lastFunction(a);
                        double yb = _lastFunction(b);
                        if (double.IsFinite(ya) && double.IsFinite(yb))
                        {
                            bool lookingForMaximum = modeComboBox.SelectedIndex == 1;
                            double bound = lookingForMaximum ? (ya >= yb ? a : b) : (ya <= yb ? a : b);
                            output.AppendLine($"Значение на лучшей границе: x = {ResultPrecisionFormatter.Format(bound, epsilonTextBox.Text)}, " +
                                $"f(x) = {ResultPrecisionFormatter.Format(_lastFunction(bound), epsilonTextBox.Text)}.");
                            output.AppendLine("Это проверка границ, не найденный методом Ньютона внутренний экстремум.");
                        }
                    }
                    catch { /* Ошибка области определения уже сообщена в диагностике. */ }
                }
                answerTextBox.Text = output.ToString();

                seriesComboBox.Items.Clear();
                foreach (var (name, _) in _results) seriesComboBox.Items.Add(name);
                if (_results.Count > 0)
                {
                    seriesComboBox.SelectedIndex = 0;
                }
                else
                {
                    calculationsTextBox.Clear();
                    historyGrid.Rows.Clear();
                    newtonPlotPanel.SetPlot(_lastFunction, a, b);
                    UpdateStepControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Не удалось выполнить расчёт: " + ex.Message,
                    "Метод Ньютона", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildGraphMenuItem_Click(object? sender, EventArgs e)
        {
            if (!ReadInputs(out MathExpression? expr, out double a, out double b,
                out _, out _) || expr == null) return;
            _lastFunction = expr.Evaluate;
            _lastA = a;
            _lastB = b;
            _results.Clear();
            seriesComboBox.Items.Clear();
            answerTextBox.Text = "График построен. Для поиска экстремума выберите Расчёт / Рассчитать.";
            calculationsTextBox.Clear();
            historyGrid.Rows.Clear();
            newtonPlotPanel.SetPlot(_lastFunction, a, b);
            UpdateStepControls();
        }

        private void ClearMenuItem_Click(object? sender, EventArgs e)
        {
            _results.Clear();
            _lastFunction = null;
            seriesComboBox.Items.Clear();
            answerTextBox.Text = "Результаты очищены.";
            calculationsTextBox.Clear();
            historyGrid.Rows.Clear();
            newtonPlotPanel.ClearPlot();
            UpdateStepControls();
        }

        private void DisplaySeries(int index)
        {
            if (_lastFunction == null || index < 0 || index >= _results.Count)
            {
                UpdateStepControls();
                return;
            }
            var (name, result) = _results[index];
            var log = new StringBuilder();
            log.AppendLine($"{name}: x(n+1) = x(n) - f'(x(n)) / f''(x(n))");
            log.AppendLine("Защитный шаг по половине интервала применяется только при неудачном шаге Ньютона.");
            log.AppendLine();
            _updatingGrid = true;
            historyGrid.Rows.Clear();
            foreach (var step in result.History)
            {
                historyGrid.Rows.Add(step.Number, step.X.ToString("G12", CultureInfo.CurrentCulture),
                    step.Y.ToString("G12", CultureInfo.CurrentCulture),
                    step.First.ToString("G9", CultureInfo.CurrentCulture),
                    step.Second.ToString("G9", CultureInfo.CurrentCulture), step.Action);
                log.AppendLine($"{step.Number,3}: x={step.X:G14}; f(x)={step.Y:G14}; " +
                    $"f'={step.First:G9}; f''={step.Second:G9}; [{step.Left:G9}; {step.Right:G9}]; {step.Action}");
            }
            _updatingGrid = false;
            calculationsTextBox.Text = log.ToString();
            stepTrackBar.Maximum = Math.Max(0, result.History.Count - 1);
            stepTrackBar.Value = stepTrackBar.Maximum;
            ShowStep(stepTrackBar.Value);
        }

        private void ShowStep(int step)
        {
            int index = seriesComboBox.SelectedIndex;
            if (_lastFunction == null || index < 0 || index >= _results.Count)
            {
                UpdateStepControls();
                return;
            }
            var (name, result) = _results[index];
            int current = Math.Clamp(step, 0, result.History.Count - 1);
            newtonPlotPanel.SetPlot(_lastFunction, _lastA, _lastB,
                result.History, current, name);
            stepStatusLabel.Text = $"Шаг {current} / {result.History.Count - 1}";
            if (historyGrid.Rows.Count > current && !_updatingGrid)
            {
                _updatingGrid = true;
                historyGrid.ClearSelection();
                historyGrid.Rows[current].Selected = true;
                if (historyGrid.DisplayedRowCount(false) > 0)
                    historyGrid.FirstDisplayedScrollingRowIndex = Math.Max(0, current - 2);
                _updatingGrid = false;
            }
            UpdateStepControls();
        }

        private void UpdateStepControls()
        {
            bool available = _results.Count > 0 && seriesComboBox.SelectedIndex >= 0;
            stepTrackBar.Enabled = available;
            firstStepMenuItem.Enabled = available && stepTrackBar.Value > 0;
            previousStepMenuItem.Enabled = available && stepTrackBar.Value > 0;
            nextStepMenuItem.Enabled = available && stepTrackBar.Value < stepTrackBar.Maximum;
            lastStepMenuItem.Enabled = available && stepTrackBar.Value < stepTrackBar.Maximum;
            if (!available) stepStatusLabel.Text = "Шаг 0 / 0";
        }

        private void SeriesComboBox_SelectedIndexChanged(object? sender, EventArgs e) =>
            DisplaySeries(seriesComboBox.SelectedIndex);

        private void StepTrackBar_ValueChanged(object? sender, EventArgs e) =>
            ShowStep(stepTrackBar.Value);

        private void HistoryGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (_updatingGrid || historyGrid.SelectedRows.Count == 0 || _results.Count == 0) return;
            int selected = historyGrid.SelectedRows[0].Index;
            if (selected >= stepTrackBar.Minimum && selected <= stepTrackBar.Maximum)
                stepTrackBar.Value = selected;
        }

        private void FirstStepMenuItem_Click(object? sender, EventArgs e) => stepTrackBar.Value = 0;
        private void PreviousStepMenuItem_Click(object? sender, EventArgs e) =>
            stepTrackBar.Value = Math.Max(stepTrackBar.Minimum, stepTrackBar.Value - 1);
        private void NextStepMenuItem_Click(object? sender, EventArgs e) =>
            stepTrackBar.Value = Math.Min(stepTrackBar.Maximum, stepTrackBar.Value + 1);
        private void LastStepMenuItem_Click(object? sender, EventArgs e) => stepTrackBar.Value = stepTrackBar.Maximum;
    }
}
