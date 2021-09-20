namespace RandomVariable.Tokenize.Interfaces
{
    using RandomVariable.Tokens.Entities;

    using System.Collections.Generic;
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(string expr);
    }
}
