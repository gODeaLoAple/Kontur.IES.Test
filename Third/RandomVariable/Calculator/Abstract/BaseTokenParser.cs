namespace RandomVariable.Calculator.Abstract
{
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.Tokens.Entities;
    using RandomVariable.Tokens.Enums;

    using System;
    public abstract class BaseTokenParser<T> : ITokenParser<T> where T : ICalc<T>
    {
        public ICalc<T> FromToken(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Number:
                    return CreateNumber(Number.Parse(token.Value));
                case TokenType.Variable:
                    {
                        var parts = token.Value.Split(Variable.Separator);
                        var x = int.Parse(parts[0]);
                        ValidateVariablePart(x);
                        var y = int.Parse(parts[1]);
                        ValidateVariablePart(y);
                        return CreateVariable(x, y);
                    }
                default:
                    throw new Exception();
            }
        }

        public INumber<T> GetZero() => CreateNumber(0);

        protected abstract INumber<T> CreateNumber(double value);
        protected abstract ICalc<T> CreateVariable(int x, int y);

        private static void ValidateVariablePart(int number)
        {
            const int minValue = 0;
            const int maxValue = 100;
            if (number < minValue || number > maxValue)
                throw new Exception($"Expected variable part in range from {minValue} to {maxValue}");
        }
    }


}
