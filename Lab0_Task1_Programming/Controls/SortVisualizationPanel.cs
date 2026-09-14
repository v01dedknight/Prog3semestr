namespace Lab0_Task1_Programming.Controls
{
    public sealed class SortVisualizationPanel : Panel
    {
        private double[] _values = Array.Empty<double>();
        private int _firstIndex = -1;
        private int _secondIndex = -1;

        public SortVisualizationPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = SystemColors.Window;
            BorderStyle = BorderStyle.FixedSingle;
        }

        public void SetState(IReadOnlyList<double> values, int firstIndex = -1, int secondIndex = -1)
        {
            _values = values.ToArray();
            _firstIndex = firstIndex;
            _secondIndex = secondIndex;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_values.Length == 0 || ClientSize.Width <= 4 || ClientSize.Height <= 4)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    "Нет данных",
                    Font,
                    ClientRectangle,
                    SystemColors.GrayText,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }

            double min = _values.Min();
            double max = _values.Max();
            double range = max - min;
            if (Math.Abs(range) < double.Epsilon)
            {
                range = 1;
            }

            int width = ClientSize.Width;
            int height = ClientSize.Height;
            float slotWidth = (float)width / _values.Length;
            float barWidth = Math.Max(1F, slotWidth - 2F);

            for (int i = 0; i < _values.Length; i++)
            {
                double normalized = (_values[i] - min) / range;
                float barHeight = Math.Max(3F, (float)(normalized * (height - 24)) + 3F);
                float x = i * slotWidth + 1F;
                float y = height - barHeight - 2F;

                Brush brush = i == _firstIndex || i == _secondIndex
                    ? SystemBrushes.Highlight
                    : SystemBrushes.ControlDark;

                e.Graphics.FillRectangle(brush, x, y, barWidth, barHeight);
            }
        }
    }
}
