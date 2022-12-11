using Scripting.Engine.BStacks;

namespace Scripting.Engine;

public static class BStackExtensions
{
    public static IBStack Clone(this IBStack stack)
    {
        var newStack = new BStack();
        foreach (var item in stack)
            newStack.Push(item);
        return newStack;
    }
}