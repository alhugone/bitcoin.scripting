using System.Collections.Generic;
using Scripting.OpCodes.Number;

namespace Scripting.OpCodes
{
    public interface IBStack : IReadonlyBStack
    {
        void Push(ScriptNumber number);

        IReadOnlyList<byte> Pop();
    }
}