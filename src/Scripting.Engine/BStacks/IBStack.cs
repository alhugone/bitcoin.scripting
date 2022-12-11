using System.Collections.Generic;
using Scripting.Engine.Number;

namespace Scripting.Engine;

public interface IBStack : IReadonlyBStack
{
    void Push(ScriptNumber number);
    void Push(IReadOnlyList<byte> bytes);
    IReadOnlyList<byte> Pop();
}