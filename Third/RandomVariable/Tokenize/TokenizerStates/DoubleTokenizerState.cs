namespace RandomVariable.Tokenize.TokenizerStates
{
    using RandomVariable.Tokens.Entities;

    using System.Text;
    public class DoubleTokenizerState : TokenizerState
    {
        private readonly StringBuilder _expression;
        public DoubleTokenizerState(StringBuilder head) => _expression = head;

        public override TokenizerState Read(char sym)
        {
            if (char.IsDigit(sym))
            {
                _expression.Append(sym);
                return this;
            }
            return new NonEmptyTokenizerState(new Number(_expression.ToString()), true);
        }
    }
}
