using Combinatorics.Collections;

using NUnit.Framework;

using RandomVariable.Calculator.Interfaces;
using RandomVariable.PolishNotation;
using RandomVariable.PolishNotation.Interfaces;
using RandomVariable.Tokenize;
using RandomVariable.Tokenize.Interfaces;
using RandomVariable.Tokens.Entities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomVariableTests.Tests.Calculation.Commutativity
{
    [TestFixture]
    public abstract class CommutativityTests<T>
        where T : ICalc<T>
    {
        private readonly ITokenizer _tokenizer = new Tokenizer();
        private readonly IPolishNotationTransformer _transformer = new PolishNotationTransformer();
        protected abstract IPolishNotationCalculator<T> Calculator { get; }

        [TestCase(new object[]
        {
            "2 * 1d4 + 5",
            "1d4 * 2 + 5",
            "5 + 2 * 1d4",
            "5 + 1d4 * 2",
        })]
        [TestCase(new object[]
        {
            "1d4 + 0",
            "0 + 1d4",
            "1d4",
        })]
        [TestCase(new object[]
        {
            "(1d4 + 1d4) * 3",
            "3 * (1d4 + 1d4)",
            "(1d4 * 3 + 1d4 * 3)",
        })]
        public void Assert_SemanticalEquals_ShouldEquals(params string[] expressions)
        {
            expressions
                .Select(x => Calculator.Calculate(TokenizeAndToPolishNotation(x)))
                .Aggregate((prev, curr) =>
                {
                    AssertAreEqual(prev, curr);
                    return prev;
                });
        }

        [TestCase(new object[] { "*", "2", "1d20" })]
        [TestCase(new object[] { "*", "2", "3", "1d20" })]
        [TestCase(new object[] { "*", "2", "3", "4", "1d20" })]
        [TestCase(new object[] { "*", "2", "3", "4", "5", "1d20" })]
        [TestCase(new object[] { "+", "2", "1d20" })]
        [TestCase(new object[] { "+", "2", "3", "1d20" })]
        [TestCase(new object[] { "+", "2", "3", "4", "1d20" })]
        [TestCase(new object[] { "+", "2", "3", "4", "5", "1d20" })]
        [TestCase(new object[] { "+", "2", "1d20", "1d20" })]
        [TestCase(new object[] { "+", "2", "2", "1d20", "1d20" })]
        public void Assert_Commutativity_Auto(params string[] operator_operands)
        {
            var op = operator_operands.First();
            var expressions = new Permutations<string>(operator_operands.Skip(1));
            AssertCommutativity(expressions.Select(x => string.Join(op, x)));
        }

        private void AssertCommutativity(IEnumerable<string> expressions)
        {
            var first = Calculator.Calculate(TokenizeAndToPolishNotation(expressions.First()));
            Assert.Multiple(() =>
            {
                foreach (var other in expressions.Skip(1))
                {
                    var curr = Calculator.Calculate(TokenizeAndToPolishNotation(other));
                    AssertAreEqual(first, curr);
                }
            });
        }

        protected abstract void AssertAreEqual(T actual, T expected);


        protected IEnumerable<Token> TokenizeAndToPolishNotation(string expression)
        {
            var tokens = _tokenizer.Tokenize(expression);
            return _transformer.Transform(tokens);
        }
    }
}
