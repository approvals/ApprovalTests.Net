using System;
using System.Collections.Generic;

namespace ApprovalUtilities.Utilities
{
    public class EnumUtilities
    {
        public static IEnumerable<TEnum> GetValues<TEnum>()
        {
            return (TEnum[]) Enum.GetValues(typeof(TEnum));
        }
    }
}
