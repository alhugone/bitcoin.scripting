using System;
using System.Collections.Generic;
using Scripting.Engine.OpCodesLogic;

namespace Scripting.Engine;

public class OpCodeArgumentsExtractor
{
    public static IReadOnlyList<byte[]> GetArgumentsForOpCode(OpcodeType opcodeType, Func<int, byte[]> readNextBytesFromScriptByteCodes)
    {
        var arguments = new List<byte[]>();
        switch (opcodeType)
        {
            case OpcodeType.OpPushData1:
                arguments.AddRange(OpCodeDecodePush.OP_PUSHDATA1(readNextBytesFromScriptByteCodes));
                break;
            case OpcodeType.OpPushData2:
                arguments.AddRange(OpCodeDecodePush.OP_PUSHDATA2(readNextBytesFromScriptByteCodes));
                break;
            case OpcodeType.OpPushData4:
                arguments.AddRange(OpCodeDecodePush.OP_PUSHDATA4(readNextBytesFromScriptByteCodes));
                break;
            case >= OpcodeType.OpPush1 and <= OpcodeType.OpPush75:
                arguments.AddRange(OpCodeDecodePush.OP_ImmediatelyN((int)opcodeType, readNextBytesFromScriptByteCodes));
                break;
        }
        return arguments;
    }
}
