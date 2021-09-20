namespace RandomVariable.Tokens.Entities
{
    using RandomVariable.Tokens.Enums;

    using System;
    using System.Collections.Generic;
    public class Operator : Token
    {
        private static readonly Dictionary<string, Operators> Operators = new()
        {
            ["+"] = Enums.Operators.Plus,
            ["-"] = Enums.Operators.Minus,
            ["*"] = Enums.Operators.Multiply,
            ["/"] = Enums.Operators.Divide,
        };

        public Operator(string value) : base(TokenType.Operator, value) { }

        public static bool Is(string value) => Operators.ContainsKey(value);

        public static Operators Get(string value)
        {
            if (!Operators.ContainsKey(value))
                throw new Exception($"Unexpected operator {value}");
            return Operators[value];
        }
    }

}
