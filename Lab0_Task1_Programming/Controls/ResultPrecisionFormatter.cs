using System.Globalization;

namespace Lab0_Task1_Programming.Controls
{
    // Форматирование только окончательных результатов, без округления вычислений.
    internal static class ResultPrecisionFormatter
    {
        public static string Format(double value, string epsilonText)
        {
            int decimals = GetDecimalPlaces(epsilonText);

            // Убираем «-0,00», если отрицательное число округлилось до нуля.
            double rounded = Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            if (rounded == 0.0)
            {
                rounded = 0.0;
            }

            return rounded.ToString("F" + decimals.ToString(CultureInfo.InvariantCulture),
                CultureInfo.CurrentCulture);
        }

        public static int GetDecimalPlaces(string epsilonText)
        {
            string input = epsilonText.Trim();
            int exponentIndex = input.IndexOfAny(new[] { 'e', 'E' });
            string mantissa = exponentIndex >= 0 ? input[..exponentIndex] : input;
            int exponent = 0;

            if (exponentIndex >= 0 &&
                !int.TryParse(input[(exponentIndex + 1)..], NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out exponent))
            {
                exponent = 0;
            }

            int separatorIndex = mantissa.IndexOfAny(new[] { '.', ',' });
            int fractionDigits = separatorIndex < 0 ? 0 : mantissa.Length - separatorIndex - 1;

            // Примеры: 0,5 -> 1; 0,001 -> 3; 1e-4 -> 4; 2.5e-3 -> 4.
            return (int)Math.Clamp((long)fractionDigits - exponent, 0, 15);
        }
    }
}
