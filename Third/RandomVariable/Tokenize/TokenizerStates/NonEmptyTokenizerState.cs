namespace RandomVariable.Tokenize.TokenizerStates
{
    using RandomVariable.Tokens.Entities;
    using RandomVariable.Tokens.Enums;
    public class NonEmptyTokenizerState : TokenizerState
    {
        public override Token Token { get; }
        public override bool ShouldReadAgain { get; }

        public NonEmptyTokenizerState(Token token, bool shouldRead)
        {
            Token = token;
            ShouldReadAgain = shouldRead;
        }

        public override TokenizerState Read(char sym)
        {
            var isNextOperatorUnary = Token.Type == TokenType.OpenBracket
                || Token.Type == TokenType.UnaryOperator
                || Token.Type == TokenType.Operator;
            return new EmptyState(isNextOperatorUnary).Read(sym);
        }
    }
}
