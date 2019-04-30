using System;
using System.Collections.Generic;
using System.Text;
using LiteGuard;

namespace HRAssistant.Tests
{
    public static class UniqueHelper
    {
        public static string MakeUnique(string value)
        {
            Guard.AgainstNullArgument(nameof(value), value);

            var uniquePart = Guid.NewGuid()
                .ToString()
                .Substring(0, 5);

            return $"{value}_{uniquePart}";
        }
    }
}
