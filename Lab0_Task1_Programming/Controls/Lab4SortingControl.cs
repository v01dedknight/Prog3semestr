using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Lab0_Task1_Programming.Controls
{
    public partial class Lab4SortingControl : UserControl
    {
        private const int MaxVisualValues = 200;
        private const int MaxBogoValues = 8;
        private const int MaxBogoShuffles = 2_000_000;

        private CancellationTokenSource? _sortingCts;
        private bool _isRunning;

        public Lab4SortingControl()
        {
            InitializeComponent();
        }

        private void GenerateToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                return;
            }

            int count = (int)countNumericUpDown.Value;
            int min = (int)minNumericUpDown.Value;
            int max = (int)maxNumericUpDown.Value;

            if (min > max)
            {
                MessageBox.Show(
                    "Минимальное значение не может быть больше максимального.",
                    "Некорректные данные",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            valuesDataGridView.Rows.Clear();

            for (int i = 0; i < count; i++)
            {
                int generatedValue = Random.Shared.Next(min, max + 1);

                // Важно передавать значение как содержимое ячейки.
                // Rows.Add(int) — это другая перегрузка: она воспринимает int
                // как количество копий строк и падает на нуле/отрицательных числах.
                valuesDataGridView.Rows.Add(new object[] { generatedValue });
            }

            ClearResults();
            sourceStatusLabel.Text = $"Сгенерировано значений: {count}.";
        }

        private void LoadExcelToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                return;
            }

            using var dialog = new OpenFileDialog
            {
                Title = "Выберите Excel-файл",
                Filter = "Книга Excel (*.xlsx)|*.xlsx",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                List<double> values = ReadNumbersFromXlsx(dialog.FileName);
                PutValuesIntoGrid(values);
                sourceStatusLabel.Text = $"Загружено из Excel: {values.Count} значений.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось загрузить Excel-файл.\n\n{ex.Message}",
                    "Ошибка загрузки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void LoadGoogleToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                return;
            }

            string sourceUrl = googleTableTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(sourceUrl))
            {
                MessageBox.Show(
                    "Вставьте ссылку на Google Table в поле формы.",
                    "Ссылка не указана",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                googleTableTextBox.Focus();
                return;
            }

            try
            {
                loadGoogleToolStripMenuItem.Enabled = false;
                UseWaitCursor = true;

                string csvUrl = BuildGoogleCsvUrl(sourceUrl);
                using var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(20)
                };

                string csv = await httpClient.GetStringAsync(csvUrl);
                List<double> values = ExtractNumbersFromCsv(csv);
                PutValuesIntoGrid(values);
                sourceStatusLabel.Text = $"Загружено из Google Table: {values.Count} значений.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось загрузить данные из Google Table. " +
                    "Таблица должна быть доступна по ссылке.\n\n" + ex.Message,
                    "Ошибка загрузки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                loadGoogleToolStripMenuItem.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private async void StartSortToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                return;
            }

            if (!TryReadGridValues(out double[] sourceValues))
            {
                return;
            }

            List<SortAlgorithmKind> algorithms = GetSelectedAlgorithms();
            if (algorithms.Count == 0)
            {
                MessageBox.Show(
                    "Выберите хотя бы один алгоритм сортировки.",
                    "Алгоритм не выбран",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (algorithms.Contains(SortAlgorithmKind.Bogo) && sourceValues.Length > MaxBogoValues)
            {
                MessageBox.Show(
                    $"Для BOGO допускается не более {MaxBogoValues} значений. " +
                    "Уменьшите набор данных или отключите BOGO.",
                    "Ограничение BOGO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool ascending = ascendingRadioButton.Checked;
            _sortingCts = new CancellationTokenSource();
            SetRunningState(true);
            ClearResults();

            try
            {
                List<BenchmarkResult> benchmarkResults = BenchmarkSelectedAlgorithms(
                    algorithms,
                    sourceValues,
                    ascending);

                ShowBenchmarkResults(benchmarkResults);

                Dictionary<SortAlgorithmKind, VisualizationCard> cards =
                    CreateVisualizationCards(algorithms, sourceValues);

                int delay = (int)delayNumericUpDown.Value;
                IEnumerable<Task> animationTasks = algorithms.Select(algorithm =>
                    AnimateAlgorithmAsync(
                        algorithm,
                        sourceValues,
                        ascending,
                        cards[algorithm],
                        delay,
                        _sortingCts.Token));

                await Task.WhenAll(animationTasks);
                sourceStatusLabel.Text = "Сортировка и визуализация завершены.";
            }
            catch (OperationCanceledException)
            {
                sourceStatusLabel.Text = "Визуализация остановлена пользователем.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при выполнении сортировки:\n\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _sortingCts?.Dispose();
                _sortingCts = null;
                SetRunningState(false);
            }
        }

        private void StopSortToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            _sortingCts?.Cancel();
        }

        private void ClearToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                _sortingCts?.Cancel();
                return;
            }

            valuesDataGridView.Rows.Clear();
            googleTableTextBox.Clear();
            ClearResults();
            sourceStatusLabel.Text = "Введите данные вручную, сгенерируйте их или загрузите из источника.";
        }

        private void ValuesDataGridView_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show(
                "В таблице допускаются только числовые значения.",
                "Некорректные данные",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private bool TryReadGridValues(out double[] values)
        {
            var result = new List<double>();

            foreach (DataGridViewRow row in valuesDataGridView.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                string text = Convert.ToString(row.Cells[0].Value)?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(text))
                {
                    continue;
                }

                if (!TryParseNumber(text, out double value) ||
                    double.IsNaN(value) ||
                    double.IsInfinity(value))
                {
                    valuesDataGridView.CurrentCell = row.Cells[0];
                    MessageBox.Show(
                        $"Строка {row.Index + 1}: значение «{text}» не является допустимым числом.",
                        "Некорректные данные",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    values = Array.Empty<double>();
                    return false;
                }

                result.Add(value);
            }

            if (result.Count < 2)
            {
                MessageBox.Show(
                    "Для сортировки необходимо ввести минимум два числа.",
                    "Недостаточно данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                values = Array.Empty<double>();
                return false;
            }

            if (result.Count > MaxVisualValues)
            {
                MessageBox.Show(
                    $"Для визуализации допускается не более {MaxVisualValues} значений.",
                    "Слишком много данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                values = Array.Empty<double>();
                return false;
            }

            values = result.ToArray();
            return true;
        }

        private List<SortAlgorithmKind> GetSelectedAlgorithms()
        {
            var result = new List<SortAlgorithmKind>();

            if (bubbleCheckBox.Checked)
            {
                result.Add(SortAlgorithmKind.Bubble);
            }
            if (insertionCheckBox.Checked)
            {
                result.Add(SortAlgorithmKind.Insertion);
            }
            if (shakerCheckBox.Checked)
            {
                result.Add(SortAlgorithmKind.Shaker);
            }
            if (quickCheckBox.Checked)
            {
                result.Add(SortAlgorithmKind.Quick);
            }
            if (bogoCheckBox.Checked)
            {
                result.Add(SortAlgorithmKind.Bogo);
            }

            return result;
        }

        private List<BenchmarkResult> BenchmarkSelectedAlgorithms(
            IEnumerable<SortAlgorithmKind> algorithms,
            double[] source,
            bool ascending)
        {
            var results = new List<BenchmarkResult>();

            foreach (SortAlgorithmKind algorithm in algorithms)
            {
                double[] data = (double[])source.Clone();
                var stopwatch = Stopwatch.StartNew();
                bool completed = SortWithoutAnimation(algorithm, data, ascending);
                stopwatch.Stop();

                results.Add(new BenchmarkResult(
                    algorithm,
                    stopwatch.Elapsed.TotalMilliseconds,
                    completed));
            }

            return results;
        }

        private void ShowBenchmarkResults(IReadOnlyList<BenchmarkResult> results)
        {
            resultsDataGridView.Rows.Clear();

            foreach (BenchmarkResult result in results)
            {
                resultsDataGridView.Rows.Add(
                    GetAlgorithmName(result.Algorithm),
                    result.ElapsedMilliseconds.ToString("F4", CultureInfo.CurrentCulture),
                    result.Completed ? "Выполнено" : "Достигнут лимит");
            }

            BenchmarkResult? fastest = results
                .Where(result => result.Completed)
                .OrderBy(result => result.ElapsedMilliseconds)
                .FirstOrDefault();

            fastestLabel.Text = fastest is null
                ? "Самый быстрый: определить не удалось"
                : $"Самый быстрый: {GetAlgorithmName(fastest.Algorithm)}";
        }

        private Dictionary<SortAlgorithmKind, VisualizationCard> CreateVisualizationCards(
            IEnumerable<SortAlgorithmKind> algorithms,
            double[] source)
        {
            visualizationFlowLayoutPanel.SuspendLayout();
            visualizationFlowLayoutPanel.Controls.Clear();

            var cards = new Dictionary<SortAlgorithmKind, VisualizationCard>();

            foreach (SortAlgorithmKind algorithm in algorithms)
            {
                var cardPanel = new Panel
                {
                    Width = 255,
                    Height = 220,
                    Margin = new Padding(6),
                    BorderStyle = BorderStyle.FixedSingle
                };

                var titleLabel = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 28,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = GetAlgorithmName(algorithm)
                };

                var statusLabel = new Label
                {
                    Dock = DockStyle.Bottom,
                    Height = 28,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "Ожидание"
                };

                var visualPanel = new SortVisualizationPanel
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(4)
                };
                visualPanel.SetState(source);

                cardPanel.Controls.Add(visualPanel);
                cardPanel.Controls.Add(statusLabel);
                cardPanel.Controls.Add(titleLabel);

                visualizationFlowLayoutPanel.Controls.Add(cardPanel);
                cards[algorithm] = new VisualizationCard(visualPanel, statusLabel);
            }

            visualizationFlowLayoutPanel.ResumeLayout();
            return cards;
        }

        private async Task AnimateAlgorithmAsync(
            SortAlgorithmKind algorithm,
            double[] source,
            bool ascending,
            VisualizationCard card,
            int delayMilliseconds,
            CancellationToken cancellationToken)
        {
            double[] data = (double[])source.Clone();
            card.StatusLabel.Text = "Сортировка...";

            bool completed = algorithm switch
            {
                SortAlgorithmKind.Bubble => await AnimateBubbleSortAsync(data, ascending, card.Panel, delayMilliseconds, cancellationToken),
                SortAlgorithmKind.Insertion => await AnimateInsertionSortAsync(data, ascending, card.Panel, delayMilliseconds, cancellationToken),
                SortAlgorithmKind.Shaker => await AnimateShakerSortAsync(data, ascending, card.Panel, delayMilliseconds, cancellationToken),
                SortAlgorithmKind.Quick => await AnimateQuickSortAsync(data, ascending, card.Panel, delayMilliseconds, cancellationToken),
                SortAlgorithmKind.Bogo => await AnimateBogoSortAsync(data, ascending, card.Panel, delayMilliseconds, cancellationToken),
                _ => false
            };

            card.Panel.SetState(data);
            card.StatusLabel.Text = completed ? "Готово" : "Лимит BOGO";
        }

        private static async Task<bool> AnimateBubbleSortAsync(
            double[] data,
            bool ascending,
            SortVisualizationPanel panel,
            int delay,
            CancellationToken token)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                bool swapped = false;

                for (int j = 0; j < data.Length - i - 1; j++)
                {
                    token.ThrowIfCancellationRequested();
                    panel.SetState(data, j, j + 1);
                    await DelayStepAsync(delay, token);

                    if (ShouldSwap(data[j], data[j + 1], ascending))
                    {
                        (data[j], data[j + 1]) = (data[j + 1], data[j]);
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }

            return true;
        }

        private static async Task<bool> AnimateInsertionSortAsync(
            double[] data,
            bool ascending,
            SortVisualizationPanel panel,
            int delay,
            CancellationToken token)
        {
            for (int i = 1; i < data.Length; i++)
            {
                double key = data[i];
                int j = i - 1;

                while (j >= 0 && Compare(data[j], key, ascending) > 0)
                {
                    token.ThrowIfCancellationRequested();
                    panel.SetState(data, j, j + 1);
                    await DelayStepAsync(delay, token);

                    data[j + 1] = data[j];
                    j--;
                }

                data[j + 1] = key;
                panel.SetState(data, Math.Max(0, j + 1), i);
                await DelayStepAsync(delay, token);
            }

            return true;
        }

        private static async Task<bool> AnimateShakerSortAsync(
            double[] data,
            bool ascending,
            SortVisualizationPanel panel,
            int delay,
            CancellationToken token)
        {
            int left = 0;
            int right = data.Length - 1;
            bool swapped = true;

            while (swapped && left < right)
            {
                swapped = false;

                for (int i = left; i < right; i++)
                {
                    token.ThrowIfCancellationRequested();
                    panel.SetState(data, i, i + 1);
                    await DelayStepAsync(delay, token);

                    if (ShouldSwap(data[i], data[i + 1], ascending))
                    {
                        (data[i], data[i + 1]) = (data[i + 1], data[i]);
                        swapped = true;
                    }
                }

                right--;
                if (!swapped)
                {
                    break;
                }

                swapped = false;
                for (int i = right; i > left; i--)
                {
                    token.ThrowIfCancellationRequested();
                    panel.SetState(data, i - 1, i);
                    await DelayStepAsync(delay, token);

                    if (ShouldSwap(data[i - 1], data[i], ascending))
                    {
                        (data[i - 1], data[i]) = (data[i], data[i - 1]);
                        swapped = true;
                    }
                }

                left++;
            }

            return true;
        }

        private static async Task<bool> AnimateQuickSortAsync(
            double[] data,
            bool ascending,
            SortVisualizationPanel panel,
            int delay,
            CancellationToken token)
        {
            await QuickSortAnimatedRangeAsync(
                data,
                0,
                data.Length - 1,
                ascending,
                panel,
                delay,
                token);
            return true;
        }

        private static async Task QuickSortAnimatedRangeAsync(
            double[] data,
            int left,
            int right,
            bool ascending,
            SortVisualizationPanel panel,
            int delay,
            CancellationToken token)
        {
            if (left >= right)
            {
                return;
            }

            int i = left;
            int j = right;
            double pivot = data[left + (right - left) / 2];

            while (i <= j)
            {
                while (i <= right && Compare(data[i], pivot, ascending) < 0)
                {
                    i++;
                }
                while (j >= left && Compare(data[j], pivot, ascending) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    token.ThrowIfCancellationRequested();
                    panel.SetState(data, i, j);
                    await DelayStepAsync(delay, token);

                    (data[i], data[j]) = (data[j], data[i]);
                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                await QuickSortAnimatedRangeAsync(data, left, j, ascending, panel, delay, token);
            }
            if (i < right)
            {
                await QuickSortAnimatedRangeAsync(data, i, right, ascending, panel, delay, token);
            }
        }

        private static async Task<bool> AnimateBogoSortAsync(
            double[] data,
            bool ascending,
            SortVisualizationPanel panel,
            int delay,
            CancellationToken token)
        {
            var random = new Random(2026);
            int shuffles = 0;
            int renderEvery = Math.Max(1, data.Length * 5);

            while (!IsSorted(data, ascending) && shuffles < MaxBogoShuffles)
            {
                token.ThrowIfCancellationRequested();
                Shuffle(data, random);
                shuffles++;

                if (shuffles % renderEvery == 0)
                {
                    panel.SetState(data);
                    await DelayStepAsync(delay, token);
                }
            }

            return IsSorted(data, ascending);
        }

        private static Task DelayStepAsync(int delayMilliseconds, CancellationToken token)
        {
            return delayMilliseconds <= 0
                ? Task.CompletedTask
                : Task.Delay(delayMilliseconds, token);
        }

        private static bool SortWithoutAnimation(
            SortAlgorithmKind algorithm,
            double[] data,
            bool ascending)
        {
            switch (algorithm)
            {
                case SortAlgorithmKind.Bubble:
                    BubbleSort(data, ascending);
                    return true;
                case SortAlgorithmKind.Insertion:
                    InsertionSort(data, ascending);
                    return true;
                case SortAlgorithmKind.Shaker:
                    ShakerSort(data, ascending);
                    return true;
                case SortAlgorithmKind.Quick:
                    QuickSort(data, 0, data.Length - 1, ascending);
                    return true;
                case SortAlgorithmKind.Bogo:
                    return BogoSort(data, ascending);
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm));
            }
        }

        private static void BubbleSort(double[] data, bool ascending)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < data.Length - i - 1; j++)
                {
                    if (ShouldSwap(data[j], data[j + 1], ascending))
                    {
                        (data[j], data[j + 1]) = (data[j + 1], data[j]);
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }
        }

        private static void InsertionSort(double[] data, bool ascending)
        {
            for (int i = 1; i < data.Length; i++)
            {
                double key = data[i];
                int j = i - 1;

                while (j >= 0 && Compare(data[j], key, ascending) > 0)
                {
                    data[j + 1] = data[j];
                    j--;
                }

                data[j + 1] = key;
            }
        }

        private static void ShakerSort(double[] data, bool ascending)
        {
            int left = 0;
            int right = data.Length - 1;
            bool swapped = true;

            while (swapped && left < right)
            {
                swapped = false;
                for (int i = left; i < right; i++)
                {
                    if (ShouldSwap(data[i], data[i + 1], ascending))
                    {
                        (data[i], data[i + 1]) = (data[i + 1], data[i]);
                        swapped = true;
                    }
                }

                right--;
                if (!swapped)
                {
                    break;
                }

                swapped = false;
                for (int i = right; i > left; i--)
                {
                    if (ShouldSwap(data[i - 1], data[i], ascending))
                    {
                        (data[i - 1], data[i]) = (data[i], data[i - 1]);
                        swapped = true;
                    }
                }

                left++;
            }
        }

        private static void QuickSort(double[] data, int left, int right, bool ascending)
        {
            int i = left;
            int j = right;
            double pivot = data[left + (right - left) / 2];

            while (i <= j)
            {
                while (Compare(data[i], pivot, ascending) < 0)
                {
                    i++;
                }
                while (Compare(data[j], pivot, ascending) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    (data[i], data[j]) = (data[j], data[i]);
                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                QuickSort(data, left, j, ascending);
            }
            if (i < right)
            {
                QuickSort(data, i, right, ascending);
            }
        }

        private static bool BogoSort(double[] data, bool ascending)
        {
            var random = new Random(2026);
            int shuffles = 0;

            while (!IsSorted(data, ascending) && shuffles < MaxBogoShuffles)
            {
                Shuffle(data, random);
                shuffles++;
            }

            return IsSorted(data, ascending);
        }

        private static void Shuffle(double[] data, Random random)
        {
            for (int i = data.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (data[i], data[j]) = (data[j], data[i]);
            }
        }

        private static bool IsSorted(double[] data, bool ascending)
        {
            for (int i = 1; i < data.Length; i++)
            {
                if (Compare(data[i - 1], data[i], ascending) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ShouldSwap(double left, double right, bool ascending)
        {
            return Compare(left, right, ascending) > 0;
        }

        private static int Compare(double left, double right, bool ascending)
        {
            int result = left.CompareTo(right);
            return ascending ? result : -result;
        }

        private void PutValuesIntoGrid(IReadOnlyList<double> values)
        {
            if (values.Count < 2)
            {
                throw new InvalidDataException("Источник должен содержать минимум два числовых значения.");
            }

            if (values.Count > MaxVisualValues)
            {
                throw new InvalidDataException(
                    $"Найдено {values.Count} значений. Для визуализации допускается не более {MaxVisualValues}.");
            }

            valuesDataGridView.Rows.Clear();
            foreach (double value in values)
            {
                valuesDataGridView.Rows.Add(value.ToString(CultureInfo.CurrentCulture));
            }

            ClearResults();
        }

        private void ClearResults()
        {
            resultsDataGridView.Rows.Clear();
            fastestLabel.Text = "Самый быстрый: —";
            visualizationFlowLayoutPanel.Controls.Clear();
        }

        private void SetRunningState(bool running)
        {
            _isRunning = running;
            generateToolStripMenuItem.Enabled = !running;
            loadExcelToolStripMenuItem.Enabled = !running;
            loadGoogleToolStripMenuItem.Enabled = !running;
            clearToolStripMenuItem.Enabled = !running;
            startSortToolStripMenuItem.Enabled = !running;
            stopSortToolStripMenuItem.Enabled = running;
            valuesDataGridView.ReadOnly = running;
            settingsGroupBox.Enabled = !running;
            algorithmsGroupBox.Enabled = !running;
        }

        private static bool TryParseNumber(string text, out double value)
        {
            text = text.Trim();

            if (double.TryParse(
                text,
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.CurrentCulture,
                out value))
            {
                return true;
            }

            string normalized = text.Replace(',', '.');
            return double.TryParse(
                normalized,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out value);
        }

        private static string BuildGoogleCsvUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                throw new ArgumentException("Некорректная ссылка.");
            }

            if (url.Contains("output=csv", StringComparison.OrdinalIgnoreCase) ||
                url.Contains("format=csv", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }

            Match idMatch = Regex.Match(
                url,
                @"/spreadsheets/d/([^/]+)",
                RegexOptions.IgnoreCase);

            if (!idMatch.Success)
            {
                throw new ArgumentException("Не удалось определить идентификатор Google Table.");
            }

            Match gidMatch = Regex.Match(url, @"[?&#]gid=(\d+)", RegexOptions.IgnoreCase);
            string gid = gidMatch.Success ? gidMatch.Groups[1].Value : "0";
            string id = idMatch.Groups[1].Value;

            return $"https://docs.google.com/spreadsheets/d/{id}/export?format=csv&gid={gid}";
        }

        private static List<double> ExtractNumbersFromCsv(string csv)
        {
            var values = new List<double>();

            foreach (string field in ParseCsvFields(csv))
            {
                if (TryParseNumber(field, out double value) &&
                    !double.IsNaN(value) &&
                    !double.IsInfinity(value))
                {
                    values.Add(value);
                }
            }

            if (values.Count == 0)
            {
                throw new InvalidDataException("В таблице не найдено числовых значений.");
            }

            return values;
        }

        private static IEnumerable<string> ParseCsvFields(string csv)
        {
            var field = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < csv.Length; i++)
            {
                char c = csv[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < csv.Length && csv[i + 1] == '"')
                    {
                        field.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (!inQuotes && (c == ',' || c == ';' || c == '\n' || c == '\r'))
                {
                    if (field.Length > 0)
                    {
                        yield return field.ToString().Trim();
                        field.Clear();
                    }
                }
                else
                {
                    field.Append(c);
                }
            }

            if (field.Length > 0)
            {
                yield return field.ToString().Trim();
            }
        }

        private static List<double> ReadNumbersFromXlsx(string filePath)
        {
            using ZipArchive archive = ZipFile.OpenRead(filePath);

            List<string> sharedStrings = ReadSharedStrings(archive);
            string worksheetPath = GetFirstWorksheetPath(archive);
            ZipArchiveEntry sheetEntry = archive.GetEntry(worksheetPath)
                ?? throw new InvalidDataException("Не найден первый лист Excel.");

            using Stream sheetStream = sheetEntry.Open();
            XDocument sheetDocument = XDocument.Load(sheetStream);
            XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

            var values = new List<double>();

            foreach (XElement cell in sheetDocument.Descendants(ns + "c"))
            {
                string? type = (string?)cell.Attribute("t");
                string rawValue = cell.Element(ns + "v")?.Value
                    ?? cell.Element(ns + "is")?.Element(ns + "t")?.Value
                    ?? string.Empty;

                if (type == "s" &&
                    int.TryParse(rawValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int index) &&
                    index >= 0 && index < sharedStrings.Count)
                {
                    rawValue = sharedStrings[index];
                }

                if (TryParseNumber(rawValue, out double number) &&
                    !double.IsNaN(number) &&
                    !double.IsInfinity(number))
                {
                    values.Add(number);
                }
            }

            if (values.Count == 0)
            {
                throw new InvalidDataException("В первом листе Excel не найдено числовых значений.");
            }

            return values;
        }

        private static List<string> ReadSharedStrings(ZipArchive archive)
        {
            ZipArchiveEntry? entry = archive.GetEntry("xl/sharedStrings.xml");
            if (entry is null)
            {
                return new List<string>();
            }

            using Stream stream = entry.Open();
            XDocument document = XDocument.Load(stream);
            XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

            return document
                .Descendants(ns + "si")
                .Select(item => string.Concat(item.Descendants(ns + "t").Select(text => text.Value)))
                .ToList();
        }

        private static string GetFirstWorksheetPath(ZipArchive archive)
        {
            ZipArchiveEntry workbookEntry = archive.GetEntry("xl/workbook.xml")
                ?? throw new InvalidDataException("Файл не содержит workbook.xml.");
            ZipArchiveEntry relsEntry = archive.GetEntry("xl/_rels/workbook.xml.rels")
                ?? throw new InvalidDataException("Файл не содержит связей книги Excel.");

            XNamespace spreadsheetNs = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
            XNamespace relationNs = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
            XNamespace packageRelationNs = "http://schemas.openxmlformats.org/package/2006/relationships";

            using Stream workbookStream = workbookEntry.Open();
            XDocument workbookDocument = XDocument.Load(workbookStream);
            XElement? firstSheet = workbookDocument.Descendants(spreadsheetNs + "sheet").FirstOrDefault();
            string relationshipId = (string?)firstSheet?.Attribute(relationNs + "id")
                ?? throw new InvalidDataException("В книге Excel нет листов.");

            using Stream relsStream = relsEntry.Open();
            XDocument relsDocument = XDocument.Load(relsStream);
            XElement? relation = relsDocument
                .Descendants(packageRelationNs + "Relationship")
                .FirstOrDefault(item => (string?)item.Attribute("Id") == relationshipId);

            string target = (string?)relation?.Attribute("Target")
                ?? throw new InvalidDataException("Не удалось определить путь к первому листу Excel.");

            target = target.Replace('\\', '/');
            if (target.StartsWith("/", StringComparison.Ordinal))
            {
                target = target.TrimStart('/');
            }
            else if (!target.StartsWith("xl/", StringComparison.OrdinalIgnoreCase))
            {
                target = "xl/" + target;
            }

            while (target.Contains("../", StringComparison.Ordinal))
            {
                target = target.Replace("../", string.Empty, StringComparison.Ordinal);
            }

            return target;
        }

        private static string GetAlgorithmName(SortAlgorithmKind algorithm)
        {
            return algorithm switch
            {
                SortAlgorithmKind.Bubble => "Пузырьковая",
                SortAlgorithmKind.Insertion => "Вставками",
                SortAlgorithmKind.Shaker => "Шейкерная",
                SortAlgorithmKind.Quick => "Быстрая",
                SortAlgorithmKind.Bogo => "BOGO",
                _ => algorithm.ToString()
            };
        }

        private enum SortAlgorithmKind
        {
            Bubble,
            Insertion,
            Shaker,
            Quick,
            Bogo
        }

        private sealed record BenchmarkResult(
            SortAlgorithmKind Algorithm,
            double ElapsedMilliseconds,
            bool Completed);

        private sealed record VisualizationCard(
            SortVisualizationPanel Panel,
            Label StatusLabel);
    }
}
