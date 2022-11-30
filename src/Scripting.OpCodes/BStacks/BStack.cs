using System.Collections.Generic;
using System.Linq;
using Scripting.OpCodes.Number;

namespace Scripting.OpCodes.BStacks
{
    public class BStack : IBStack
    {
        private readonly Stack<IReadOnlyList<byte>> _stack = new();
        public int Count => _stack.Count;

        public void Push(ScriptNumber number)
        {
            _stack.Push(number.Get());
        }

        public void Push(IReadOnlyList<byte> bytes)
        {
            _stack.Push(bytes);
        }

        public IReadOnlyList<byte> Pop() => _stack.Pop();

        public IReadOnlyList<byte> this[int index] => _stack.ToArray().Reverse().ToArray()[index];
        public IReadOnlyList<byte> Peek() => _stack.Peek();
    }
}
