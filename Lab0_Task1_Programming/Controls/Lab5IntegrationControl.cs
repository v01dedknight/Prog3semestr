using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Lab0_Task1_Programming.Controls
{
    // Laboratory work 5: numerical evaluation of a definite integral.
    // Inputs are TextBox fields; multiple quadrature methods can be selected by CheckBox.
    public partial class Lab5IntegrationControl : UserControl
    {
        private readonly Random _random = new Random();
        private readonly List<IntegrationRun> _runs = new();
        private MathExpression? _expression;
        private double _a;
        private double _b;
        private IntegrationRun? _selectedRun;
        private bool _changingSelection;

        public Lab5IntegrationControl()
        {
            InitializeComponent();
        }

        private void CalculateMenuItem_Click(object? sender, EventArgs e) => Calculate();

        private void GraphMenuItem_Click(object? sender, EventArgs e)
        {
            if (!TryReadFunctionAndBounds(out var expression, out double a, out double b)) return;
            ClearResults();
            _expression = expression;
            _a = a;
            _b = b;
            plotPanel.Display(expression.Evaluate, a, b, null, 0);
            statusLabel.Text = "График построен. Для разбиений запустите «Расчёт → Рассчитать выбранные методы»." +
                (string.IsNullOrWhiteSpace(expression.InterpretationNote) ? "" : " " + expression.InterpretationNote);
        }

        private void EnlargedGraphMenuItem_Click(object? sender, EventArgs e)
        {
            if (_expression == null)
            {
                if (!TryReadFunctionAndBounds(out var expression, out double a, out double b)) return;
                _expression = expression;
                _a = a;
                _b = b;
            }
            using var window = new Form
            {
                Text = "Лабораторная №5 — Увеличенный график интегрирования",
                StartPosition = FormStartPosition.CenterParent,
                MinimumSize = new System.Drawing.Size(700, 500),
                Size = new System.Drawing.Size(1120, 740)
            };
            var largePanel = new IntegralPlotPanel { Dock = DockStyle.Fill };
            window.Controls.Add(largePanel);
            IntegrationStep? step = _selectedRun == null || _selectedRun.Steps.Count == 0
                ? null
                : _selectedRun.Steps[Math.Clamp(historyTrackBar.Value, 0, _selectedRun.Steps.Count - 1)];
            largePanel.Display(_expression.Evaluate, _a, _b,
                step == null ? null : _selectedRun?.Method, step?.Partitions ?? 0);
            window.ShowDialog(this);
        }

        private void MethodCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (_runs.Count > 0)
                statusLabel.Text = "Выбор методов изменён. Для обновления таблицы и графика снова запустите расчёт.";
        }

        private void ClearMenuItem_Click(object? sender, EventArgs e)
        {
            ClearResults();
            statusLabel.Text = "Результаты очищены. Введите f(x), границы, n и e и запустите расчёт.";
        }

        private void GenerateMenuItem_Click(object? sender, EventArgs e)
        {
            // Ready-to-evaluate random smooth functions, all in the supported parser syntax.
            int a = _random.Next(-3, 2);
            int b = a + _random.Next(2, 6);
            string function = _random.Next(5) switch
            {
                0 => $"{_random.Next(1, 5)}*x^2+({_random.Next(-5, 6)})*x+({_random.Next(-5, 6)})",
                1 => $"sin(x)+({_random.Next(1, 5)})*x",
                2 => $"cos(x)+({_random.Next(1, 4)})*x^2",
                3 => $"exp(x/{_random.Next(2, 5)})",
                _ => $"x^3+({_random.Next(1, 5)})*x^2+({_random.Next(-5, 6)})"
            };
            formulaTextBox.Text = function;
            aTextBox.Text = a.ToString(CultureInfo.InvariantCulture);
            bTextBox.Text = b.ToString(CultureInfo.InvariantCulture);
            nTextBox.Text = (_random.Next(2, 9) * 2).ToString(CultureInfo.InvariantCulture);
            epsilonTextBox.Text = new[] { "0,01", "0,001", "0,0001" }[_random.Next(3)];
            ClearResults();
            statusLabel.Text = "Случайные корректные данные сгенерированы. Запустите расчёт в меню.";
        }

        private void PolynomialExample_Click(object? sender, EventArgs e) =>
            SetExample("x^2", "0", "1", "8", "0,0001");

        private void SineExample_Click(object? sender, EventArgs e) =>
            SetExample("sin(x)", "0", "3,141592653589793", "8", "0,0001");

        private void ExponentialExample_Click(object? sender, EventArgs e) =>
            SetExample("exp(x)", "0", "1", "8", "0,0001");

        private void SetExample(string function, string a, string b, string n, string e)
        {
            formulaTextBox.Text = function;
            aTextBox.Text = a;
            bTextBox.Text = b;
            nTextBox.Text = n;
            epsilonTextBox.Text = e;
            Calculate();
        }

        private void Input_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Calculate();
            }
        }

        private static bool TryReal(string text, out double number)
        {
            return double.TryParse(text.Trim().Replace(',', '.'), NumberStyles.Float,
                CultureInfo.InvariantCulture, out number) && double.IsFinite(number);
        }

        private bool TryReadFunctionAndBounds(out MathExpression expression, out double a, out double b)
        {
            expression = null!;
            a = b = 0;
            try
            {
                expression = new MathExpression(formulaTextBox.Text);
                if (!TryReal(aTextBox.Text, out a))
                    throw new FormatException("Некорректная левая граница a. Введите вещественное число.");
                if (!TryReal(bTextBox.Text, out b))
                    throw new FormatException("Некорректная правая граница b. Введите вещественное число.");
                if (!double.IsFinite(b - a))
                    throw new FormatException("Слишком большой диапазон интегрирования.");
                return true;
            }
            catch (FormatException ex)
            {
                statusLabel.Text = ex.Message;
                return false;
            }
        }

        private List<IntegrationMethod> SelectedMethods()
        {
            var selected = new List<IntegrationMethod>();
            if (leftCheckBox.Checked) selected.Add(IntegrationMethod.LeftRectangles);
            if (rightCheckBox.Checked) selected.Add(IntegrationMethod.RightRectangles);
            if (middleCheckBox.Checked) selected.Add(IntegrationMethod.MidpointRectangles);
            if (trapezoidCheckBox.Checked) selected.Add(IntegrationMethod.Trapezoids);
            if (simpsonCheckBox.Checked) selected.Add(IntegrationMethod.Simpson);
            return selected;
        }

        private void Calculate()
        {
            var methods = SelectedMethods();
            if (methods.Count == 0)
            {
                statusLabel.Text = "Выберите хотя бы один метод с помощью CheckBox.";
                return;
            }
            if (!TryReadFunctionAndBounds(out var expression, out double a, out double b)) return;
            if (!int.TryParse(nTextBox.Text.Trim(), NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out int n) || n < 1 || n > IntegralNumerics.MaximumPartitions)
            {
                statusLabel.Text = $"n должно быть целым числом от 1 до {IntegralNumerics.MaximumPartitions}.";
                return;
            }
            if (!TryReal(epsilonTextBox.Text, out double epsilon) || epsilon <= 0)
            {
                statusLabel.Text = "Точность e должна быть положительным вещественным числом.";
                return;
            }

            ClearResults();
            _expression = expression;
            _a = a;
            _b = b;

            _changingSelection = true;
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (var method in methods)
                {
                    IntegrationRun run = IntegralNumerics.Refine(expression.Evaluate, a, b, n, epsilon, method);
                    _runs.Add(run);
                    var last = run.LastStep;
                    int row = resultsGrid.Rows.Add(
                        IntegrationMethodInfo.Name(method),
                        last == null ? "—" : last.Partitions.ToString(CultureInfo.InvariantCulture),
                        last == null ? "—" : ResultPrecisionFormatter.Format(last.Value, epsilonTextBox.Text),
                        last?.ErrorEstimate is double error ? IntegralNumerics.Format(error, 5) : "—",
                        run.Converged ? "Достигнута" : "Не достигнута");
                    resultsGrid.Rows[row].Tag = run;
                    if (!run.Converged)
                    {
                        resultsGrid.Rows[row].DefaultCellStyle.BackColor =
                            System.Drawing.Color.FromArgb(255, 246, 228);
                        resultsGrid.Rows[row].Cells[4].ToolTipText = run.Message;
                    }
                }
            }
            catch (Exception ex) when (ex is FormatException || ex is ArithmeticException || ex is ArgumentException)
            {
                ClearResults();
                statusLabel.Text = "Ошибка вычисления: " + ex.Message;
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
                _changingSelection = false;
            }

            if (resultsGrid.Rows.Count > 0)
            {
                resultsGrid.ClearSelection();
                resultsGrid.Rows[0].Selected = true;
                SelectRun(resultsGrid.Rows[0].Tag as IntegrationRun);
            }

            int passed = _runs.Count(r => r.Converged);
            string note = string.IsNullOrWhiteSpace(expression.InterpretationNote)
                ? "" : " " + expression.InterpretationNote;
            statusLabel.Text =
                $"Методов рассчитано: {_runs.Count}. Точность e подтверждена оценкой Рунге для {passed} из {_runs.Count}. " +
                "Выбирайте метод, затем шаг истории или двигайте ползунок." + note;
            if (passed != _runs.Count)
                statusLabel.Text += " Неудачные методы выделены: проверьте их сообщение и функцию.";
        }

        private void ClearResults()
        {
            _changingSelection = true;
            _runs.Clear();
            _selectedRun = null;
            _expression = null;
            resultsGrid.Rows.Clear();
            historyGrid.Rows.Clear();
            historyTrackBar.Enabled = false;
            historyTrackBar.Maximum = 0;
            historyTrackBar.Value = 0;
            stepLabel.Text = "Шаги уточнения будут доступны после расчёта.";
            plotPanel.ClearGraph();
            _changingSelection = false;
        }

        private void ResultsGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (_changingSelection || resultsGrid.SelectedRows.Count == 0) return;
            SelectRun(resultsGrid.SelectedRows[0].Tag as IntegrationRun);
        }

        private void SelectRun(IntegrationRun? run)
        {
            _selectedRun = run;
            _changingSelection = true;
            historyGrid.Rows.Clear();
            if (run != null)
            {
                foreach (var step in run.Steps)
                {
                    int row = historyGrid.Rows.Add(
                        step.Number,
                        step.Partitions,
                        IntegralNumerics.Format(step.Value),
                        step.ErrorEstimate.HasValue ? IntegralNumerics.Format(step.ErrorEstimate.Value, 7) : "первый расчёт");
                    historyGrid.Rows[row].Tag = step;
                }
            }
            int lastIndex = Math.Max(0, historyGrid.Rows.Count - 1);
            historyTrackBar.Maximum = lastIndex;
            historyTrackBar.Value = lastIndex;
            historyTrackBar.Enabled = historyGrid.Rows.Count > 1;
            historyGrid.ClearSelection();
            if (historyGrid.Rows.Count > 0)
                historyGrid.Rows[lastIndex].Selected = true;
            _changingSelection = false;
            ShowSelectedStep();
        }

        private void HistoryGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (_changingSelection || historyGrid.SelectedRows.Count == 0) return;
            int index = historyGrid.SelectedRows[0].Index;
            if (index >= historyTrackBar.Minimum && index <= historyTrackBar.Maximum)
            {
                _changingSelection = true;
                historyTrackBar.Value = index;
                _changingSelection = false;
                ShowSelectedStep();
            }
        }

        private void HistoryTrackBar_ValueChanged(object? sender, EventArgs e)
        {
            if (_changingSelection) return;
            _changingSelection = true;
            int index = historyTrackBar.Value;
            historyGrid.ClearSelection();
            if (index < historyGrid.Rows.Count)
            {
                historyGrid.Rows[index].Selected = true;
                historyGrid.FirstDisplayedScrollingRowIndex = Math.Max(0, index);
            }
            _changingSelection = false;
            ShowSelectedStep();
        }

        private void ShowSelectedStep()
        {
            if (_expression == null || _selectedRun == null)
            {
                stepLabel.Text = "Выберите метод и запустите вычисления.";
                return;
            }
            if (_selectedRun.Steps.Count == 0)
            {
                plotPanel.Display(_expression.Evaluate, _a, _b, null, 0);
                stepLabel.Text = _selectedRun.Message;
                return;
            }
            int index = Math.Clamp(historyTrackBar.Value, 0, _selectedRun.Steps.Count - 1);
            var step = _selectedRun.Steps[index];
            plotPanel.Display(_expression.Evaluate, _a, _b, _selectedRun.Method, step.Partitions);
            string error = step.ErrorEstimate.HasValue
                ? $"Оценка Рунге ≈ {IntegralNumerics.Format(step.ErrorEstimate.Value, 6)}"
                : "Первоначальное разбиение: оценка пока неизвестна";
            stepLabel.Text = $"Шаг {step.Number} из {_selectedRun.Steps.Count - 1}; n = {step.Partitions}. {error}.";
        }
    }
}
