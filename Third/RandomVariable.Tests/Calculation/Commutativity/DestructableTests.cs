
using NUnit.Framework;

using RandomVariable.PolishNotation;
using RandomVariable.PolishNotation.Interfaces;
using RandomVariable.Tokenize;
using RandomVariable.Tokenize.Interfaces;
using RandomVariable.Tokens.Entities;

using System;
using System.Collections.Generic;

namespace RandomVariableTests.Tests.Calculation.Commutativity
{

    [TestFixture]
    public abstract class DestructableTests
    {
        private readonly ITokenizer _tokenizer = new Tokenizer();
        private readonly IPolishNotationTransformer _transformer = new PolishNotationTransformer();
        [TestCase("1d4 * 1d4")]
        [TestCase("1d4 / 1d4")]
        [TestCase("2 / 1d4")]
        [TestCase("1d4 * 2 * 1d4")]
        [TestCase("2 * 1d4 * 1d4")]
        [TestCase("1d4 * 1d4 * 2")]
        [TestCase("2 * 1d4 / 1d4")]
        public void Should_Throw_Excpetion(string expression)
        {
            var tokens = _tokenizer.Tokenize(expression);
            var notation = _transformer.Transform(tokens);
            Assert.Throws<InvalidOperationException>(() =>
            {
                Calculate(notation);
            });
        }

        protected abstract void Calculate(IEnumerable<Token> polishNotation);
    }
}
