using System.Collections.Generic;
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

        public IReadOnlyList<byte> Pop()
        {
            return _stack.Pop();
        }

        public IReadOnlyList<byte> Peek()
        {
            return _stack.Peek();
        }
    }
}