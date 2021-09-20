
using RandomVariable.Tokens.Entities;

using System.Collections.Generic;
using System.Linq;

namespace RandomVariableTests.Tests
{
    public static class TokensUtils
    {
        public static string GetExpression(IEnumerable<Token> tokens) => string.Join(" ", tokens.Select(x => x.Value));
    }


}