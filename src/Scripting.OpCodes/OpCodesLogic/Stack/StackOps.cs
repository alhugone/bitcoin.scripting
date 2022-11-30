using Scripting.OpCodes.BStacks;
using Scripting.OpCodes.Number;

namespace Scripting.OpCodes.OpCodesLogic.Stack
{
    public static class StackOps
    {
        public static void OP_2DROP(IBStack stack)
        {
            stack.Pop();
            stack.Pop();
        }

        public static void OP_2DUP(IBStack stack)
        {
            var x1 = stack.Pop();
            var x2 = stack.Pop();
            for (var i = 0; i < 2; i++)
            {
                stack.Push(x2);
                stack.Push(x1);
            }
        }

        public static void OP_3DUP(IBStack stack)
        {
            var x1 = stack.Pop();
            var x2 = stack.Pop();
            var x3 = stack.Pop();
            for (var i = 0; i < 2; i++)
            {
                stack.Push(x3);
                stack.Push(x2);
                stack.Push(x1);
            }
        }

       public static void OP_2ROT(IBStack stack)
        {
            var x1 = stack.Pop();
            var x2 = stack.Pop();
            var x3 = stack.Pop();
            var x4 = stack.Pop();
            var x5 = stack.Pop();
            var x6 = stack.Pop();
            stack.Push(x4);
            stack.Push(x3);
            stack.Push(x2);
            stack.Push(x1);
            stack.Push(x6);
            stack.Push(x5);
        }

        public static void OP_2SWAP(IBStack stack)
        {
            var x1 = stack.Pop();
            var x2 = stack.Pop();
            var x3 = stack.Pop();
            var x4 = stack.Pop();
            stack.Push(x2);
            stack.Push(x1);
            stack.Push(x4);
            stack.Push(x3);
        }

        public static void OP_IFDUP(IBStack stack)
        {
            var x = stack.Peek();
            if (x.AsBool())
                stack.Push(x);
        }

        public static void OP_DEPTH(IBStack stack)
        {
            stack.Push(new BInt32(stack.Count));
        }

        public static void OP_DROP(IBStack stack)
        {
            stack.Pop();
        }

        public static void OP_DUP(IBStack stack)
        {
            stack.Push(stack.Peek());
        }

        public static void OP_NIP(IBStack stack)
        {
            while (stack.Count > 2)
                stack.Pop();
        }

        public static void OP_OVER(IBStack stack)
        {
            var x1 = stack.Pop();
            var x2 = stack.Pop();
            stack.Push(x2);
            stack.Push(x1);
            stack.Push(x2);
        }

        public static void OP_PICK(IBStack stack)
        {
            var index = stack.PopInt32();
            stack.Push(stack[^(index)]);
        }

        public static void OP_ROLL(IBStack stack)
        {
            var index = stack.PopInt32();
            var cpy = stack[^index];
            var bst = new BStack();
            for (var i = 0; i < index; i++)
                bst.Push(stack.Pop());
            bst.Pop();
            while (bst.Count > 0)
                stack.Push(bst.Pop());
            stack.Push(cpy);
        }

        public static void OP_ROT(IBStack stack)
        {
            var x1 = stack.Pop();
            var x2 = stack.Pop();
            var x3 = stack.Pop();
            stack.Push(x2);
            stack.Push(x1);
            stack.Push(x3);
        }

        public static void OP_SWAP(IBStack stack)
        {
            var x1 = stack.Pop();
            var x2 = stack.Pop();
            stack.Push(x2);
            stack.Push(x1);
        }

        public static void OP_TUCK(IBStack stack)
        {
            var x1 = stack.Pop();
            var x2 = stack.Pop();
            stack.Push(x1);
            stack.Push(x2);
            stack.Push(x1);
        }

        public static void OP_2OVER(IBStack stack)
        {
            var x1 = stack[^4];
            var x2 = stack[^3];
            stack.Push(x1);
            stack.Push(x2);
        }
    }
}
