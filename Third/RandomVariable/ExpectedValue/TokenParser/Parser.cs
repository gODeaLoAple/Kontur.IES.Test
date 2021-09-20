namespace RandomVariable.ExpectedValue.TokenParser
{
    using RandomVariable.Calculator.Abstract;
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.ExpectedValue.Operands;
    public class Parser : BaseTokenParser<CalculatedValue>
    {
        protected override INumber<CalculatedValue> CreateNumber(double value) => new Number(value);

        protected override ICalc<CalculatedValue> CreateVariable(int x, int y) => new CalculatedValue(Calculator.Calculator.CalculateDices(x, y));
    }
}
