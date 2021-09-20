namespace RandomVariable.Tokens.Entities
{
    using RandomVariable.Tokens.Enums;
    public class CloseBracket : Token
    {
        public const string Bracket = ")";
        public CloseBracket() : base(TokenType.CloseBracket, Bracket) { }
        public static bool Is(string v) => v == Bracket;
    }

}
