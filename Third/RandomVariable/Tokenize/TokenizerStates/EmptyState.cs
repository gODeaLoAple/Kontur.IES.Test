namespace RandomVariable.Tokenize.TokenizerStates
{
    using RandomVariable.Tokens.Entities;

    using System;
    public class EmptyState : TokenizerState
    {
        private readonly bool _isNextOperatorUnary;

        public EmptyState(bool isNextOperatorUnary = true) : base() => _isNextOperatorUnary = isNextOperatorUnary;

        public override TokenizerState Read(char sym)
        {
            if (char.IsDigit(sym))
            {
                return new IntegerTokenizerState().Read(sym);
            }
            else if (Operator.Is(sym.ToString()))
            {
                return _isNextOperatorUnary
                    ? new NonEmptyTokenizerState(new UnaryOperator(sym.ToString()), false)
                    : new NonEmptyTokenizerState(new Operator(sym.ToString()), false);
            }
            else if (OpenBracket.Is(sym.ToString()))
            {
                return new NonEmptyTokenizerState(new OpenBracket(), false);
            }
            else if (CloseBracket.Is(sym.ToString()))
            {
                return new NonEmptyTokenizerState(new CloseBracket(), false);
            }
            else if (char.IsWhiteSpace(sym))
            {
                return this;
            }
            throw new Exception($"Unexpected char {sym}");
        }

    }
}
