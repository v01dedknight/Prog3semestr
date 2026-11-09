using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace Lab0_Task1_Programming.Controls
{
    public sealed class QuadraticPlotPanel : Panel
    {
        private QuadraticEquationSolution? _equation;

        public QuadraticPlotPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            ResizeRedraw = true;
        }

        internal void SetEquation(QuadraticEquationSolution solution)
        {
            _equation = solution;
            Invalidate();
        }

        internal void ClearGraph()
        {
            _equation = null;
            Invalidate();
        }

        private static string N(double v) => v.ToString("G5", CultureInfo.GetCultureInfo("ru-RU"));
        private static double Evaluate(double a, double b, double c, double x)
            => Math.FusedMultiplyAdd(Math.FusedMultiplyAdd(a, x, b), x, c);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            RectangleF plot = new RectangleF(58, 28, Math.Max(10, Width - 84), Math.Max(10, Height - 85));
            if (_equation is null)
            {
                TextRenderer.DrawText(g, "График появится после решения уравнения.", Font,
                    ClientRectangle, Color.DimGray,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }
            if (plot.Width < 30 || plot.Height < 30) return;

            var s = _equation;
            double center = s.A != 0 ? s.VertexX :
                s.Kind == QuadraticEquationKind.Linear ? s.Root1Real : 0.0;
            if (!double.IsFinite(center)) center = 0;
            double radius = 4.0;
            if (s.Kind is QuadraticEquationKind.TwoReal or QuadraticEquationKind.OneReal)
            {
                radius = Math.Max(radius, Math.Abs(s.Root1Real - center) * 1.4);
                radius = Math.Max(radius, Math.Abs(s.Root2Real - center) * 1.4);
            }
            radius = Math.Min(radius, 1e100);
            double xMin = center - radius;
            double xMax = center + radius;
            if (!double.IsFinite(xMin) || !double.IsFinite(xMax) || xMax <= xMin)
            {
                xMin = -4;
                xMax = 4;
            }

            int sampleCount = Math.Clamp((int)plot.Width, 160, 900);
            var samples = new List<PointF>(sampleCount + 1);
            double yMin = 0.0, yMax = 0.0;
            for (int i = 0; i <= sampleCount; i++)
            {
                double x = xMin + (xMax - xMin) * i / sampleCount;
                double y = Evaluate(s.A, s.B, s.C, x);
                if (!double.IsFinite(y)) continue;
                yMin = Math.Min(yMin, y);
                yMax = Math.Max(yMax, y);
                samples.Add(new PointF((float)i / sampleCount, (float)y));
            }
            double span = yMax - yMin;
            if (!double.IsFinite(span) || span <= 0)
            {
                span = Math.Max(1, Math.Abs(yMax) * 0.2);
            }
            yMin -= 0.12 * span;
            yMax += 0.12 * span;
            double ySpan = yMax - yMin;
            if (!double.IsFinite(ySpan) || ySpan <= 0)
                return;

            float PX(double x) => plot.Left + (float)((x - xMin) / (xMax - xMin) * plot.Width);
            float PY(double y) => plot.Bottom - (float)((y - yMin) / ySpan * plot.Height);

            using var gridPen = new Pen(Color.FromArgb(228, 232, 240), 1f);
            using var axisPen = new Pen(Color.FromArgb(120, 132, 149), 1.3f);
            using var curvePen = new Pen(Color.FromArgb(34, 103, 182), 2.4f);
            using var rootBrush = new SolidBrush(Color.FromArgb(36, 153, 101));
            using var vertexBrush = new SolidBrush(Color.FromArgb(220, 113, 39));
            using var textBrush = new SolidBrush(Color.FromArgb(64, 76, 92));

            for (int i = 0; i <= 6; i++)
            {
                float px = plot.Left + plot.Width * i / 6f;
                g.DrawLine(gridPen, px, plot.Top, px, plot.Bottom);
                double xv = xMin + (xMax - xMin) * i / 6;
                string txt = N(xv);
                SizeF ts = g.MeasureString(txt, Font);
                g.DrawString(txt, Font, textBrush, px - ts.Width / 2, plot.Bottom + 6);
            }
            for (int i = 0; i <= 4; i++)
            {
                float py = plot.Top + plot.Height * i / 4f;
                g.DrawLine(gridPen, plot.Left, py, plot.Right, py);
                double yv = yMax - ySpan * i / 4;
                string txt = N(yv);
                SizeF ts = g.MeasureString(txt, Font);
                g.DrawString(txt, Font, textBrush, plot.Left - ts.Width - 8, py - ts.Height / 2);
            }
            if (xMin <= 0 && xMax >= 0)
                g.DrawLine(axisPen, PX(0), plot.Top, PX(0), plot.Bottom);
            if (yMin <= 0 && yMax >= 0)
                g.DrawLine(axisPen, plot.Left, PY(0), plot.Right, PY(0));

            using (var path = new GraphicsPath())
            {
                bool started = false;
                PointF previous = default;
                foreach (var item in samples)
                {
                    PointF p = new PointF(plot.Left + item.X * plot.Width,
                        PY(item.Y));
                    if (!float.IsFinite(p.Y) || p.Y < -1000000 || p.Y > 1000000)
                    {
                        started = false;
                        continue;
                    }
                    if (started) path.AddLine(previous, p);
                    previous = p;
                    started = true;
                }
                g.SetClip(plot);
                g.DrawPath(curvePen, path);
                g.ResetClip();
            }

            if (s.A != 0 && double.IsFinite(s.VertexY))
                DrawMarker(s.VertexX, s.VertexY, "Вершина", vertexBrush);
            if (s.Kind is QuadraticEquationKind.TwoReal or QuadraticEquationKind.OneReal)
            {
                DrawMarker(s.Root1Real, 0, "x₁", rootBrush);
                if (s.Kind == QuadraticEquationKind.TwoReal && Math.Abs(PX(s.Root2Real) - PX(s.Root1Real)) > 14)
                    DrawMarker(s.Root2Real, 0, "x₂", rootBrush);
            }

            string legend = "Синий — f(x)    Зелёный — корни    Оранжевый — вершина";
            g.DrawString(legend, Font, textBrush, plot.Left, 6);

            void DrawMarker(double x, double y, string title, Brush brush)
            {
                float px = PX(x), py = PY(y);
                if (!float.IsFinite(px) || !float.IsFinite(py) || !plot.Contains(px, py)) return;
                g.FillEllipse(brush, px - 5, py - 5, 10, 10);
                g.DrawEllipse(Pens.White, px - 5, py - 5, 10, 10);
                string caption = $"{title}: ({N(x)}; {N(y)})";
                SizeF size = g.MeasureString(caption, Font);
                float textX = Math.Max(plot.Left, Math.Min(px + 8, plot.Right - size.Width));
                float textY = py - size.Height - 7;
                if (textY < plot.Top) textY = py + 8;
                g.DrawString(caption, Font, brush, textX, textY);
            }
        }
    }
}
