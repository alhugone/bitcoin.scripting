namespace Scripting.Engine;

public static class OpCodeDecoderFactory
{
    public static OpCodeDecoder GetDefault() => new(OpCodeArgumentsExtractor.GetArgumentsForOpCode);
}
