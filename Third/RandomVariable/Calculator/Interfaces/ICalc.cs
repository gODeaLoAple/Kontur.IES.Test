namespace RandomVariable.Calculator.Interfaces
{
    using RandomVariable.Tokens.Enums;
    public interface ICalc<T> where T : ICalc<T>
    {
        T Get();
        ICalc<T> ApplyReversed(INumber<T> number, Operators op);
        ICalc<T> ApplyReversed(T value, Operators op);
        ICalc<T> Apply(ICalc<T> calc, Operators op);
    }
}
