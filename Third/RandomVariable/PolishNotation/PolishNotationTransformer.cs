namespace RandomVariable.PolishNotation
{
    using RandomVariable.PolishNotation.Interfaces;
    using RandomVariable.Tokens.Entities;
    using RandomVariable.Tokens.Enums;

    using System;
    using System.Collections.Generic;
    public class PolishNotationTransformer : IPolishNotationTransformer
    {
        public IEnumerable<Token> Transform(IEnumerable<Token> tokens)
        {
            var opStack = new Stack<Token>();
            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Variable:
                    case TokenType.Number:
                        yield return token;
                        break;
                    case TokenType.Operator:
                    case TokenType.UnaryOperator:
                        foreach (var t in HandleOperator(token, opStack))
                        {
                            yield return t;
                        }
                        opStack.Push(token);
                        break;
                    case TokenType.OpenBracket:
                        opStack.Push(token);
                        break;
                    case TokenType.CloseBracket:
                        foreach (var t in HandleCloseBracket(opStack))
                        {
                            yield return t;
                        }
                        break;
                }
            }

            foreach (var op in opStack)
            {
                if (op.Type == TokenType.OpenBracket)
                    throw new Exception("Invalid expression: close bracket not found");
                yield return op;
            }
        }

        private static IEnumerable<Token> HandleOperator(Token token, Stack<Token> opStack)
        {
            while (opStack.Count > 0)
            {
                if (GetPriority(opStack.Peek()) < GetPriority(token))
                {
                    break;
                }
                yield return opStack.Pop();
            }
        }

        private static IEnumerable<Token> HandleCloseBracket(Stack<Token> opStack)
        {
            var opsInsideBrackets = 0;
            while (true)
            {
                if (!opStack.TryPop(out var peek))
                    throw new Exception("Invalid expression: open bracket not found");
                if (peek.Type == TokenType.OpenBracket)
                {
                    break;
                }
                opsInsideBrackets++;
                yield return peek;
            }
            if (opsInsideBrackets == 0)
                throw new Exception("Invalid expression: empty brackets");
        }

        private static int GetPriority(Token op)
        {
            if (op.Type == TokenType.UnaryOperator)
                return 4;
            if (Operator.Is(op.Value))
                return Operator.Get(op.Value) switch
                {
                    Operators.Multiply => 3,
                    Operators.Divide => 3,
                    Operators.Plus => 2,
                    Operators.Minus => 2,
                    _ => 0
                };
            if (CloseBracket.Is(op.Value))
                return 1;
            if (OpenBracket.Is(op.Value))
                return 0;
            return 0;
        }
    }
}
