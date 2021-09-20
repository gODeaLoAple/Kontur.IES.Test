namespace RandomVariable.PolishNotation
{
    using RandomVariable.Calculator.Interfaces;
    using RandomVariable.PolishNotation.Interfaces;
    using RandomVariable.Tokens.Entities;
    using RandomVariable.Tokens.Enums;

    using System;
    using System.Collections.Generic;
    public class PolishNotationCalculator<T> : IPolishNotationCalculator<T> where T : ICalc<T>
    {
        private readonly ITokenParser<T> _parser;

        public PolishNotationCalculator(ITokenParser<T> parser)
        {
            _parser = parser;
        }

        public T Calculate(IEnumerable<Token> polishNotation)
        {
            var operands = new Stack<ICalc<T>>();
            foreach (var token in polishNotation)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                    case TokenType.Variable:
                        operands.Push(_parser.FromToken(token));
                        break;
                    case TokenType.UnaryOperator:
                        operands.Push(Calculate(operands.Pop(), _parser.GetZero(), Operator.Get(token.Value)));
                        break;
                    case TokenType.Operator:
                        operands.Push(Calculate(operands.Pop(), operands.Pop(), Operator.Get(token.Value)));
                        break;
                    default:
                        throw new Exception("Incorrect expression");
                }
            }
            return operands.Pop().Get();
        }

        private ICalc<T> Calculate(ICalc<T> x, ICalc<T> y, Operators op) => y.Apply(x, op);
    }
}
