using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab0_Task1_Programming.Controls
{
    // Independent graphic component: function curve and the partitions for a selected method/step.
    internal sealed class IntegralPlotPanel : Panel
    {
        private Func<double, double>? _function;
        private double _a;
        private double _b;
        private int _n;
        private IntegrationMethod? _method;

        public IntegralPlotPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.White;
        }

        public void Display(Func<double, double> function, double a, double b,
            IntegrationMethod? method, int partitions)
        {
            _function = function;
            _a = a;
            _b = b;
            _method = method;
            _n = partitions;
            Invalidate();
        }

        public void ClearGraph()
        {
            _function = null;
            _method = null;
            _n = 0;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(Color.White);

            if (_function == null || !double.IsFinite(_a) || !double.IsFinite(_b))
            {
                DrawCentered(g, "Задайте функцию и выполните расчёт или постройте график.");
                return;
            }
            if (_a == _b)
            {
                DrawCentered(g, "a = b. Определённый интеграл по нулевому отрезку равен 0.");
                return;
            }

            RectangleF plot = new RectangleF(65, 22,
                Math.Max(20, ClientSize.Width - 85),
                Math.Max(20, ClientSize.Height - 90));
            if (plot.Width < 50 || plot.Height < 40) return;

            double intervalMin = Math.Min(_a, _b);
            double intervalMax = Math.Max(_a, _b);
            double length = intervalMax - intervalMin;
            double xPadding = length * 0.035;
            double minX = intervalMin - xPadding;
            double maxX = intervalMax + xPadding;

            double minY = 0, maxY = 0;
            int valid = 0;
            for (int i = 0; i <= 420; i++)
            {
                double x = intervalMin + length * i / 420.0;
                if (!TryF(x, out double y)) continue;
                minY = Math.Min(minY, y);
                maxY = Math.Max(maxY, y);
                valid++;
            }
            if (valid == 0)
            {
                DrawCentered(g, "Нет конечных значений функции на данном интервале.");
                return;
            }
            if (Math.Abs(maxY - minY) < 1e-12 * Math.Max(1, Math.Max(Math.Abs(minY), Math.Abs(maxY))))
            {
                minY -= 1;
                maxY += 1;
            }
            else
            {
                double pad = (maxY - minY) * 0.10;
                minY -= pad;
                maxY += pad;
            }

            float Px(double x) => plot.Left + (float)((x - minX) / (maxX - minX) * plot.Width);
            float Py(double y) => plot.Bottom - (float)((y - minY) / (maxY - minY) * plot.Height);
            float yZero = Py(0);

            using var gridPen = new Pen(Color.FromArgb(235, 239, 244));
            using var axisPen = new Pen(Color.FromArgb(115, 128, 148), 1.4f);
            using var baselinePen = new Pen(Color.FromArgb(105, 119, 142), 1.25f);
            using var curvePen = new Pen(Color.FromArgb(33, 93, 164), 2.2f);
            using var labelBrush = new SolidBrush(Color.FromArgb(94, 105, 122));
            using var captionFont = new Font("Segoe UI", 8.4f);
            using var noteFont = new Font("Segoe UI", 9f, FontStyle.Bold);

            // Tick labels and grid lines.
            for (int i = 0; i <= 6; i++)
            {
                float xPixel = plot.Left + i * plot.Width / 6f;
                g.DrawLine(gridPen, xPixel, plot.Top, xPixel, plot.Bottom);
                double xValue = minX + i * (maxX - minX) / 6.0;
                string label = Tick(xValue);
                SizeF measure = g.MeasureString(label, captionFont);
                g.DrawString(label, captionFont, labelBrush,
                    xPixel - measure.Width / 2, plot.Bottom + 4);
            }
            for (int i = 0; i <= 5; i++)
            {
                float yPixel = plot.Top + i * plot.Height / 5f;
                g.DrawLine(gridPen, plot.Left, yPixel, plot.Right, yPixel);
                string label = Tick(maxY - i * (maxY - minY) / 5.0);
                SizeF measure = g.MeasureString(label, captionFont);
                g.DrawString(label, captionFont, labelBrush,
                    plot.Left - measure.Width - 5, yPixel - measure.Height / 2);
            }

            GraphicsState state = g.Save();
            g.SetClip(plot);
            if (_method.HasValue && _n > 0)
                DrawPartitions(g, Px, Py, yZero, _method.Value, _n);

            // The curve is drawn separately from the integration polygons.
            bool havePrior = false;
            PointF prior = PointF.Empty;
            int count = Math.Max(300, Math.Min(1800, (int)plot.Width * 2));
            for (int i = 0; i <= count; i++)
            {
                double x = minX + (maxX - minX) * i / count;
                if (!TryF(x, out double y)) { havePrior = false; continue; }
                var point = new PointF(Px(x), Py(y));
                if (havePrior && Math.Abs(point.Y - prior.Y) < plot.Height * 1.2f)
                    g.DrawLine(curvePen, prior, point);
                prior = point;
                havePrior = true;
            }
            if (yZero >= plot.Top && yZero <= plot.Bottom)
                g.DrawLine(baselinePen, plot.Left, yZero, plot.Right, yZero);
            if (minX <= 0 && maxX >= 0)
                g.DrawLine(axisPen, Px(0), plot.Top, Px(0), plot.Bottom);
            g.Restore(state);

            using (var borderPen = new Pen(Color.FromArgb(195, 204, 215)))
                g.DrawRectangle(borderPen, plot.X, plot.Y, plot.Width, plot.Height);

            string caption = _method.HasValue && _n > 0
                ? $"{IntegrationMethodInfo.Name(_method.Value)} · разбиений n = {_n}"
                : "График функции f(x)";
            if (_method.HasValue && _n > 160)
                caption += " · для наглядности показана часть разбиений";
            g.DrawString(caption, noteFont, labelBrush, plot.Left, ClientSize.Height - 24);
        }

        private void DrawPartitions(Graphics g, Func<double, float> px,
            Func<double, float> py, float baseY, IntegrationMethod method, int n)
        {
            if (_function == null) return;
            double h = (_b - _a) / n;
            using var fillBrush = new SolidBrush(Color.FromArgb(58, 46, 171, 127));
            using var edgePen = new Pen(Color.FromArgb(165, 28, 137, 99), 1f);
            using var simpsonPen = new Pen(Color.FromArgb(230, 22, 145, 110), 1.5f);

            if (method == IntegrationMethod.Simpson)
            {
                // One parabolic arc covers two equal subintervals.
                int pairs = n / 2;
                int stride = Math.Max(1, (int)Math.Ceiling(pairs / 90.0));
                for (int pair = 0; pair < pairs; pair += stride)
                {
                    int i = 2 * pair;
                    double x0 = _a + i * h;
                    double x1 = _a + (i + 1) * h;
                    double x2 = _a + (i + 2) * h;
                    if (!TryF(x0, out double f0) || !TryF(x1, out double f1) || !TryF(x2, out double f2))
                        continue;
                    var nodes = new List<PointF> { new PointF(px(x0), baseY) };
                    for (int sample = 0; sample <= 16; sample++)
                    {
                        double t = 2.0 * sample / 16.0;
                        double y = ((t - 1) * (t - 2) / 2) * f0
                                 - t * (t - 2) * f1
                                 + (t * (t - 1) / 2) * f2;
                        nodes.Add(new PointF(px(x0 + h * t), py(y)));
                    }
                    nodes.Add(new PointF(px(x2), baseY));
                    using var path = new GraphicsPath();
                    path.AddPolygon(nodes.ToArray());
                    g.FillPath(fillBrush, path);
                    g.DrawLines(simpsonPen, nodes.GetRange(1, nodes.Count - 2).ToArray());
                    g.DrawLine(edgePen, px(x0), baseY, px(x0), py(f0));
                    g.DrawLine(edgePen, px(x2), baseY, px(x2), py(f2));
                }
                return;
            }

            int intervalStride = Math.Max(1, (int)Math.Ceiling(n / 160.0));
            for (int i = 0; i < n; i += intervalStride)
            {
                double left = _a + i * h;
                double right = i == n - 1 ? _b : _a + (i + 1) * h;
                float xLeft = px(left), xRight = px(right);
                if (method == IntegrationMethod.Trapezoids)
                {
                    if (!TryF(left, out double yl) || !TryF(right, out double yr)) continue;
                    PointF[] polygon =
                    {
                        new PointF(xLeft, baseY), new PointF(xLeft, py(yl)),
                        new PointF(xRight, py(yr)), new PointF(xRight, baseY)
                    };
                    g.FillPolygon(fillBrush, polygon);
                    g.DrawLine(edgePen, xLeft, py(yl), xRight, py(yr));
                    g.DrawLine(edgePen, xLeft, baseY, xLeft, py(yl));
                    g.DrawLine(edgePen, xRight, baseY, xRight, py(yr));
                }
                else
                {
                    double sampleX = method switch
                    {
                        IntegrationMethod.LeftRectangles => left,
                        IntegrationMethod.RightRectangles => right,
                        _ => (left + right) / 2
                    };
                    if (!TryF(sampleX, out double y)) continue;
                    float top = py(y);
                    g.FillRectangle(fillBrush, Math.Min(xLeft, xRight),
                        Math.Min(top, baseY), Math.Abs(xRight - xLeft), Math.Abs(top - baseY));
                    g.DrawLine(edgePen, xLeft, top, xRight, top);
                    g.DrawLine(edgePen, xLeft, baseY, xLeft, top);
                    g.DrawLine(edgePen, xRight, baseY, xRight, top);
                }
            }
        }

        private bool TryF(double x, out double value)
        {
            value = double.NaN;
            if (_function == null) return false;
            try { value = _function(x); return double.IsFinite(value); }
            catch (ArithmeticException) { return false; }
            catch (ArgumentException) { return false; }
        }

        private void DrawCentered(Graphics g, string message)
        {
            using var brush = new SolidBrush(Color.FromArgb(126, 139, 157));
            using var font = new Font("Segoe UI", 10f);
            var bounds = new RectangleF(10, 10, ClientSize.Width - 20, ClientSize.Height - 20);
            using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(message, font, brush, bounds, format);
        }

        private static string Tick(double value)
        {
            if (Math.Abs(value) < 1e-11) return "0";
            return value.ToString("G4", System.Globalization.CultureInfo.GetCultureInfo("ru-RU"));
        }
    }
}
