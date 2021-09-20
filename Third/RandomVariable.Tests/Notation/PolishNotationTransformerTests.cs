
using NUnit.Framework;

using RandomVariable.PolishNotation;
using RandomVariable.PolishNotation.Interfaces;
using RandomVariable.Tokenize;
using RandomVariable.Tokenize.Interfaces;

namespace RandomVariableTests.Tests.Notation
{


    [TestFixture]
    public class PolishNotationTransformerTests
    {
        private readonly ITokenizer _tokenizer = new Tokenizer();
        private readonly IPolishNotationTransformer _transformer = new PolishNotationTransformer();

        [TestCase("1", "1")]
        [TestCase("1 + 1", "1 1 +")]
        [TestCase("1 + 1 - 1", "1 1 + 1 -")]
        [TestCase("1 + 1 * 1", "1 1 1 * +")]
        [TestCase("(1 + 1) * 1", "1 1 + 1 *")]
        [TestCase("- (1 + 1) * 1", "1 1 + - 1 *")]
        [TestCase("(-1 * 1) * 1", "1 - 1 * 1 *")]
        [TestCase("(1 + 2) * (3 + 4) - 5", "1 2 + 3 4 + * 5 -")]
        public void Assert_NotationIsCorrect(string expression, string expected)
        {
            var tokens = _tokenizer.Tokenize(expression);
            var actual = _transformer.Transform(tokens);
            Assert.That(TokensUtils.GetExpression(actual), Is.EqualTo(expected), () => expression);
        }
    }
}