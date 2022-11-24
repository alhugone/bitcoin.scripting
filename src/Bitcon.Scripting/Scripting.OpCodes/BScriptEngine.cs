using Scripting.OpCodes.Number;

namespace Scripting.OpCodes
{
    public class BScriptEngine
    {
        private readonly string[] _instructions;
        private readonly OpLocator _locator;
        private int _instructionPointer;

        public BScriptEngine(string script, OpLocator locator, IBStack stack)
        {
            _locator = locator;
            Stack = stack;
            _instructions = script.Split(" ");
            _instructionPointer = 0;
        }

        public IBStack Stack { get; }

        public void DoStep()
        {
            if (_instructionPointer < _instructions.Length)
            {
                var currentInstruction = _instructions[_instructionPointer++];
                ExecuteInstruction(currentInstruction);
            }
        }

        private void ExecuteInstruction(string currentInstruction)
        {
            if (int.TryParse(currentInstruction, out var result))
                Stack.Push(new BInt32(result));
            else
                _locator.Execute(currentInstruction, Stack);
        }

        public void Execute()
        {
            while (_instructionPointer < _instructions.Length)
                DoStep();
        }
    }
}