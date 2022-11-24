namespace Scripting.OpCodes
{
    public enum OpcodeType
    {
        // push value
        Op0 = 0x00,
        OpFalse = Op0,
        OpPushData1 = 0x4c,
        OpPushData2 = 0x4d,
        OpPushData4 = 0x4e,
        Op1Negate = 0x4f,
        OpReserved = 0x50,
        Op1 = 0x51,
        OpTrue = Op1,
        Op2 = 0x52,
        Op3 = 0x53,
        Op4 = 0x54,
        Op5 = 0x55,
        Op6 = 0x56,
        Op7 = 0x57,
        Op8 = 0x58,
        Op9 = 0x59,
        Op10 = 0x5a,
        Op11 = 0x5b,
        Op12 = 0x5c,
        Op13 = 0x5d,
        Op14 = 0x5e,
        Op15 = 0x5f,
        Op16 = 0x60,

        // control
        OpNop = 0x61,
        OpVer = 0x62,
        OpIf = 0x63,
        OpNotif = 0x64,
        OpVerif = 0x65,
        OpVernotif = 0x66,
        OpElse = 0x67,
        OpEndif = 0x68,
        OpVerify = 0x69,
        OpReturn = 0x6a,

        // stack ops
        OpToaltstack = 0x6b,
        OpFromaltstack = 0x6c,
        Op2Drop = 0x6d,
        Op2Dup = 0x6e,
        Op3Dup = 0x6f,
        Op2Over = 0x70,
        Op2Rot = 0x71,
        Op2Swap = 0x72,
        OpIfdup = 0x73,
        OpDepth = 0x74,
        OpDrop = 0x75,
        OpDup = 0x76,
        OpNip = 0x77,
        OpOver = 0x78,
        OpPick = 0x79,
        OpRoll = 0x7a,
        OpRot = 0x7b,
        OpSwap = 0x7c,
        OpTuck = 0x7d,

        // splice ops
        OpCat = 0x7e,
        OpSubstr = 0x7f,
        OpLeft = 0x80,
        OpRight = 0x81,
        OpSize = 0x82,

        // bit logic
        OpInvert = 0x83,
        OpAnd = 0x84,
        OpOr = 0x85,
        OpXor = 0x86,
        OpEqual = 0x87,
        OpEqualverify = 0x88,
        OpReserved1 = 0x89,
        OpReserved2 = 0x8a,

        // numeric
        Op1Add = 0x8b,
        Op1Sub = 0x8c,

        OpNegate = 0x8f,
        OpAbs = 0x90,
        OpNot = 0x91,
        Op0Notequal = 0x92,

        OpAdd = 0x93,
        OpSub = 0x94,

        OpBooland = 0x9a,
        OpBoolor = 0x9b,
        OpNumequal = 0x9c,
        OpNumequalverify = 0x9d,
        OpNumnotequal = 0x9e,
        OpLessthan = 0x9f,
        OpGreaterthan = 0xa0,
        OpLessthanorequal = 0xa1,
        OpGreaterthanorequal = 0xa2,
        OpMin = 0xa3,
        OpMax = 0xa4,

        OpWithin = 0xa5,

        // disabled 4 ever:
        Op2Mul = 0x8d,
        Op2Div = 0x8e,
        OpMul = 0x95,
        OpDiv = 0x96,
        OpMod = 0x97,
        OpLshift = 0x98,
        OpRshift = 0x99,

        // crypto
        OpRipemd160 = 0xa6,
        OpSha1 = 0xa7,
        OpSha256 = 0xa8,
        OpHash160 = 0xa9,
        OpHash256 = 0xaa,
        OpCodeseparator = 0xab,
        OpChecksig = 0xac,
        OpChecksigverify = 0xad,
        OpCheckmultisig = 0xae,
        OpCheckmultisigverify = 0xaf,

        // expansion
        OpNop1 = 0xb0,
        OpChecklocktimeverify = 0xb1,
        OpNop2 = OpChecklocktimeverify,
        OpChecksequenceverify = 0xb2,
        OpNop3 = OpChecksequenceverify,
        OpNop4 = 0xb3,
        OpNop5 = 0xb4,
        OpNop6 = 0xb5,
        OpNop7 = 0xb6,
        OpNop8 = 0xb7,
        OpNop9 = 0xb8,
        OpNop10 = 0xb9,

        // Opcode added by BIP 342 (Tapscript)
        OpChecksigadd = 0xba,

        OpInvalidopcode = 0xff
    }
}