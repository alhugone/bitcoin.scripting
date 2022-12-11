using BtcScript.Compiler.Compiling;
using Scripting.Engine;

public class BuildBScriptEngineFactory
{
    public static BScriptEngine BuildBScriptEngine(string script, IBStack stack) => new(
        script,
        BScriptCompilerFactory.GetDefault().Compile,
        OpLocatorFactory.CreateStandard(),
        stack,
        new OpCodeDecoder(OpCodeArgumentsExtractor.GetArgumentsForOpCode)
    );

    public static BScriptEngineV3 BuildBScriptEngineV3(IBStack stack) => new(
        BScriptCompilerFactory.GetDefault().Compile,
        OpLocatorFactory.CreateStandard(),
        stack,
        new OpCodeDecoder(OpCodeArgumentsExtractor.GetArgumentsForOpCode)
    );
}