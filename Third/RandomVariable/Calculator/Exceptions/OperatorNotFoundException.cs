namespace RandomVariable.Calculator.Exceptions
{
    using RandomVariable.Tokens.Enums;

    using System;
    public class OperatorNotFoundException : Exception
    {
        public OperatorNotFoundException(Operators op) : base($"Operator {op} not found") { }
    }
}
