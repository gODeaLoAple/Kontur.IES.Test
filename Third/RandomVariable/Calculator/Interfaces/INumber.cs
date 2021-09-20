namespace RandomVariable.Calculator.Interfaces
{
    public interface INumber<T> : ICalc<T> where T : ICalc<T>
    {
        public double Value { get; }
    }
}
