namespace RandomVariable.Tokens.Entities
{
    using RandomVariable.Tokens.Enums;

    using System;
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value) => (Type, Value) = (type, value);

        public override bool Equals(object obj) => obj is Token token && Type == token.Type && Value == token.Value;

        public override int GetHashCode() => HashCode.Combine(Type, Value);

        public override string ToString() => $"Token {{ Type = {Type}, Value = {Value} }}";
    }

}
