using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripting.OpCodes.Number
{
    public class ScriptNumber
    {
        protected readonly IReadOnlyList<byte> Raw;
        public ScriptNumber(IReadOnlyList<byte> raw) => Raw = raw;
        public IReadOnlyList<byte> Get() => Raw;

        public override string ToString() =>
            Raw.Count == 8
                ? BitConverter.ToInt64(Raw.ToArray()).ToString()
                : BitConverter.ToInt32(Raw.ToArray()).ToString();
    }
}