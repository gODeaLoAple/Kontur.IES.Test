namespace RandomVariable.Calculator.Extensions
{
    using RandomVariable.Calculator.Exceptions;
    using RandomVariable.Tokens.Enums;
    public static class OperatorsExtensions
    {
        public static double Apply(this Operators op, double x, double y)
        {
            return op switch
            {
                Operators.Plus => x + y,
                Operators.Minus => x - y,
                Operators.Multiply => x * y,
                Operators.Divide => x / y,
                _ => throw new OperatorNotFoundException(op),
            };
        }
    }
}
