namespace RandomVariable.Tokenize.TokenizerStates
{
    using RandomVariable.Tokens.Entities;

    using System.Text;
    public class IntegerTokenizerState : TokenizerState
    {
        private readonly StringBuilder _expression = new();
        public IntegerTokenizerState() : base() { }

        public override TokenizerState Read(char sym)
        {
            if (sym.ToString() == Number.Separator)
            {
                return new IntegerWithDotTokenizerState(_expression.Append(sym));
            }
            else if (char.IsDigit(sym))
            {
                _expression.Append(sym);
                return this;
            }
            else if (sym == Variable.Separator)
            {
                return new IntegerWithDTokenizerState(_expression.Append(sym));
            }
            return new NonEmptyTokenizerState(new Number(_expression.ToString()), true);
        }
    }
}
