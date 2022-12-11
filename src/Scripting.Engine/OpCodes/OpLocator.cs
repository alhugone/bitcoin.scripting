using System;
using System.Collections.Generic;

namespace Scripting.Engine;

public class OpLocator
{
    private readonly Dictionary<string, Action<IBStack>> _opsDictionary;
    private readonly Dictionary<string, Action<IBStack, IReadOnlyList<byte[]>>> _opsDictionaryStatic;

    public OpLocator(
        Dictionary<string, Action<IBStack>> opsDictionary,
        Dictionary<string, Action<IBStack, IReadOnlyList<byte[]>>> opsDictionaryStatic
    )
    {
        _opsDictionaryStatic = opsDictionaryStatic;
        _opsDictionary = opsDictionary;
    }

    public void Execute(OpCodeDecodedInstruction currentInstruction, IBStack stack)
    {
        if (_opsDictionary.ContainsKey(currentInstruction.Opcode.ToString()))
            _opsDictionary[currentInstruction.Opcode.ToString()](stack);
        else if (_opsDictionaryStatic.ContainsKey(currentInstruction.Opcode.ToString()))
            _opsDictionaryStatic[currentInstruction.Opcode.ToString()](
                stack,
                ((OpCodeWithArgumentsDecodedInstruction)currentInstruction).StaticArgs
            );
        else if (_opsDictionaryStatic.ContainsKey(currentInstruction.Opcode.ToString()))
            _opsDictionaryStatic[currentInstruction.Opcode.ToString()](
                stack,
                ((OpCodeWithArgumentsDecodedInstruction)currentInstruction).StaticArgs
            );
        else
            throw new ArgumentOutOfRangeException(
                nameof(currentInstruction),
                currentInstruction.Opcode,
                "Is not known OpCode"
            );
    }
}