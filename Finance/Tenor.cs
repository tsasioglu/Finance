using System;
using System.Text.RegularExpressions;

namespace Finance
{
    public class Tenor
    {
        private const string RegexPattern = @"^(?<Value>[0-9]+)(?<Unit>[DWMY])$";

        public string    Name  { get; set; }
        public int       Value { get; set; }
        public TenorUnit Unit  { get; set; }

        public Tenor(string name)
        {
            if (name == null)                    throw new ArgumentNullException("name", "Must not be null. ");
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException("Must not be empty or white space", "name");

            Match match = new Regex(RegexPattern).Match(name);
            if (!match.Success)
                throw new ArgumentException(String.Format("Not a match for a valid tenor. Must match '{0}'. ", RegexPattern), "name");

            Name  = name;
            Value = int.Parse(match.Groups["Value"].Value);
            Unit  = ParseUnit(match.Groups["Unit"].Value);
        }

        private static TenorUnit ParseUnit(string unit)
        {
            switch (unit)
            {
                case "D":
                    return TenorUnit.Days;
                case "W":
                    return TenorUnit.Weeks;
                case "M":
                    return TenorUnit.Months;
                case "Y":
                    return TenorUnit.Years;
                default:
                    throw new ArgumentException(String.Format("Unable to parse '{0}'. ", unit), "unit");
            }
        }

        public int Days
        {
            get
            {
                switch (Unit)
                {
                    case TenorUnit.Days:
                        return Value;
                    case TenorUnit.Weeks:
                        return Value * 7;
                    case TenorUnit.Months:
                        return Value * 30;
                    case TenorUnit.Years:
                        return Value*365;
                    default:
                        return 0;
                }
            }
        }

    }

    public enum TenorUnit
    {
        Days,
        Weeks,
        Months,
        Years,
    }
}
