using System.Globalization;

namespace Lab0_Task1_Programming.Controls
{
    public sealed class FunctionPlotPanel : Panel
    {
        private Func<double, double>? _function;
        private double _a;
        private double _b;
        private double? _markerX;
        private string _markerLabel = string.Empty;

        public FunctionPlotPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = SystemColors.Window;
            BorderStyle = BorderStyle.FixedSingle;
        }

        public void SetPlot(
            Func<double, double>? function,
            double a,
            double b,
            double? markerX = null,
            string? markerLabel = null)
        {
            _function = function;
            _a = a;
            _b = b;
            _markerX = markerX;
            _markerLabel = markerLabel ?? string.Empty;
            Invalidate();
        }

        public void ClearPlot()
        {
            _function = null;
            _markerX = null;
            _markerLabel = string.Empty;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.Clear(BackColor);

            Rectangle plotRect = Rectangle.Inflate(ClientRectangle, -48, -32);
            if (plotRect.Width < 80 || plotRect.Height < 80)
            {
                return;
            }

            using Font smallFont = new Font(Font.FontFamily, 8F);
            using Font captionFont = new Font(Font.FontFamily, 9F, FontStyle.Bold);

            if (_function is null || !double.IsFinite(_a) || !double.IsFinite(_b) || _a >= _b)
            {
                TextRenderer.DrawText(
                    g,
                    "Здесь будет график функции",
                    captionFont,
                    plotRect,
                    SystemColors.GrayText,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }

            int sampleCount = Math.Clamp(plotRect.Width * 2, 300, 1600);
            var points = new List<(double X, double Y, bool Valid)>(sampleCount);
            var finiteY = new List<double>(sampleCount);

            for (int i = 0; i < sampleCount; i++)
            {
                double x = _a + (_b - _a) * i / (sampleCount - 1.0);
                try
                {
                    double y = _function(x);
                    bool valid = double.IsFinite(y) && Math.Abs(y) < 1e12;
                    points.Add((x, y, valid));
                    if (valid)
                    {
                        finiteY.Add(y);
                    }
                }
                catch
                {
                    points.Add((x, double.NaN, false));
                }
            }

            if (finiteY.Count < 2)
            {
                TextRenderer.DrawText(
                    g,
                    "На выбранном интервале нет достаточного количества конечных значений функции.",
                    smallFont,
                    plotRect,
                    SystemColors.GrayText,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
                return;
            }

            finiteY.Sort();
            int lowIndex = (int)Math.Floor((finiteY.Count - 1) * 0.02);
            int highIndex = (int)Math.Ceiling((finiteY.Count - 1) * 0.98);
            double minY = finiteY[Math.Clamp(lowIndex, 0, finiteY.Count - 1)];
            double maxY = finiteY[Math.Clamp(highIndex, 0, finiteY.Count - 1)];

            if (!double.IsFinite(minY) || !double.IsFinite(maxY))
            {
                return;
            }

            if (Math.Abs(maxY - minY) < 1e-12)
            {
                minY -= 1;
                maxY += 1;
            }
            else
            {
                double padding = (maxY - minY) * 0.12;
                minY -= padding;
                maxY += padding;
            }

            float MapX(double x) => plotRect.Left + (float)((x - _a) / (_b - _a) * plotRect.Width);
            float MapY(double y) => plotRect.Bottom - (float)((y - minY) / (maxY - minY) * plotRect.Height);

            using Pen borderPen = new Pen(SystemColors.ControlDark);
            using Pen axisPen = new Pen(SystemColors.GrayText);
            using Pen curvePen = new Pen(SystemColors.Highlight, 2F);
            using Pen markerPen = new Pen(SystemColors.HotTrack, 1.5F);
            using Brush markerBrush = new SolidBrush(SystemColors.HotTrack);

            g.DrawRectangle(borderPen, plotRect);

            if (_a <= 0 && _b >= 0)
            {
                float x0 = MapX(0);
                g.DrawLine(axisPen, x0, plotRect.Top, x0, plotRect.Bottom);
            }

            if (minY <= 0 && maxY >= 0)
            {
                float y0 = MapY(0);
                g.DrawLine(axisPen, plotRect.Left, y0, plotRect.Right, y0);
            }

            PointF? previous = null;
            double? previousY = null;
            double discontinuityThreshold = Math.Max(1.0, (maxY - minY) * 0.8);

            foreach ((double x, double y, bool valid) in points)
            {
                if (!valid || y < minY - (maxY - minY) || y > maxY + (maxY - minY))
                {
                    previous = null;
                    previousY = null;
                    continue;
                }

                PointF current = new PointF(MapX(x), MapY(y));
                if (previous.HasValue && previousY.HasValue && Math.Abs(y - previousY.Value) < discontinuityThreshold)
                {
                    g.DrawLine(curvePen, previous.Value, current);
                }

                previous = current;
                previousY = y;
            }

            TextRenderer.DrawText(
                g,
                _a.ToString("G6", CultureInfo.CurrentCulture),
                smallFont,
                new Point(plotRect.Left - 4, plotRect.Bottom + 6),
                SystemColors.ControlText);
            TextRenderer.DrawText(
                g,
                _b.ToString("G6", CultureInfo.CurrentCulture),
                smallFont,
                new Point(plotRect.Right - 45, plotRect.Bottom + 6),
                SystemColors.ControlText);
            TextRenderer.DrawText(
                g,
                maxY.ToString("G5", CultureInfo.CurrentCulture),
                smallFont,
                new Point(2, plotRect.Top - 7),
                SystemColors.ControlText);
            TextRenderer.DrawText(
                g,
                minY.ToString("G5", CultureInfo.CurrentCulture),
                smallFont,
                new Point(2, plotRect.Bottom - 8),
                SystemColors.ControlText);

            if (_markerX.HasValue && _markerX.Value >= _a && _markerX.Value <= _b)
            {
                try
                {
                    double markerY = _function(_markerX.Value);
                    if (double.IsFinite(markerY) && markerY >= minY && markerY <= maxY)
                    {
                        float px = MapX(_markerX.Value);
                        float py = MapY(markerY);
                        g.DrawLine(markerPen, px, plotRect.Top, px, plotRect.Bottom);
                        g.FillEllipse(markerBrush, px - 5, py - 5, 10, 10);

                        string text = string.IsNullOrWhiteSpace(_markerLabel)
                            ? $"x = {_markerX.Value:G6}"
                            : _markerLabel;
                        TextRenderer.DrawText(
                            g,
                            text,
                            smallFont,
                            new Point(Math.Min((int)px + 8, plotRect.Right - 180), plotRect.Top + 6),
                            SystemColors.ControlText);
                    }
                }
                catch
                {
                    // График остаётся доступным даже если значение маркера не определено.
                }
            }
        }
    }
}
