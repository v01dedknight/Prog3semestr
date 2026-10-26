using System.Globalization;
using System.Text.RegularExpressions;

namespace Lab0_Task1_Programming.Controls
{
    /// <summary>
    /// Parses a mathematical expression once into a reusable function of x.
    /// Expressions use normal operator precedence and right-associative powers.
    /// </summary>
    internal sealed class MathExpression
    {
        private readonly string _text;
        private readonly Func<double, double> _evaluate;
        private int _position;

        public string DisplayFormula => _text;
        public string InterpretationNote { get; }

        public MathExpression(string formula)
        {
            if (string.IsNullOrWhiteSpace(formula))
                throw new FormatException("Введите формулу f(x).");

            string original = formula.Trim();
            int equals = original.IndexOf('=');
            if (equals >= 0)
            {
                string prefix = original[..equals].Replace(" ", "").ToLowerInvariant();
                if (prefix != "f(x)" && prefix != "y")
                    throw new FormatException("Перед '=' допускается только f(x) или y.");
                original = original[(equals + 1)..];
            }

            original = original.Replace("Math.", "", StringComparison.OrdinalIgnoreCase)
                .Replace('×', '*').Replace('÷', '/')
                .Replace('−', '-').Replace('π', 'π')
                .Replace(',', '.');

            // The informal notation e^-x/3 is commonly intended to mean e^(-x/3),
            // although a strict left-to-right parser reads e^(-x)/3. Resolve that
            // notation explicitly, and make the chosen interpretation visible.
            string adjusted = Regex.Replace(original,
                @"\be\s*\^\s*([+-])\s*(x|(?:\d+(?:\.\d+)?))\s*/\s*(x|(?:\d+(?:\.\d+)?))\b",
                m => $"exp({m.Groups[1].Value}{m.Groups[2].Value}/{m.Groups[3].Value})",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            _text = adjusted;
            InterpretationNote = original == adjusted ? string.Empty :
                "Неоднозначная запись экспоненты уточнена автоматически. Вычисляется f(x) = " + adjusted +
                ". Для однозначного ввода используйте exp(-x/3) или e^(-x/3).";

            _evaluate = ParseExpression();
            SkipWhitespace();
            if (_position < _text.Length)
                throw Error($"Лишний символ '{_text[_position]}'.");
        }

        public double Evaluate(double x) => _evaluate(x);

        private Func<double, double> ParseExpression()
        {
            var left = ParseTerm();
            while (true)
            {
                if (Eat('+')) { var a = left; var b = ParseTerm(); left = x => a(x) + b(x); }
                else if (Eat('-')) { var a = left; var b = ParseTerm(); left = x => a(x) - b(x); }
                else return left;
            }
        }

        private Func<double, double> ParseTerm()
        {
            var left = ParseUnary();
            while (true)
            {
                if (Eat('*')) { var a = left; var b = ParseUnary(); left = x => a(x) * b(x); }
                else if (Eat('/')) { var a = left; var b = ParseUnary(); left = x => a(x) / b(x); }
                else return left;
            }
        }

        private Func<double, double> ParseUnary()
        {
            if (Eat('+')) return ParseUnary();
            if (Eat('-')) { var value = ParseUnary(); return x => -value(x); }
            return ParsePower();
        }

        private Func<double, double> ParsePower()
        {
            var left = ParsePrimary();
            if (Eat('^'))
            {
                var exponent = ParseUnary(); // right associative: 2^3^2 = 2^(3^2)
                return x => Math.Pow(left(x), exponent(x));
            }
            return left;
        }

        private Func<double, double> ParsePrimary()
        {
            if (Eat('('))
            {
                var inner = ParseExpression();
                Require(')');
                return inner;
            }

            SkipWhitespace();
            if (_position < _text.Length && (char.IsDigit(_text[_position]) || _text[_position] == '.'))
            {
                double number = ReadNumber();
                return _ => number;
            }

            if (_position >= _text.Length || (!char.IsLetter(_text[_position]) && _text[_position] != 'π'))
                throw Error("Ожидалось число, x, функция или скобка.");

            string name = ReadIdentifier().ToLowerInvariant();
            if (name == "x") return x => x;
            if (name == "e") return _ => Math.E;
            if (name is "pi" or "π") return _ => Math.PI;

            Require('(');
            var arg = ParseExpression();
            Require(')');
            return name switch
            {
                "sin" => x => Math.Sin(arg(x)),
                "cos" => x => Math.Cos(arg(x)),
                "tan" or "tg" => x => Math.Tan(arg(x)),
                "asin" => x => Math.Asin(arg(x)),
                "acos" => x => Math.Acos(arg(x)),
                "atan" or "arctan" => x => Math.Atan(arg(x)),
                "sqrt" => x => Math.Sqrt(arg(x)),
                "abs" => x => Math.Abs(arg(x)),
                "exp" => x => Math.Exp(arg(x)),
                "ln" or "log" => x => Math.Log(arg(x)),
                "log10" => x => Math.Log10(arg(x)),
                "floor" => x => Math.Floor(arg(x)),
                "ceil" or "ceiling" => x => Math.Ceiling(arg(x)),
                "round" => x => Math.Round(arg(x)),
                "sinh" => x => Math.Sinh(arg(x)),
                "cosh" => x => Math.Cosh(arg(x)),
                "tanh" => x => Math.Tanh(arg(x)),
                "sign" => x => Math.Sign(arg(x)),
                _ => throw Error($"Неизвестная математическая функция '{name}'.")
            };
        }

        private double ReadNumber()
        {
            int start = _position;
            bool hasDigits = false;
            while (_position < _text.Length && char.IsDigit(_text[_position])) { hasDigits = true; _position++; }
            if (_position < _text.Length && _text[_position] == '.')
            {
                _position++;
                while (_position < _text.Length && char.IsDigit(_text[_position])) { hasDigits = true; _position++; }
            }
            if (!hasDigits) throw Error("Некорректное число.");

            if (_position < _text.Length && (_text[_position] == 'e' || _text[_position] == 'E'))
            {
                int saved = _position;
                int pos = saved + 1;
                if (pos < _text.Length && (_text[pos] == '+' || _text[pos] == '-')) pos++;
                if (pos < _text.Length && char.IsDigit(_text[pos]))
                {
                    pos++;
                    while (pos < _text.Length && char.IsDigit(_text[pos])) pos++;
                    _position = pos;
                }
            }

            string token = _text[start.._position];
            if (!double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out double result) || !double.IsFinite(result))
                throw Error($"Недопустимое число '{token}'.");
            return result;
        }

        private string ReadIdentifier()
        {
            int start = _position;
            while (_position < _text.Length && (char.IsLetterOrDigit(_text[_position]) || _text[_position] == '_')) _position++;
            return _text[start.._position];
        }

        private bool Eat(char character)
        {
            SkipWhitespace();
            if (_position >= _text.Length || _text[_position] != character) return false;
            _position++;
            return true;
        }
        private void Require(char character)
        {
            if (!Eat(character)) throw Error($"Ожидался символ '{character}'.");
        }
        private void SkipWhitespace()
        {
            while (_position < _text.Length && char.IsWhiteSpace(_text[_position])) _position++;
        }
        private FormatException Error(string message) =>
            new($"Ошибка формулы в позиции {_position + 1}: {message} Проверьте скобки и знаки операций.");
    }
}
