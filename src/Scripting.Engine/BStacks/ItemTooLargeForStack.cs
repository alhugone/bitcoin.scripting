namespace Scripting.Engine.BStacks;

public class ItemTooLargeForStack : BtcScriptRuntimeException
{
    public ItemTooLargeForStack(int stackElementByteLength, int maxStackElementByteLength) : base(
        $"Tryied to push element on stack that exceeds RuntimeLimits. Element length: {stackElementByteLength}.Stack MaxByteLength:{maxStackElementByteLength}. "
    )
    {
        StackElementByteLength = stackElementByteLength;
        MaxStackElementByteLength = maxStackElementByteLength;
    }

    public int MaxStackElementByteLength { get; set; }
    public int StackElementByteLength { get; set; }
}