namespace RandomVariable.Calculator.Interfaces
{
    using RandomVariable.Tokens.Entities;
    public interface ITokenParser<T> where T : ICalc<T>
    {
        ICalc<T> FromToken(Token token);
        INumber<T> GetZero();
    }
}
