using System;

namespace Scripting.Engine.BStacks;

public class BtcScriptRuntimeException : Exception
{
    public BtcScriptRuntimeException(string message) : base(message)
    {
    }
}