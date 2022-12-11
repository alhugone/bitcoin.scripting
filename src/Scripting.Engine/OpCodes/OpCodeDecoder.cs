using System;
using System.Collections.Generic;
using System.Linq;
using Scripting.Engine.BStacks;

namespace Scripting.Engine;

public class OpCodeDecoder
{
    private readonly Func<OpcodeType, Func<int, byte[]>, IReadOnlyList<byte[]>> _decodeOpCodeArgs;

    public OpCodeDecoder(Func<OpcodeType, Func<int, byte[]>, IReadOnlyList<byte[]>> decodeOpCodeArgs) =>
        _decodeOpCodeArgs = decodeOpCodeArgs;

    public OpCodeDecodedInstruction DecodeNextOpCode(Func<int, byte[]> readNextBytes)
    {
        var opCodeByte = readNextBytes(1);
        var opCode = (OpcodeType)opCodeByte[0];
        var staticArgs = _decodeOpCodeArgs(opCode, readNextBytes);
        return staticArgs is null
            ? new OpCodeDecodedInstruction
            {
                Opcode = opCode
            }
            : new OpCodeWithArgumentsDecodedInstruction
            {
                Opcode = opCode,
                StaticArgs = staticArgs
            };
    }

    public IReadOnlyList<OpCodeDecodedInstruction> DecodeAll(byte[] scriptByteCodes)
    {
        var currentByteIndex = 0;

        byte[] ReadNextBytesFromScriptByteCode(int bytesCount)
        {
            // if (currentByteIndex + bytesCount >= scriptByteCodes.Length)
            //     throw new BtcScriptRuntimeException("out of range");
            var bytes = scriptByteCodes.Skip(currentByteIndex).Take(bytesCount).ToArray();
            currentByteIndex += bytesCount;
            return bytes;
        }

        var decodedOpCodes = new List<OpCodeDecodedInstruction>();
        while (currentByteIndex < scriptByteCodes.Length)
            decodedOpCodes.Add(DecodeNextOpCode(ReadNextBytesFromScriptByteCode));
        return decodedOpCodes;
    }
}
