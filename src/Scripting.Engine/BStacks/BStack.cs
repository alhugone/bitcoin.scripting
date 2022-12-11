using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripting.Engine.Number;

namespace Scripting.Engine.BStacks;

public class BStack : IBStack
{
    private readonly Stack<IReadOnlyList<byte>> _stack = new();
    public BStack(int maxItemLength = BtcBitcoinConst.MaxStackElementLength) => MaxItemLength = maxItemLength;
    public int MaxItemLength { get; }
    public int Count => _stack.Count;

    public void Push(ScriptNumber number)
    {
        Push(number.Get().ToList());
    }

    public void Push(IReadOnlyList<byte> bytes)
    {
        if (bytes.Count > MaxItemLength)
            throw new ItemTooLargeForStack(bytes.Count, MaxItemLength);
        _stack.Push(bytes.ToList());
    }

    public IReadOnlyList<byte> Pop() => _stack.Pop();
    public IReadOnlyList<byte> this[int index] => _stack.ToArray().Reverse().ToArray()[index];
    public IReadOnlyList<byte> Peek() => _stack.Peek();
    public IEnumerator<IReadOnlyList<byte>> GetEnumerator() => _stack.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
