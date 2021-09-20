namespace RandomVariable.Calculator.Abstract
{
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.Tokens.Enums;
    public abstract class BaseCalculatedValue<T> : ICalc<T> where T : ICalc<T>
    {
        public abstract ICalc<T> ApplyReversed(INumber<T> number, Operators op);
        public abstract ICalc<T> ApplyReversed(T value, Operators op);
        public abstract ICalc<T> Apply(ICalc<T> calc, Operators op);
        public abstract T Get();
    }
}
