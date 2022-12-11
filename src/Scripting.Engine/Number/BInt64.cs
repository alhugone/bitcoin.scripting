using System;
using System.Collections.Generic;
using BtcScript.Compiler.Compiling.TokenCompilers;

namespace Scripting.Engine.Number;

public class BInt64 : ScriptNumber
{
    private const int Max = 8;

    public BInt64(IReadOnlyList<byte> raw) : this(IntegerSerializer.Deserialize(raw))
    {
        if (raw.Count > Max)
            throw new ArgumentOutOfRangeException();
    }

    public BInt64(long value) : base(IntegerSerializer.Serialize(value))
    {
        if (value == long.MinValue)
            throw new ArgumentOutOfRangeException(
                nameof(value),
                value,
                $"Range <{long.MinValue + 1};{long.MaxValue}>"
            );
        Value = value;
    }

    public long Value { get; set; }
}