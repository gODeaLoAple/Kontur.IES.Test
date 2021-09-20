namespace RandomVariable.Variance.Calculator
{
    using RandomVariable.Calculator.Extensions;
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.Tokens.Enums;
    using RandomVariable.Variance.Operands;

    using System;

    using Number = RandomVariable.Calculator.Interfaces.INumber<Operands.CalculatedValue>;
    public static class Calculator
    {
        public static ICalc<CalculatedValue> Apply(Number x, Number y, Operators op) => new Operands.Number(ApplyNumbers(x.Value, y.Value, op));

        public static ICalc<CalculatedValue> Apply(CalculatedValue x, CalculatedValue y, Operators op) => ApplyCalculatedValues(x.Value, y.Value, op);

        public static ICalc<CalculatedValue> Apply(Number x, CalculatedValue y, Operators op) => ApplyNumberAndCalculated(x.Value, y.Value, op);

        public static ICalc<CalculatedValue> Apply(CalculatedValue x, Number y, Operators op) => ApplyCalculatedAndNumber(x.Value, y.Value, op);

        public static double Calculate(int x, int y) => (y * y - 1) * x / 12.0;

        private static ICalc<CalculatedValue> ApplyCalculatedValues(double x, double y, Operators op)
        {
            if (op == Operators.Divide || op == Operators.Multiply)
                throw new InvalidOperationException();
            return new CalculatedValue(ApplyNumbers(x, y, op));
        }

        private static ICalc<CalculatedValue> ApplyNumberAndCalculated(double number, double calculated, Operators op)
        {
            var result = op switch
            {
                Operators.Multiply => op.Apply(number * number, calculated),
                Operators.Divide => throw new InvalidOperationException(),
                _ => ApplyNumbers(0, calculated, op)
            };
            return new CalculatedValue(result);
        }

        private static ICalc<CalculatedValue> ApplyCalculatedAndNumber(double calculated, double number, Operators op)
        {
            var result = op switch
            {
                Operators.Multiply => op.Apply(number * number, calculated),
                Operators.Divide => op.Apply(calculated, number * number),
                _ => ApplyNumbers(calculated, 0, op)
            };
            return new CalculatedValue(result);
        }

        private static double ApplyNumbers(double x, double y, Operators op)
        {
            return op switch
            {
                Operators.Minus => Operators.Plus.Apply(x, y),
                _ => op.Apply(x, y)
            };
        }
    }
}
