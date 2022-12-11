using System.Collections.Generic;

namespace Scripting.Engine;

public interface IReadonlyBStack : IEnumerable<IReadOnlyList<byte>>
{
    int Count { get; }
    IReadOnlyList<byte> this[int index] { get; }
    IReadOnlyList<byte> Peek();
}