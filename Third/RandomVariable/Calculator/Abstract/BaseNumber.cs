namespace RandomVariable.Calculator.Abstract
{
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.Tokens.Enums;
    public abstract class BaseNumber<T> : INumber<T> where T : ICalc<T>
    {
        public double Value { get; }
        public BaseNumber(double value) => Value = value;

        public abstract T Get();
        public abstract ICalc<T> ApplyReversed(INumber<T> number, Operators op);
        public abstract ICalc<T> ApplyReversed(T value, Operators op);
        public abstract ICalc<T> Apply(ICalc<T> calc, Operators op);
    }
}
