namespace RandomVariable.Tokens.Entities
{
    using RandomVariable.Tokens.Enums;
    public class OpenBracket : Token
    {
        public const string Bracket = "(";

        public OpenBracket() : base(TokenType.OpenBracket, Bracket) { }

        public static bool Is(string sym) => Bracket == sym;
    }
}
