namespace RandomVariable.Tokens.Entities
{
    using RandomVariable.Tokens.Enums;
    public class Variable : Token
    {
        public const char Separator = 'd';

        public Variable(string value) : base(TokenType.Variable, value) { }
    }

}
