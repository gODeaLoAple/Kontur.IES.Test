namespace RandomVariable.Variance.Operands
{
    using RandomVariable.Calculator.Abstract;
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.Tokens.Enums;
    public class CalculatedValue : BaseCalculatedValue<CalculatedValue>
    {
        public double Value { get; }
        public CalculatedValue(double value) => (Value) = (value);

        public override CalculatedValue Get() => this;

        public override ICalc<CalculatedValue> ApplyReversed(INumber<CalculatedValue> number, Operators op) => Calculator.Calculator.Apply(number, this, op);

        public override ICalc<CalculatedValue> ApplyReversed(CalculatedValue value, Operators op) => Calculator.Calculator.Apply(value, this, op);

        public override ICalc<CalculatedValue> Apply(ICalc<CalculatedValue> calc, Operators op) => calc.ApplyReversed(this, op);
    }
}
