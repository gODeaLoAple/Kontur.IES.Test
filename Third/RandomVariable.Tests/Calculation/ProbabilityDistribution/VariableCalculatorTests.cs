using NUnit.Framework;

using RandomVariable.ProbabilityDistribution.Calculator;

using System.Collections.Generic;

namespace RandomVariableTests.Tests.Calculation
{
    [TestFixture]
    public class VariableCalculatorTests
    {

        [TestCase(1, 1, new[]
        {
            1, 1,
        })]
        [TestCase(1, 2, new[]
        {
            1, 1,
            2, 1
        })]
        [TestCase(1, 3, new[]
        {
            1, 1,
            2, 1,
            3, 1
        })]
        [TestCase(2, 1, new[]
        {
            2, 1,
        })]
        [TestCase(2, 2, new[]
        {
            2, 1,
            3, 2,
            4, 1,
        })]
        [TestCase(2, 3, new[]
        {
            2, 1,
            3, 2,
            4, 3,
            5, 2,
            6, 1,
        })]
        [TestCase(2, 4, new[]
        {
            2, 1,
            3, 2,
            4, 3,
            5, 4,
            6, 3,
            7, 2,
            8, 1,
        })]
        [TestCase(3, 2, new[]
        {
            3, 1,
            4, 3,
            5, 3,
            6, 1,
        })]
        [TestCase(3, 3, new[]
        {
            3, 1,
            4, 3,
            5, 6,
            6, 7,
            7, 6,
            8, 3,
            9, 1,
        })]
        [TestCase(3, 4, new[]
        {
            3, 1,
            4, 3,
            5, 6,
            6, 10,
            7, 12,
            8, 12,
            9, 10,
            10, 6,
            11, 3,
            12, 1
        })]
        [TestCase(3, 5, new[]
        {
            3, 1,
            4, 3,
            5, 6,
            6, 10,
            7, 15,
            8, 18,
            9, 19,
            10, 18,
            11, 15,
            12, 10,
            13, 6,
            14, 3,
            15, 1,
        })]
        [TestCase(3, 6, new[]
        {
            3, 1,
            4, 3,
            5, 6,
            6, 10,
            7, 15,
            8, 21,
            9, 25,
            10, 27,
            11, 27,
            12, 25,
            13, 21,
            14, 15,
            15, 10,
            16, 6,
            17, 3,
            18, 1,
        })]
        [TestCase(4, 2, new[]
        {
            4, 1,
            5, 4,
            6, 6,
            7, 4,
            8, 1,
        })]
        [TestCase(4, 3, new[]
        {
            4, 1,
            5, 4,
            6, 10,
            7, 16,
            8, 19,
            9, 16,
            10, 10,
            11, 4,
            12, 1,
        })]
        public void Test(int x, int y, int[] values)
        {
            var expected = new Dictionary<int, int>();
            for (var i = 0; i < values.Length; i += 2)
            {
                expected[values[i]] = values[i + 1];
            }
            var actual = Calculator.CountNumbers(x, y);

            Assert.Multiple(() =>
            {
                foreach (var (key, value) in expected)
                {
                    Assert.That(actual[key], Is.EqualTo(value), () => $"[{key}] => {actual[key]} != {value}");
                }
            });
        }
    }
}