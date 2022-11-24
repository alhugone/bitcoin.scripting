using System;
using System.Collections.Generic;
using Scripting.OpCodes.OpCodesLogic;

namespace Scripting.OpCodes
{
    public class OpLocatorFactory
    {
        public static OpLocator CreateStandard()
        {
            return new OpLocator(new Dictionary<string, Action<IBStack>>
            {
                { OpcodeType.OpAdd.ToString(), ArithmeticOpsCompose.Add },
                { OpcodeType.OpSub.ToString(), ArithmeticOpsCompose.Subtract },
                { OpcodeType.OpBooland.ToString(), ArithmeticOpsCompose.OP_BOOLAND },
                { OpcodeType.OpBoolor.ToString(), ArithmeticOpsCompose.OP_BOOLOR },
                { OpcodeType.OpNumequal.ToString(), ArithmeticOpsCompose.OP_NUMEQUAL },
                { OpcodeType.OpNumequalverify.ToString(), ArithmeticOpsCompose.OP_NUMEQUALVERIFY },
                { OpcodeType.OpNumnotequal.ToString(), ArithmeticOpsCompose.OP_NUMNOTEQUAL },
                { OpcodeType.OpLessthan.ToString(), ArithmeticOpsCompose.OP_LESSTHAN },
                { OpcodeType.OpLessthanorequal.ToString(), ArithmeticOpsCompose.OP_LESSTHANOREQUAL },
                { OpcodeType.OpGreaterthan.ToString(), ArithmeticOpsCompose.OP_GREATERTHAN },
                { OpcodeType.OpGreaterthanorequal.ToString(), ArithmeticOpsCompose.OP_GREATERTHANOREQUAL },
                { OpcodeType.OpMin.ToString(), ArithmeticOpsCompose.OP_MIN },
                { OpcodeType.OpMax.ToString(), ArithmeticOpsCompose.OP_MAX },
                { OpcodeType.OpWithin.ToString(), ArithmeticOpsCompose.OP_WITHIN },
                { OpcodeType.Op1Add.ToString(), ArithmeticOpsCompose.Increment },
                { OpcodeType.Op1Sub.ToString(), ArithmeticOpsCompose.Decrement },
                { OpcodeType.OpNegate.ToString(), ArithmeticOpsCompose.Negate },
                { OpcodeType.OpAbs.ToString(), ArithmeticOpsCompose.Abs },
                { OpcodeType.OpNot.ToString(), ArithmeticOpsCompose.OP_NOT },
                { OpcodeType.Op0Notequal.ToString(), ArithmeticOpsCompose.OP_0NOTEQUAL }
            });
        }
    }
}