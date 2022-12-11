using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripting.Engine;

public class OpCodeWithArgumentsDecodedInstruction : OpCodeDecodedInstruction
{
    public IReadOnlyList<byte[]> StaticArgs { get; set; }

    public override string ToString() => string.Join(
        " ",
        new[] { base.ToString() }.Concat(StaticArgs.Select(Convert.ToHexString))
    );

    public override string ToHex() => string.Join(
        " ",
        new[] { base.ToHex() }.Concat(StaticArgs.Select(Convert.ToHexString))
    );
}
