using System.Linq;
using System.Text;
using FluentAssertions;
using Scripting.Engine.BStacks;

namespace Scripting.Opcodes.Tests;

public static class AssertExtensions
{
    public static void ShouldBeEquivalentTo(this BStack stack, BStack expected)
    {
        stack.Count.Should().Be(expected.Count);
        var cnt = stack.Count;
        var stackArray = new string[cnt];
        var expectedArray = new string[cnt];
        for (var i = 0; i < cnt; i++)
        {
            stackArray[i] = Encoding.UTF8.GetString(stack.Pop().ToArray());
            expectedArray[i] = Encoding.UTF8.GetString(expected.Pop().ToArray());
        }
        stackArray.Reverse().Should().BeEquivalentTo(expectedArray.Reverse());
    }
}