using System;
using System.Globalization;

namespace Churub.Core
{
    // Abbreviates large numbers with K/M/B/T suffixes (e.g. 1500 -> "1.5K").
    // Pure number formatting: no string cutting or string-to-number roundtrip,
    // and the output never depends on the device's culture settings.
    public static class NumberAbbreviator
    {
        private static readonly string[] Suffixes = { "", "K", "M", "B", "T" };
        private const double UnitScale = 1000d;

        public static string Format(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                return value.ToString(CultureInfo.InvariantCulture);

            bool negative = value < 0d;
            double magnitude = negative ? -value : value;

            int unit = 0;
            while (magnitude >= UnitScale && unit < Suffixes.Length - 1)
            {
                magnitude /= UnitScale;
                unit++;
            }

            // Rounding to two decimals can push 999.999 to 1000; promote it to
            // the next suffix instead of printing "1000K".
            if (unit < Suffixes.Length - 1 && Math.Round(magnitude, 2) >= UnitScale)
            {
                magnitude /= UnitScale;
                unit++;
            }

            // A tiny magnitude rounds to "0"; keep it unsigned instead of "-0".
            if (Math.Round(magnitude, 2) == 0d)
            {
                negative = false;
                magnitude = 0d;
            }

            string formatted = magnitude.ToString("0.##", CultureInfo.InvariantCulture);
            return (negative ? "-" : "") + formatted + Suffixes[unit];
        }
    }
}
