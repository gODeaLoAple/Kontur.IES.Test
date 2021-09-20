namespace RandomVariable.ExpectedValue.Calculator
{
    using RandomVariable.Calculator.Extensions;
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.ExpectedValue.Operands;
    using RandomVariable.Tokens.Enums;

    using System;

    using Number = RandomVariable.Calculator.Interfaces.INumber<Operands.CalculatedValue>;
    public static class Calculator
    {
        public static double CalculateDices(int x, int y) => (y + 1) * x / 2.0;

        public static ICalc<CalculatedValue> Apply(Number x, Number y, Operators op) => new Operands.Number(op.Apply(x.Value, y.Value));

        public static ICalc<CalculatedValue> Apply(CalculatedValue x, CalculatedValue y, Operators op)
        {
            if (op == Operators.Divide || op == Operators.Multiply)
                throw new InvalidOperationException();
            return new CalculatedValue(op.Apply(x.Value, y.Value));
        }

        public static ICalc<CalculatedValue> Apply(Number x, CalculatedValue y, Operators op)
        {
            if (op == Operators.Divide)
                throw new InvalidOperationException();
            return new CalculatedValue(op.Apply(x.Value, y.Value));
        }

        public static ICalc<CalculatedValue> Apply(CalculatedValue x, Number y, Operators op) => new CalculatedValue(op.Apply(x.Value, y.Value));
    }
}
