using System;
using System.Collections.Generic;

namespace Scripting.OpCodes.Number
{
    public class BInt64 : ScriptNumber
    {
        private const int Max = 8;

        public BInt64(IReadOnlyList<byte> raw) : base(raw)
        {
            if (raw.Count > Max)
                throw new ArgumentOutOfRangeException();
        }

        public BInt64(long value) : this(BitConverter.GetBytes(value))
        {
        }
    }
}