using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Unicode;
using Scripting.OpCodes.BStacks;

namespace Scripting.Opcodes.Tests
{
    public static class BStackBuilder
    {
        public static BStack WithItems(params string[] items) =>
            WithItems(items.Select(
                    item => Encoding.UTF8.GetBytes(item).ToList())
                .ToArray()
            );

        public static BStack PushItem(this BStack stack, int item)
        {
            stack.Push(BitConverter.GetBytes(item));
            return stack;
        }

        public static BStack WithItems(params IReadOnlyList<byte>[] items)
        {
            var stack = new BStack();
            foreach (var item in items)
            {
                stack.Push(item);
            }
            return stack;
        }
    }
}
