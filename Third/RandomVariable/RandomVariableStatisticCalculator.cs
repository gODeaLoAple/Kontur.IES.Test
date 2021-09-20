namespace RandomVariable
{
    using RandomVariable.PolishNotation;
    using RandomVariable.PolishNotation.Interfaces;
    using RandomVariable.Tokenize;
    using RandomVariable.Tokenize.Interfaces;

    using System;
    using System.Linq;
    public class RandomVariableStatisticCalculator : IRandomVariableStatisticCalculator
    {
        private readonly ITokenizer _tokenizer;
        private readonly IPolishNotationTransformer _transformer;

        public RandomVariableStatisticCalculator()
        {
            _tokenizer = new Tokenizer();
            _transformer = new PolishNotationTransformer();
        }

        public RandomVariableStatistic CalculateStatistic(string expression, params StatisticKind[] statisticForCalculate)
        {
            var tokens = _tokenizer.Tokenize(expression);
            var notation = _transformer.Transform(tokens).ToList();

            var result = new RandomVariableStatistic();
            if (statisticForCalculate.Contains(StatisticKind.ExpectedValue))
            {
                var parser = new ExpectedValue.TokenParser.Parser();
                var calculator = new PolishNotationCalculator<ExpectedValue.Operands.CalculatedValue>(parser);
                result.ExpectedValue = calculator.Calculate(notation).Value;
            }
            if (statisticForCalculate.Contains(StatisticKind.Variance))
            {
                var parser = new Variance.TokenParser.Parser();
                var calculator = new PolishNotationCalculator<Variance.Operands.CalculatedValue>(parser);
                result.Variance = calculator.Calculate(notation).Value;
            }
            if (statisticForCalculate.Contains(StatisticKind.ProbabilityDistribution))
            {
                var parser = new ProbabilityDistribution.TokenParser.Parser();
                var calculator = new PolishNotationCalculator<ProbabilityDistribution.Operands.CalculatedValue>(parser);
                result.ProbabilityDistribution = calculator.Calculate(notation).CalculateProbability();
            }
            return result;
        }
    }
}