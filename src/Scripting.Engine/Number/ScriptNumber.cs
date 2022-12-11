using System.Collections.Generic;
using System.Linq;
using BtcScript.Compiler.Compiling.TokenCompilers;

namespace Scripting.Engine.Number;

public class ScriptNumber
{
    protected readonly IReadOnlyList<byte> Raw;
    public ScriptNumber(IReadOnlyList<byte> raw) => Raw = raw;
    public IReadOnlyList<byte> Get() => Raw;

    public override string ToString() =>
        IntegerSerializer.Deserialize(Raw.ToArray()).ToString();
}