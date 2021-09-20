﻿namespace RandomVariable.ProbabilityDistribution.Operands
{
    using RandomVariable.Calculator.Abstract;
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.Tokens.Enums;

    using System.Collections.Generic;
    public class Number : BaseNumber<CalculatedValue>
    {
        public Number(double value) : base(value) { }

        public override CalculatedValue Get() => new(new Dictionary<double, int>() { [Value] = 1 });

        public override ICalc<CalculatedValue> ApplyReversed(INumber<CalculatedValue> number, Operators op) => Calculator.Calculator.Apply(number, this, op);

        public override ICalc<CalculatedValue> ApplyReversed(CalculatedValue value, Operators op) => Calculator.Calculator.Apply(value, this, op);

        public override ICalc<CalculatedValue> Apply(ICalc<CalculatedValue> calc, Operators op) => calc.ApplyReversed(this, op);
    }
}
