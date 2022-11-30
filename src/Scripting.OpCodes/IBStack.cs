using System.Collections.Generic;
using Scripting.OpCodes.Number;

namespace Scripting.OpCodes
{
    public interface IBStack : IReadonlyBStack
    {
        void Push(ScriptNumber number);
        void Push(IReadOnlyList<byte> bytes);
        IReadOnlyList<byte> Pop();
        IReadOnlyList<byte> this[int index] { get; }
    }
}
