using System;

namespace Scripting.Engine;

public class OpCodeDecodedInstruction
{
    public OpcodeType Opcode { get; set; }
    public override string ToString() => Opcode.ToString();
    public virtual string ToHex() => Convert.ToHexString(new[] { (byte)Opcode });
}