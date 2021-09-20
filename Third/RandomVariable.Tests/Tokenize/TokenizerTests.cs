using Combinatorics.Collections;

using NUnit.Framework;

using RandomVariable.Tokenize;
using RandomVariable.Tokenize.Interfaces;
using RandomVariable.Tokens.Entities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomVariableTests.Tests.Tokenize
{
    [TestFixture]
    public class TokenizerTests
    {
        private readonly ITokenizer _tokenizer = new Tokenizer();

        [TestCase("1.")]
        [TestCase("1d")]
        [TestCase(".")]
        [TestCase(".1")]
        [TestCase("d")]
        [TestCase("d1")]
        [TestCase("1.d")]
        [TestCase("1d.")]
        [TestCase("1.1.1")]
        [TestCase("1.1d1")]
        [TestCase("1d1.1")]
        [TestCase("1d1d1")]
        public void Tokenizer_Should_ThrowException_When_Expression_Incorrect(string expression)
        {
            Assert.Throws<Exception>(() => _tokenizer.Tokenize(expression).ToList());
        }

        [TestCase("+")]
        [TestCase("-")]
        [TestCase("*")]
        [TestCase("/")]
        public void Tokenize_Should_UnaryOperatorsTokens(string op)
        {
            AssertParsedExpression(op, new Token[] { new UnaryOperator(op) });
        }

        [Test]
        public void Tokenize_Should_RecognizeOperatorsCorrectlyTokens()
        {
            Assert.Multiple(() =>
            {
                AssertParsedExpression("(+", new Token[] { new OpenBracket(), new UnaryOperator("+") });
                AssertParsedExpression(" +", new Token[] { new UnaryOperator("+") });
                AssertParsedExpression("+ +", new Token[] { new UnaryOperator("+"), new UnaryOperator("+") });
                AssertParsedExpression("4 + +", new Token[] { new Number("4"), new Operator("+"), new UnaryOperator("+") });
                AssertParsedExpression("4 + 4", new Token[] { new Number("4"), new Operator("+"), new Number("4") });
            });
        }

        [Test]
        public void Tokenize_Should_BracketsTokens()
        {
            AssertParsedExpression("(", new Token[] { new OpenBracket() });
            AssertParsedExpression(")", new Token[] { new CloseBracket() });
        }

        [TestCase("2")]
        [TestCase("2.1")]
        [TestCase("22.1")]
        [TestCase("22.22")]
        public void Tokenize_Should_NumberTokens(string number)
        {
            AssertParsedExpression(number, new[] { new Number(number) });
        }

        [TestCase("2d2")]
        [TestCase("2d22")]
        [TestCase("22d2")]
        [TestCase("22d22")]
        public void Tokenize_Should_VariationTokens(string number)
        {
            AssertParsedExpression(number, new[] { new Variable(number) });
        }

        [Ignore("Too long")]
        [TestCase(2)]
        [TestCase(3)]
        public void Tokenize_Should_Parse_WithNOperators(int opCount)
        {
            foreach (var expression in GenerateExpressionsWithOperationsCount(opCount))
            {
                var strExpression = string.Join(" ", expression.Select(x => x.Value));
                var actual = _tokenizer.Tokenize(strExpression);
                Assert.That(string.Join(" ", actual.Select(x => x.Value)),
                    Is.EquivalentTo(strExpression), () => strExpression);
            }
        }

        private IEnumerable<List<Token>> GenerateExpressionsWithOperationsCount(int count)
        {
            var terms = new Token[]
            {
                new Number("2"),
                new Number("2.1"),
                new Number("22.1"),
                new Number("22.11"),
                new Variable("2d2"),
                new Variable("22d1"),
                new Variable("2d22"),
                new Variable("22d22"),
            };

            var ops = new Token[]
            {
                new Operator("+"),
                new Operator("-"),
                new Operator("/"),
                new Operator("*"),
            };

            var variations = new Variations<Token>(terms, count + 1);
            foreach (var termSequence in variations)
            {
                foreach (var operators in GenerateAllVariants(ops, count))
                {
                    var lst = new List<Token>();
                    lst.Add(termSequence[0]);
                    for (var i = 1; i < termSequence.Count; i++)
                    {
                        lst.Add(operators[i - 1]);
                        lst.Add(termSequence[i]);
                    }
                    yield return lst;
                }
            }
        }

        private IEnumerable<T[]> GenerateAllVariants<T>(T[] items, int count)
        {
            if (count < 1)
                throw new ArgumentException(nameof(count));
            if (count == 1)
            {
                foreach (var item in items)
                {
                    yield return new[] { item };
                }
            }
            else
            {
                foreach (var tail in GenerateAllVariants(items, count - 1))
                {
                    foreach (var head in items)
                    {
                        yield return tail.Prepend(head).ToArray();
                    }
                }
            }
        }

        private void AssertParsedExpression(string expression, Token[] expected)
        {
            var actual = _tokenizer.Tokenize(expression);

            Assert.That(actual, Is.EquivalentTo(expected), () => expression);
        }
    }
}