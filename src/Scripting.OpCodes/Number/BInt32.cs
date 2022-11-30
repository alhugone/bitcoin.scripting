using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripting.OpCodes.Number
{
    public class BInt32 : ScriptNumber
    {
        private const int Max = 4;

        public BInt32(int value) : this(BitConverter.GetBytes(value))
        {
        }

        public BInt32(IReadOnlyList<byte> raw) : base(raw)
        {
            if (raw.Count > Max)
                throw new ArgumentOutOfRangeException();
        }

        public static implicit operator int(BInt32 d) => d.ToInt();
        public int ToInt() => BitConverter.ToInt32(Raw.ToArray());

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
}