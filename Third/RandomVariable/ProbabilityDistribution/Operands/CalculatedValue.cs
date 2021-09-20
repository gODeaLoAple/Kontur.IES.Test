namespace RandomVariable.ProbabilityDistribution.Operands
{
    using RandomVariable.Calculator.Abstract;
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.Tokens.Enums;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class CalculatedValue : BaseCalculatedValue<CalculatedValue>
    {
        public Dictionary<double, int> Counter { get; }
        public double Multiplier { get; }
        public double Offset { get; }

        public CalculatedValue(Dictionary<double, int> counter, double multiplier = 1, double offset = 0)
        {
            Counter = counter;
            Multiplier = multiplier;
            Offset = offset;
        }

        public Dictionary<double, int> Calculate()
        {
            if (Multiplier == 1 && Offset == 0)
                return Counter;
            return Counter.ToDictionary(x => x.Key * Multiplier + Offset, x => x.Value);
        }

        public Dictionary<double, double> CalculateProbability()
        {
            var counter = Calculate();
            var total = counter.Sum(x => x.Value);
            return counter.ToDictionary(x => x.Key, x => Convert.ToDouble(x.Value) / total);
        }

        public override CalculatedValue Get() => this;

        public override ICalc<CalculatedValue> ApplyReversed(INumber<CalculatedValue> number, Operators op) => Calculator.Calculator.Apply(number, this, op);

        public override ICalc<CalculatedValue> ApplyReversed(CalculatedValue value, Operators op) => Calculator.Calculator.Apply(value, this, op);

        public override ICalc<CalculatedValue> Apply(ICalc<CalculatedValue> calc, Operators op) => calc.ApplyReversed(this, op);
    }
}
