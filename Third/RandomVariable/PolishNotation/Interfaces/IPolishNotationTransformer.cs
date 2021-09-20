namespace RandomVariable.PolishNotation.Interfaces
{
    using RandomVariable.Tokens.Entities;
    using System.Collections.Generic;
    public interface IPolishNotationTransformer
    {
        public IEnumerable<Token> Transform(IEnumerable<Token> tokens);
    }
}
