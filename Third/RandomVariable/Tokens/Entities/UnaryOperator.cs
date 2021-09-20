namespace RandomVariable.Tokens.Entities
{
    using RandomVariable.Tokens.Enums;
    public class UnaryOperator : Token
    {
        public UnaryOperator(string value) : base(TokenType.UnaryOperator, value) { }
    }
}
