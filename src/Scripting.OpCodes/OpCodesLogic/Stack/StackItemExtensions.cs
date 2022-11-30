using System.Collections.Generic;

namespace Scripting.OpCodes.OpCodesLogic.Stack
{
    public static class StackItemExtensions
    {
        public static bool AsBool(this IReadOnlyList<byte> item)
        {
            for (var i = 0; i < item.Count; i++)
                if (item[i] != 0)
                {
                    if (i == item.Count - 1 && item[i] == 0x80)
                        return false;
                    return true;
                }
            return false;
        }
    }
}