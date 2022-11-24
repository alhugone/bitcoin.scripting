using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripting.OpCodes
{
    public class OpLocator
    {
        private readonly Dictionary<string, Action<IBStack>> _opsDictionary;

        public OpLocator(Dictionary<string, Action<IBStack>> opsDictionary)
        {
            _opsDictionary = opsDictionary.ToDictionary(x => x.Key.ToUpperInvariant(), y => y.Value);
        }

        public void Execute(string currentInstruction, IBStack stack)
        {
            var key = currentInstruction.ToUpperInvariant();
            if (!_opsDictionary.ContainsKey(key))
                throw new ArgumentOutOfRangeException(nameof(currentInstruction), currentInstruction,
                    "Is not known OpCode");
            _opsDictionary[key](stack);
        }
    }
}