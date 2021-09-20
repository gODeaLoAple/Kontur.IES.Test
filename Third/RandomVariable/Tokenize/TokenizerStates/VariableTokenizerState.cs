namespace RandomVariable.Tokenize.TokenizerStates
{
    using RandomVariable.Tokens.Entities;

    using System.Text;
    public class VariableTokenizerState : TokenizerState
    {
        private readonly StringBuilder _expression;
        public VariableTokenizerState(StringBuilder head) : base() => _expression = head;

        public override TokenizerState Read(char sym)
        {
            if (char.IsDigit(sym))
            {
                _expression.Append(sym);
                return this;
            }
            else
            {
                return new NonEmptyTokenizerState(new Variable(_expression.ToString()), true);
            }
        }
    }
}
