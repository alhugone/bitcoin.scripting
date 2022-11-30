using System;
using System.Collections.Generic;
using Scripting.OpCodes.OpCodesLogic;
using Scripting.OpCodes.OpCodesLogic.Arithmetic;
using Scripting.OpCodes.OpCodesLogic.Bit;
using Scripting.OpCodes.OpCodesLogic.Splice;
using Scripting.OpCodes.OpCodesLogic.Stack;

namespace Scripting.OpCodes
{
    public class OpLocatorFactory
    {
        public static OpLocator CreateStandard()
        {
            byte[] ReadNextBytes(ulong n) => new byte[n];
            var dict =
                new Dictionary<string, Action<IBStack>>
                {
                    { OpcodeType.OpAdd.ToString(), ArithmeticOpsCompose.Add },
                    { OpcodeType.OpSub.ToString(), ArithmeticOpsCompose.Subtract },
                    { OpcodeType.OpBoolAnd.ToString(), ArithmeticOpsCompose.OP_BOOLAND },
                    { OpcodeType.OpBoolOr.ToString(), ArithmeticOpsCompose.OP_BOOLOR },
                    { OpcodeType.OpNumEqual.ToString(), ArithmeticOpsCompose.OP_NUMEQUAL },
                    { OpcodeType.OpNumEqualVerify.ToString(), ArithmeticOpsCompose.OP_NUMEQUALVERIFY },
                    { OpcodeType.OpNumNotEqual.ToString(), ArithmeticOpsCompose.OP_NUMNOTEQUAL },
                    { OpcodeType.OpLessThan.ToString(), ArithmeticOpsCompose.OP_LESSTHAN },
                    { OpcodeType.OpLessThanOrEqual.ToString(), ArithmeticOpsCompose.OP_LESSTHANOREQUAL },
                    { OpcodeType.OpGreaterThan.ToString(), ArithmeticOpsCompose.OP_GREATERTHAN },
                    { OpcodeType.OpGreaterThanOrEqual.ToString(), ArithmeticOpsCompose.OP_GREATERTHANOREQUAL },
                    { OpcodeType.OpMin.ToString(), ArithmeticOpsCompose.OP_MIN },
                    { OpcodeType.OpMax.ToString(), ArithmeticOpsCompose.OP_MAX },
                    { OpcodeType.OpWithin.ToString(), ArithmeticOpsCompose.OP_WITHIN },
                    { OpcodeType.Op1Add.ToString(), ArithmeticOpsCompose.Increment },
                    { OpcodeType.Op1Sub.ToString(), ArithmeticOpsCompose.Decrement },
                    { OpcodeType.OpNegate.ToString(), ArithmeticOpsCompose.Negate },
                    { OpcodeType.OpAbs.ToString(), ArithmeticOpsCompose.Abs },
                    { OpcodeType.OpNot.ToString(), ArithmeticOpsCompose.OP_NOT },
                    { OpcodeType.Op0NotEqual.ToString(), ArithmeticOpsCompose.OP_0NOTEQUAL },
                    { OpcodeType.OpSize.ToString(), SpliceOps.OP_SIZE },
                    { OpcodeType.OpDup.ToString(), StackOps.OP_DUP },
                    { OpcodeType.Op2Dup.ToString(), StackOps.OP_2DUP },
                    { OpcodeType.Op3Dup.ToString(), StackOps.OP_3DUP },
                    { OpcodeType.OpDrop.ToString(), StackOps.OP_DROP },
                    { OpcodeType.Op2Drop.ToString(), StackOps.OP_2DROP },
                    { OpcodeType.OpNip.ToString(), StackOps.OP_NIP },
                    { OpcodeType.OpOver.ToString(), StackOps.OP_OVER },
                    { OpcodeType.OpPick.ToString(), StackOps.OP_PICK },
                    { OpcodeType.OpRoll.ToString(), StackOps.OP_ROLL },
                    { OpcodeType.OpRot.ToString(), StackOps.OP_ROT },
                    { OpcodeType.OpSwap.ToString(), StackOps.OP_SWAP },
                    { OpcodeType.OpTuck.ToString(), StackOps.OP_TUCK },
                    { OpcodeType.Op2Over.ToString(), StackOps.OP_2OVER },
                    { OpcodeType.Op2Rot.ToString(), StackOps.OP_2ROT },
                    { OpcodeType.Op2Swap.ToString(), StackOps.OP_2SWAP },
                    { OpcodeType.OpIfdup.ToString(), StackOps.OP_IFDUP },
                    { OpcodeType.OpDepth.ToString(), StackOps.OP_DEPTH },
                    { OpcodeType.OpEqual.ToString(), BitOps.OP_Equal },
                    { OpcodeType.OpEqualVerify.ToString(), BitOps.OP_EqualVerify },
                    { OpcodeType.Op0.ToString(), OpsPush.OP_0 },
                  //  { OpcodeType.OpFalse.ToString(), OpsPush.OP_0 },
                    { OpcodeType.Op1.ToString(), OpsPush.OP_1 },
                    //{ OpcodeType.OpTrue.ToString(), OpsPush.OP_1 },
                    { OpcodeType.Op1Negate.ToString(), OpsPush.OP_1NEGATE },
                    { OpcodeType.OpPushData1.ToString(), (stack)=>OpsPush.OP_PUSHDATA1(stack, ReadNextBytes) },
                    { OpcodeType.OpPushData2.ToString(), (stack)=>OpsPush.OP_PUSHDATA2(stack, ReadNextBytes) },
                    { OpcodeType.OpPushData4.ToString(), (stack)=>OpsPush.OP_PUSHDATA4(stack, ReadNextBytes) },
                    { OpcodeType.OpSha1.ToString(), CryptoOps.Sha1 },
                    { OpcodeType.OpSha256.ToString(), CryptoOps.Sha256 },
                    { OpcodeType.OpRipemd160.ToString(), CryptoOps.RIPEMD160 },
                    { OpcodeType.OpHash160.ToString(), CryptoOps.OP_HASH160 },
                    { OpcodeType.OpHash256.ToString(), CryptoOps.OP_HASH256 },
                    { OpcodeType.OpCodeseparator.ToString(), CryptoOps.OP_CODESEPARATOR },
                    { OpcodeType.OpNop1.ToString(), Expansion.NopN },
                    { OpcodeType.OpNop3.ToString(), Expansion.NopN },
                    { OpcodeType.OpNop4.ToString(), Expansion.NopN },
                    { OpcodeType.OpNop5.ToString(), Expansion.NopN },
                    { OpcodeType.OpNop6.ToString(), Expansion.NopN },
                    { OpcodeType.OpNop7.ToString(), Expansion.NopN },
                    { OpcodeType.OpNop8.ToString(), Expansion.NopN },
                    { OpcodeType.OpNop9.ToString(), Expansion.NopN },
                    { OpcodeType.OpNop10.ToString(), Expansion.NopN },
                };


            for (int i = (int)OpcodeType.OpImmediately1; i < (int)OpcodeType.OpImmediately75; i++)
            {
                var i1 = (ulong)i;
                dict["OpImmediately" + i] = (stack) => OpsPush.OP_PushByteLen(stack, i1, ReadNextBytes);
            }

            for (int i = (int)OpcodeType.OpImmediately1; i < (int)OpcodeType.OpImmediately75; i++)
            {
                var i1 = (int)i;
                dict["Op" + i] = (stack) => OpsPush.OP_N(stack, i1);
            }
            return new OpLocator(dict);
        }
    }
}
