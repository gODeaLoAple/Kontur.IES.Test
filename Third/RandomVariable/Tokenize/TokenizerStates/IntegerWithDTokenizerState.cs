namespace RandomVariable.Tokenize.TokenizerStates
{
    using System;
    using System.Text;
    public class IntegerWithDTokenizerState : TokenizerState
    {
        private readonly StringBuilder _expression;
        public IntegerWithDTokenizerState(StringBuilder head) : base() => _expression = head;

        public override TokenizerState Read(char sym)
        {
            if (char.IsDigit(sym))
                return new VariableTokenizerState(_expression).Read(sym);
            throw new Exception();
        }
    }
}
