namespace RandomVariable.Tokenize
{
    using RandomVariable.Tokenize.Interfaces;
    using RandomVariable.Tokenize.TokenizerStates;
    using RandomVariable.Tokens.Entities;

    using System.Collections.Generic;
    using System.Linq;
    public class Tokenizer : ITokenizer
    {
        public IEnumerable<Token> Tokenize(string expr)
        {
            TokenizerState state = new EmptyState();
            foreach (var sym in expr.Append(' '))
            {
                do
                {
                    state = state.Read(sym);
                    if (state.Token != null)
                    {
                        yield return state.Token;
                    }
                } while (state.ShouldReadAgain);
            }
        }
    }
}
