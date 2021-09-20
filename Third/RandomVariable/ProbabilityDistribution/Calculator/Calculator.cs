namespace RandomVariable.ProbabilityDistribution.Calculator
{
    using RandomVariable.Calculator.Exceptions;
    using RandomVariable.Calculator.Extensions;
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.ProbabilityDistribution.Operands;
    using RandomVariable.Tokens.Enums;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Number = RandomVariable.Calculator.Interfaces.INumber<Operands.CalculatedValue>;
    public static class Calculator
    {
        public static ICalc<CalculatedValue> Apply(CalculatedValue value, Number number, Operators op)
        {
            return op switch
            {
                Operators.Plus => new CalculatedValue(value.Counter, value.Multiplier, value.Offset + number.Value),
                Operators.Minus => new CalculatedValue(value.Counter, value.Multiplier, value.Offset - number.Value),
                Operators.Multiply => new CalculatedValue(value.Counter, value.Multiplier * number.Value, value.Offset),
                Operators.Divide => new CalculatedValue(value.Counter, value.Multiplier / number.Value, value.Offset),
                _ => throw new OperatorNotFoundException(op),
            };
        }

        public static ICalc<CalculatedValue> Apply(CalculatedValue value, CalculatedValue calc, Operators op)
        {
            if (op == Operators.Divide || op == Operators.Multiply)
                throw new InvalidOperationException();
            return new CalculatedValue(Merge(value.Calculate(), calc.Calculate(), (x, y) => op.Apply(x, y)));
        }

        public static ICalc<CalculatedValue> Apply(Number number, Number variable, Operators op)
            => new Operands.Number(op.Apply(number.Value, variable.Value));

        public static ICalc<CalculatedValue> Apply(Number number, CalculatedValue counter, Operators op)
        {
            return op switch
            {
                Operators.Plus => new CalculatedValue(counter.Counter, counter.Multiplier, counter.Offset + number.Value),
                Operators.Minus => new CalculatedValue(counter.Counter, -counter.Multiplier, counter.Offset + number.Value),
                Operators.Multiply => new CalculatedValue(counter.Counter, counter.Multiplier * number.Value, counter.Offset),
                Operators.Divide => throw new InvalidOperationException(),
                _ => throw new OperatorNotFoundException(op),
            };
        }

        public static Dictionary<double, int> CountNumbers(int x, int y)
        {
            if (x < 1)
                throw new ArgumentException(nameof(x));
            if (x == 1)
            {
                return Enumerable.Range(1, y).ToDictionary<int, double, int>(x => x, x => 1);
            }
            var less = CountNumbers(x - 1, y);
            var res = new Dictionary<double, int>();
            foreach (var (key, value) in less)
            {
                foreach (var k in Enumerable.Range(1, y).Select(z => key + z))
                {
                    res[k] = res.GetValueOrDefault(k, 0) + value;
                }
            }
            return res;
        }

        private static Dictionary<double, int> Merge(Dictionary<double, int> counter, Dictionary<double, int> other, Func<double, double, double> keyMapper)
        {
            var res = new Dictionary<double, int>();
            foreach (var first in counter)
            {
                foreach (var second in other)
                {
                    var key = keyMapper.Invoke(first.Key, second.Key);
                    res[key] = res.GetValueOrDefault(key, 0) + first.Value * second.Value;
                }
            }
            return res;
        }
    }
}
