namespace RandomVariable.Tokenize.TokenizerStates
{
    using System;
    using System.Text;
    public class IntegerWithDotTokenizerState : TokenizerState
    {
        private readonly StringBuilder _expression;
        public IntegerWithDotTokenizerState(StringBuilder head) : base() => _expression = head;

        public override TokenizerState Read(char sym)
        {
            if (char.IsDigit(sym))
                return new DoubleTokenizerState(_expression).Read(sym);
            throw new Exception();
        }
    }
}
