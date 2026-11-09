using System;

namespace Lab0_Task1_Programming.Controls
{
    internal enum QuadraticEquationKind
    {
        TwoReal,
        OneReal,
        TwoComplex,
        Linear,
        NoSolutions,
        AllRealNumbers
    }

    internal sealed class QuadraticEquationSolution
    {
        public double A { get; init; }
        public double B { get; init; }
        public double C { get; init; }
        public double Discriminant { get; init; }
        public QuadraticEquationKind Kind { get; init; }
        public double Root1Real { get; init; }
        public double Root1Imaginary { get; init; }
        public double Root2Real { get; init; }
        public double Root2Imaginary { get; init; }
        public double VertexX { get; init; }
        public double VertexY { get; init; }
    }

    internal static class QuadraticEquationSolver
    {
        public static QuadraticEquationSolution Solve(double a, double b, double c)
        {
            if (!double.IsFinite(a) || !double.IsFinite(b) || !double.IsFinite(c))
                throw new ArgumentException("Коэффициенты должны быть конечными числами.");

            if (a == 0)
            {
                if (b == 0)
                {
                    return new QuadraticEquationSolution
                    {
                        A = a, B = b, C = c,
                        Kind = c == 0 ? QuadraticEquationKind.AllRealNumbers : QuadraticEquationKind.NoSolutions
                    };
                }

                double x = -c / b;
                if (!double.IsFinite(x))
                    throw new ArithmeticException("Не удалось вычислить корень: коэффициенты слишком велики по отношению друг к другу.");

                return new QuadraticEquationSolution
                {
                    A = a, B = b, C = c,
                    Kind = QuadraticEquationKind.Linear,
                    Root1Real = x
                };
            }

            // FMA improves accuracy when b*b and 4*a*c nearly cancel.
            double fourAC = (4.0 * a) * c;
            double bSquared = b * b;
            double d = Math.FusedMultiplyAdd(b, b, -fourAC);
            if (!double.IsFinite(d))
                throw new ArithmeticException("Дискриминант вышел за диапазон double. Уменьшите масштаб коэффициентов (разделите все три на одно число).");
            // Decimal input is stored in binary floating-point. Treat an error at the
            // scale of a few floating-point ULPs as zero (double-root case).
            double roundoff = 16.0 * 2.220446049250313e-16 *
                Math.Max(Math.Abs(bSquared), Math.Abs(fourAC));
            if (Math.Abs(d) <= roundoff) d = 0;

            double vertexX = (-b / a) / 2.0;
            double vertexY = Math.FusedMultiplyAdd(a * vertexX + b, vertexX, c);
            if (!double.IsFinite(vertexX) || !double.IsFinite(vertexY))
                throw new ArithmeticException("Вершину параболы невозможно представить типом double. Нормализуйте коэффициенты.");

            if (d > 0)
            {
                double sqrtD = Math.Sqrt(d);
                // Stable equivalent of the quadratic formula: avoids subtracting close values.
                double q = -0.5 * (b + Math.CopySign(sqrtD, b));
                double x1 = q / a;
                double x2 = c / q;
                if (!double.IsFinite(x1) || !double.IsFinite(x2))
                    throw new ArithmeticException("Корни вышли за диапазон double. Нормализуйте коэффициенты.");
                if (x1 > x2) (x1, x2) = (x2, x1);

                return new QuadraticEquationSolution
                {
                    A = a, B = b, C = c, Discriminant = d,
                    Kind = QuadraticEquationKind.TwoReal,
                    Root1Real = x1, Root2Real = x2,
                    VertexX = vertexX, VertexY = vertexY
                };
            }

            if (d == 0)
            {
                return new QuadraticEquationSolution
                {
                    A = a, B = b, C = c, Discriminant = d,
                    Kind = QuadraticEquationKind.OneReal,
                    Root1Real = vertexX, Root2Real = vertexX,
                    VertexX = vertexX, VertexY = vertexY
                };
            }

            double imaginary = Math.Sqrt(-d) / (2.0 * Math.Abs(a));
            if (!double.IsFinite(imaginary))
                throw new ArithmeticException("Комплексные корни вышли за диапазон double. Нормализуйте коэффициенты.");
            return new QuadraticEquationSolution
            {
                A = a, B = b, C = c, Discriminant = d,
                Kind = QuadraticEquationKind.TwoComplex,
                Root1Real = vertexX, Root1Imaginary = imaginary,
                Root2Real = vertexX, Root2Imaginary = -imaginary,
                VertexX = vertexX, VertexY = vertexY
            };
        }
    }
}
