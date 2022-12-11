using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripting.Engine;

public class BScriptEngine
{
    private readonly Func<string, IEnumerable<byte>> _compiler;
    private readonly OpLocator _locator;
    private readonly OpCodeDecoder _opcodeDecoder;
    private readonly string _script;

    public BScriptEngine(
        Func<string, IEnumerable<byte>> compiler,
        OpLocator locator,
        IBStack stack,
        OpCodeDecoder opcodeDecoder
    )
    {
        _compiler = compiler;
        _locator = locator;
        Stack = stack;
        _opcodeDecoder = opcodeDecoder;
    }

    public BScriptEngine(
        string script,
        Func<string, IEnumerable<byte>> compiler,
        OpLocator locator,
        IBStack stack,
        OpCodeDecoder opcodeDecoder
    ) : this(compiler, locator, stack, opcodeDecoder) => _script = script;

    public IBStack Stack { get; }

    public void Execute()
    {
        Execute(_script);
    }

    public void Execute(string script)
    {
        var byteOfCode = _compiler(script).ToArray();
        Execute(byteOfCode);
    }

    private void Execute(byte[] byteOfCode)
    {
        var opCodes = _opcodeDecoder.DecodeAll(byteOfCode);
        foreach (var codeDecodedInstruction in opCodes)
            ExecuteStep(codeDecodedInstruction);
    }

    private void ExecuteStep(OpCodeDecodedInstruction enumerator)
    {
        _locator.Execute(enumerator, Stack);
    }
}