using System.Collections.Generic;
using Scripting.OpCodes.Number;

namespace Scripting.OpCodes.BStacks
{
    public class CountingBStack : IBStack
    {
        private readonly IBStack _bStack;
        public CountingBStack(IBStack bStack) => _bStack = bStack;
        public int PushInvoked { get; private set; }
        public int PopInvoked { get; private set; }
        public int PeekInvoked { get; private set; }

        public void Push(ScriptNumber number)
        {
            ++PushInvoked;
            _bStack.Push(number);
        }

        public void Push(IReadOnlyList<byte> bytes)
        {
            ++PushInvoked;
            _bStack.Push(bytes);
        }

        public IReadOnlyList<byte> Pop()
        {
            ++PopInvoked;
            return _bStack.Pop();
        }

        public IReadOnlyList<byte> this[int index] => _bStack[index];

        public IReadOnlyList<byte> Peek()
        {
            ++PeekInvoked;
            return _bStack.Peek();
        }

        public int Count => _bStack.Count;
    }
}
