using System;
using System.Collections.Generic;
using System.Linq;
using BtcScript.Compiler.Compiling.TokenCompilers;

namespace Scripting.Engine.Number;

public class BInt32 : ScriptNumber
{
    private const int MaxBytes = 4;
    public const int MaxValue = int.MaxValue;
    public const int MinValue = -int.MaxValue;

    public BInt32(int value) : base(IntegerSerializer.Serialize(value))
    {
        if (value < MinValue)
            throw new ArgumentOutOfRangeException(
                nameof(value),
                value,
                $"Allowed range is <{MinValue};{MaxValue}>"
            );
        Value = value;
    }

    public BInt32(long value) : base(IntegerSerializer.Serialize(value))
    {
        if (value < MinValue)
            throw new ArgumentOutOfRangeException(
                nameof(value),
                value,
                $"Allowed range is <{MinValue};{MaxValue}>"
            );
        Value = (int)value;
    }

    public BInt32(IReadOnlyList<byte> raw) : this(IntegerSerializer.Deserialize(raw))
    {
        if (raw.Count > MaxBytes)
            throw new ArgumentOutOfRangeException(nameof(raw), raw.Count, "BInt32 can be 4 bytes max");
    }

    public int Value { get; set; }
    public static implicit operator int(BInt32 d) => d.ToInt();
    public int ToInt() => Value;

    public ScriptNumber Add(BInt32 bInt32)
    {
        var thisInt = (long)BitConverter.ToInt32(Raw.ToArray());
        var thatInt = (long)BitConverter.ToInt32(bInt32.Raw.ToArray());
        var result = thisInt + thatInt;
        if (result <= int.MaxValue && int.MinValue <= result)
            return new BInt32((int)result);
        return new BInt64(result);
    }
}