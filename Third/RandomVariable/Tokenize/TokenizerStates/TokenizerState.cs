namespace RandomVariable.Tokenize.TokenizerStates
{
    using RandomVariable.Tokens.Entities;
    public abstract class TokenizerState
    {
        public virtual Token Token => null;
        public virtual bool ShouldReadAgain => false;
        public TokenizerState() { }

        public abstract TokenizerState Read(char sym);

    }
}
