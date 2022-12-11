using System;
using System.Collections.Generic;
using System.Linq;
using Scripting.Engine.BStacks;
using Scripting.Engine.OpCodesLogic.Stack;

namespace Scripting.Engine;

public class BScriptEngineV3
{
    private readonly Func<string, IEnumerable<byte>> _compiler;
    private readonly Stack<IBStack> _history = new();
    private readonly OpLocator _locator;
    private readonly OpCodeDecoder _opcodeDecoder;
    private byte[] _byteOfCodes;
    private IReadOnlyList<OpCodeDecodedInstruction> _compiledOpCodes = new List<OpCodeDecodedInstruction>();
    private int _currentInstruction;
    private string _script;

    public BScriptEngineV3(
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
        Completed = true;
        SetScript("");
    }

    public IBStack Stack { get; set; }

    public OpCodeDecodedInstruction? NextInstruction =>
        HasMoreInstructions ? _compiledOpCodes[_currentInstruction] : null;

    public bool HasMoreInstructions => _currentInstruction < _compiledOpCodes.Count;
    public bool Completed { get; private set; }

    public bool? Result
    {
        get
        {
            var result = (bool?)null;
            if (Completed)
                result = Error is null && Stack.Count > 0 && Stack.Peek().AsBool();
            return result;
        }
    }

    public Exception Error { get; private set; }

    public void ExecuteAll()
    {
        while (!Completed)
            ExecuteNext();
    }

    public void SetScript(string script)
    {
        _byteOfCodes = _compiler(script).ToArray();
        _compiledOpCodes = _opcodeDecoder.DecodeAll(_byteOfCodes);
        _currentInstruction = 0;
        _history.Clear();
        Stack = new BStack();
        Completed = !_compiledOpCodes.Any();
        Error = null;
        _script = script;
    }

    public void ExecuteNext()
    {
        if (Completed)
            throw new Exception("Execution of script is already completed.");
        _history.Push(Stack.Clone());
        try
        {
            ExecuteStep(NextInstruction);
            ++_currentInstruction;
            if (!HasMoreInstructions)
                Completed = true;
        }
        catch (Exception ex)
        {
            Completed = true;
            Error = ex;
        }
    }

    private void ExecuteStep(OpCodeDecodedInstruction instruction) =>
        _locator.Execute(instruction, Stack);

    public void UndoLast()
    {
        if (_currentInstruction > 0)
        {
            Stack = _history.Pop();
            --_currentInstruction;
            Completed = false;
            Error = null;
        }
    }
}