using System.Collections.Generic;

namespace Scripting.OpCodes
{
    public interface IReadonlyBStack
    {
        int Count { get; }
        IReadOnlyList<byte> Peek();
    }
}