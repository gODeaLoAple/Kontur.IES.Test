namespace RandomVariable.PolishNotation.Interfaces
{
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.Tokens.Entities;

    using System.Collections.Generic;
    public interface IPolishNotationCalculator<T> where T : ICalc<T>
    {
        public T Calculate(IEnumerable<Token> polishNotation);
    }
}
