namespace RandomVariable.Tokens.Entities
{
    using RandomVariable.Tokens.Enums;

    using System.Globalization;

    public class Number : Token
    {
        public static string Separator => CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;

        public Number(string value) : base(TokenType.Number, value) { }

        public static double Parse(string value) => double.Parse(value, CultureInfo.InvariantCulture);
    }
}
