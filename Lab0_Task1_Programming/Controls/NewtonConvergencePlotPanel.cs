using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;

namespace Lab0_Task1_Programming.Controls
{
    /// <summary>График f(x) и пошаговая траектория x_n метода Ньютона.</summary>
    public sealed class NewtonConvergencePlotPanel : Panel
    {
        private Func<double, double>? _function;
        private double _a;
        private double _b;
        private IReadOnlyList<NewtonExtremumSolver.Iteration> _history = Array.Empty<NewtonExtremumSolver.Iteration>();
        private int _step;
        private string _modeName = "";
        private string _pointLabel = "";

        public NewtonConvergencePlotPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
        }

        internal void SetPlot(Func<double, double> function, double a, double b,
            IReadOnlyList<NewtonExtremumSolver.Iteration>? history = null, int step = 0,
            string modeName = "", string pointLabel = "")
        {
            _function = function;
            _a = a;
            _b = b;
            _history = history ?? Array.Empty<NewtonExtremumSolver.Iteration>();
            _step = Math.Clamp(step, 0, Math.Max(0, _history.Count - 1));
            _modeName = modeName;
            _pointLabel = pointLabel;
            Invalidate();
        }

        internal void ClearPlot()
        {
            _function = null;
            _history = Array.Empty<NewtonExtremumSolver.Iteration>();
            _pointLabel = "";
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle plot = new Rectangle(66, 30, Math.Max(1, ClientSize.Width - 100), Math.Max(1, ClientSize.Height - 95));
            if (plot.Width < 90 || plot.Height < 90) return;
            using Font font = new Font("Segoe UI", 8.2f);
            using Font strongFont = new Font("Segoe UI", 9f, FontStyle.Bold);
            if (_function is null)
            {
                TextRenderer.DrawText(g, "Постройте график или запустите расчёт", strongFont,
                    plot, Color.Gray, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }

            var samples = new List<(double X, double Y, bool Valid)>();
            var finite = new List<double>();
            int sampleCount = Math.Clamp(plot.Width * 2, 450, 2200);
            for (int i = 0; i < sampleCount; i++)
            {
                double x = _a + (_b - _a) * i / (sampleCount - 1.0);
                try
                {
                    double y = _function(x);
                    bool valid = double.IsFinite(y) && Math.Abs(y) < 1e14;
                    samples.Add((x, y, valid));
                    if (valid) finite.Add(y);
                }
                catch { samples.Add((x, double.NaN, false)); }
            }
            if (finite.Count < 2)
            {
                TextRenderer.DrawText(g, "Недостаточно определённых значений f(x) на этом интервале.", font,
                    plot, Color.DarkRed, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }
            finite.Sort();
            int n = finite.Count;
            double yMin = finite[(int)((n - 1) * .01)];
            double yMax = finite[(int)((n - 1) * .99)];
            foreach (var h in _history)
            {
                if (double.IsFinite(h.Y)) { yMin = Math.Min(yMin, h.Y); yMax = Math.Max(yMax, h.Y); }
            }
            if (Math.Abs(yMax - yMin) < 1e-12) { yMin -= 1; yMax += 1; }
            double yPad = Math.Max((yMax - yMin) * .1, 1e-9);
            yMin -= yPad;
            yMax += yPad;

            float MapX(double x) => plot.Left + (float)((x - _a) / (_b - _a) * plot.Width);
            float MapY(double y) => plot.Bottom - (float)((y - yMin) / (yMax - yMin) * plot.Height);
            using Pen gridPen = new Pen(Color.FromArgb(232, 237, 243), 1);
            using Pen borderPen = new Pen(Color.FromArgb(165, 174, 183));
            using Pen curvePen = new Pen(Color.FromArgb(30, 111, 206), 2);
            using Pen stepPen = new Pen(Color.FromArgb(223, 106, 38), 1.5f) { DashStyle = DashStyle.Dash };
            using Brush stepBrush = new SolidBrush(Color.FromArgb(242, 164, 97));
            using Brush currentBrush = new SolidBrush(Color.FromArgb(188, 45, 45));
            using Pen axisPen = new Pen(Color.FromArgb(100, 114, 130));

            for (int i = 0; i <= 5; i++)
            {
                float px = plot.Left + plot.Width * i / 5f;
                float py = plot.Top + plot.Height * i / 5f;
                g.DrawLine(gridPen, px, plot.Top, px, plot.Bottom);
                g.DrawLine(gridPen, plot.Left, py, plot.Right, py);
                string xtick = (_a + (_b - _a) * i / 5).ToString("G5", CultureInfo.CurrentCulture);
                string ytick = (yMax - (yMax - yMin) * i / 5).ToString("G5", CultureInfo.CurrentCulture);
                TextRenderer.DrawText(g, xtick, font, new Rectangle((int)px - 40, plot.Bottom + 6, 80, 22), Color.DimGray, TextFormatFlags.HorizontalCenter);
                TextRenderer.DrawText(g, ytick, font, new Rectangle(0, (int)py - 10, 62, 22), Color.DimGray, TextFormatFlags.Right);
            }
            if (_a <= 0 && _b >= 0) g.DrawLine(axisPen, MapX(0), plot.Top, MapX(0), plot.Bottom);
            if (yMin <= 0 && yMax >= 0) g.DrawLine(axisPen, plot.Left, MapY(0), plot.Right, MapY(0));
            g.DrawRectangle(borderPen, plot);
            TextRenderer.DrawText(g, "x", strongFont, new Rectangle(plot.Right + 8, plot.Bottom - 4, 25, 25), Color.DimGray);
            TextRenderer.DrawText(g, "f(x)", strongFont, new Rectangle(9, 8, 55, 20), Color.DimGray);

            GraphicsState state = g.Save();
            g.SetClip(plot);
            PointF? last = null;
            double? lastY = null;
            foreach (var p in samples)
            {
                if (!p.Valid || p.Y < yMin - (yMax - yMin) || p.Y > yMax + (yMax - yMin))
                { last = null; lastY = null; continue; }
                PointF current = new PointF(MapX(p.X), MapY(p.Y));
                if (last.HasValue && lastY.HasValue && Math.Abs(p.Y - lastY.Value) < (yMax - yMin) * .55)
                    g.DrawLine(curvePen, last.Value, current);
                last = current;
                lastY = p.Y;
            }
            for (int i = 0; i <= _step && i < _history.Count; i++)
            {
                var h = _history[i];
                if (!double.IsFinite(h.Y) || h.Y < yMin || h.Y > yMax) continue;
                float px = MapX(h.X), py = MapY(h.Y);
                if (i > 0)
                {
                    var previous = _history[i - 1];
                    if (double.IsFinite(previous.Y) && previous.Y >= yMin && previous.Y <= yMax)
                        g.DrawLine(stepPen, MapX(previous.X), MapY(previous.Y), px, py);
                }
                float radius = i == _step ? 6f : 3.5f;
                g.FillEllipse(i == _step ? currentBrush : stepBrush, px - radius, py - radius, radius * 2, radius * 2);
                if (i == _step)
                    g.DrawLine(new Pen(Color.FromArgb(120, 188, 45, 45)), px, plot.Top, px, plot.Bottom);
            }
            g.Restore(state);
            if (_history.Count > 0)
            {
                var h = _history[_step];
                string label = $"{_modeName}: шаг {_step}/{_history.Count - 1}, x = {h.X:G10}, f(x) = {h.Y:G10}";
                TextRenderer.DrawText(g, label, strongFont,
                    new Rectangle(plot.Left, ClientSize.Height - 27, plot.Width, 24), Color.FromArgb(117, 45, 31));
            }
            else if (!string.IsNullOrEmpty(_pointLabel))
                TextRenderer.DrawText(g, _pointLabel, font,
                    new Rectangle(plot.Left, ClientSize.Height - 27, plot.Width, 23), Color.DimGray);
        }
    }
}
