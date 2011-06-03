/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using ClrTest.Reflection;
using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    public class MonoILReader : IEnumerable<ILInstruction>
    {
        Byte[] m_byteArray;
        Int32 m_position;
        ITokenResolver m_resolver;
        bool m_fixupSuccess;

        static OpCode[] s_OneByteOpCodes = new OpCode[0x100];
        static OpCode[] s_TwoByteOpCodes = new OpCode[0x100];

        static FieldInfo s_fiLen = typeof(ILGenerator).GetField("code_len", BindingFlags.NonPublic | BindingFlags.Instance);
        static FieldInfo s_fiStream = typeof(ILGenerator).GetField("code", BindingFlags.NonPublic | BindingFlags.Instance);

        static MethodInfo s_miLabelFixup = typeof(ILGenerator).GetMethod("label_fixup", BindingFlags.NonPublic | BindingFlags.Instance);

        static MonoILReader()
        {
            foreach(FieldInfo fi in typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                OpCode opCode = (OpCode) fi.GetValue(null);
                UInt16 value = (UInt16) opCode.Value;
                if(value < 0x100)
                {
                    s_OneByteOpCodes[value] = opCode;
                }
                else if((value & 0xff00) == 0xfe00)
                {
                    s_TwoByteOpCodes[value & 0xff] = opCode;
                }
            }
        }

        public bool FixupSuccess
        {
            get { return this.m_fixupSuccess; }
        }

        public MonoILReader(DynamicMethod dynamicMethod)
        {
            this.m_resolver = new MonoDynamicScopeTokenResolver(dynamicMethod);
            ILGenerator ilgen = dynamicMethod.GetILGenerator();

            this.m_fixupSuccess = false;
            try
            {
                s_miLabelFixup.Invoke(ilgen, null);
                this.m_byteArray = (byte[]) s_fiStream.GetValue(ilgen);
                if(this.m_byteArray == null) this.m_byteArray = new byte[0];
                m_fixupSuccess = true;
            }
            catch(TargetInvocationException)
            {
                int length = (int) s_fiLen.GetValue(ilgen);
                this.m_byteArray = new byte[length];
                Array.Copy((byte[]) s_fiStream.GetValue(ilgen), this.m_byteArray, length);
            }
            this.m_position = 0;
        }

        public IEnumerator<ILInstruction> GetEnumerator()
        {
            while(m_position < m_byteArray.Length)
                yield return Next();

            m_position = 0;
            yield break;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

        ILInstruction Next()
        {
            Int32 offset = m_position;
            OpCode opCode = OpCodes.Nop;
            Int32 token = 0;

            // read first 1 or 2 bytes as opCode
            Byte code = ReadByte();
            if(code != 0xFE)
            {
                opCode = s_OneByteOpCodes[code];
            }
            else
            {
                code = ReadByte();
                opCode = s_TwoByteOpCodes[code];
            }

            switch(opCode.OperandType)
            {
                case OperandType.InlineNone:
                    return new InlineNoneInstruction(m_resolver, offset, opCode);

                //The operand is an 8-bit integer branch target.
                case OperandType.ShortInlineBrTarget:
                    SByte shortDelta = ReadSByte();
                    return new ShortInlineBrTargetInstruction(m_resolver, offset, opCode, shortDelta);

                //The operand is a 32-bit integer branch target.
                case OperandType.InlineBrTarget:
                    Int32 delta = ReadInt32();
                    return new InlineBrTargetInstruction(m_resolver, offset, opCode, delta);

                //The operand is an 8-bit integer: 001F  ldc.i4.s, FE12  unaligned.
                case OperandType.ShortInlineI:
                    Byte int8 = ReadByte();
                    return new ShortInlineIInstruction(m_resolver, offset, opCode, int8);

                //The operand is a 32-bit integer.
                case OperandType.InlineI:
                    Int32 int32 = ReadInt32();
                    return new InlineIInstruction(m_resolver, offset, opCode, int32);

                //The operand is a 64-bit integer.
                case OperandType.InlineI8:
                    Int64 int64 = ReadInt64();
                    return new InlineI8Instruction(m_resolver, offset, opCode, int64);

                //The operand is a 32-bit IEEE floating point number.
                case OperandType.ShortInlineR:
                    Single float32 = ReadSingle();
                    return new ShortInlineRInstruction(m_resolver, offset, opCode, float32);

                //The operand is a 64-bit IEEE floating point number.
                case OperandType.InlineR:
                    Double float64 = ReadDouble();
                    return new InlineRInstruction(m_resolver, offset, opCode, float64);

                //The operand is an 8-bit integer containing the ordinal of a local variable or an argument
                case OperandType.ShortInlineVar:
                    Byte index8 = ReadByte();
                    return new ShortInlineVarInstruction(m_resolver, offset, opCode, index8);

                //The operand is 16-bit integer containing the ordinal of a local variable or an argument.
                case OperandType.InlineVar:
                    UInt16 index16 = ReadUInt16();
                    return new InlineVarInstruction(m_resolver, offset, opCode, index16);

                //The operand is a 32-bit metadata string token.
                case OperandType.InlineString:
                    token = ReadInt32();
                    return new InlineStringInstruction(m_resolver, offset, opCode, token);

                //The operand is a 32-bit metadata signature token.
                case OperandType.InlineSig:
                    token = ReadInt32();
                    return new InlineSigInstruction(m_resolver, offset, opCode, token);

                //The operand is a 32-bit metadata token.
                case OperandType.InlineMethod:
                    token = ReadInt32();
                    return new InlineMethodInstruction(m_resolver, offset, opCode, token);

                //The operand is a 32-bit metadata token.
                case OperandType.InlineField:
                    token = ReadInt32();
                    return new InlineFieldInstruction(m_resolver, offset, opCode, token);

                //The operand is a 32-bit metadata token.
                case OperandType.InlineType:
                    token = ReadInt32();
                    return new InlineTypeInstruction(m_resolver, offset, opCode, token);

                //The operand is a FieldRef, MethodRef, or TypeRef token.
                case OperandType.InlineTok:
                    token = ReadInt32();
                    return new InlineTokInstruction(m_resolver, offset, opCode, token);

                //The operand is the 32-bit integer argument to a switch instruction.
                case OperandType.InlineSwitch:
                    Int32 cases = ReadInt32();
                    Int32[] deltas = new Int32[cases];
                    for(Int32 i = 0; i < cases; i++) deltas[i] = ReadInt32();
                    return new InlineSwitchInstruction(m_resolver, offset, opCode, deltas);

                default:
                    throw new BadImageFormatException("unexpected OperandType " + opCode.OperandType);
            }
        }

        Byte ReadByte() { return (Byte) m_byteArray[m_position++]; }
        SByte ReadSByte() { return (SByte) ReadByte(); }

        UInt16 ReadUInt16() { m_position += 2; return BitConverter.ToUInt16(m_byteArray, m_position - 2); }
        UInt32 ReadUInt32() { m_position += 4; return BitConverter.ToUInt32(m_byteArray, m_position - 4); }
        UInt64 ReadUInt64() { m_position += 8; return BitConverter.ToUInt64(m_byteArray, m_position - 8); }

        Int32 ReadInt32() { m_position += 4; return BitConverter.ToInt32(m_byteArray, m_position - 4); }
        Int64 ReadInt64() { m_position += 8; return BitConverter.ToInt64(m_byteArray, m_position - 8); }

        Single ReadSingle() { m_position += 4; return BitConverter.ToSingle(m_byteArray, m_position - 4); }
        Double ReadDouble() { m_position += 8; return BitConverter.ToDouble(m_byteArray, m_position - 8); }
    }

    public class MonoDynamicScopeTokenResolver : ITokenResolver
    {
/*        static PropertyInfo s_indexer;
        static FieldInfo s_scopeFi;

        static Type s_genMethodInfoType;
        static FieldInfo s_genmethFi1, s_genmethFi2;

        static Type s_varArgMethodType;
        static FieldInfo s_varargFi1, s_varargFi2;

        static MonoDynamicScopeTokenResolver()
        {
            BindingFlags s_bfInternal = BindingFlags.NonPublic | BindingFlags.Instance;
            s_indexer = Type.GetType("System.Reflection.Emit.DynamicScope").GetProperty("Item", s_bfInternal);
            s_scopeFi = Type.GetType("System.Reflection.Emit.DynamicILGenerator").GetField("m_scope", s_bfInternal);

            s_varArgMethodType = Type.GetType("System.Reflection.Emit.VarArgMethod");
            s_varargFi1 = s_varArgMethodType.GetField("m_method", s_bfInternal);
            s_varargFi2 = s_varArgMethodType.GetField("m_signature", s_bfInternal);

            s_genMethodInfoType = Type.GetType("System.Reflection.Emit.GenericMethodInfo");
            s_genmethFi1 = s_genMethodInfoType.GetField("m_method", s_bfInternal);
            s_genmethFi2 = s_genMethodInfoType.GetField("m_context", s_bfInternal);
        }*/

        DynamicMethod dynMethod;
//        object m_scope = null;

        public MonoDynamicScopeTokenResolver(DynamicMethod dm)
        {
            dynMethod = dm;
//            m_scope = s_scopeFi.GetValue(dm.GetILGenerator());
        }

/*        internal object this[int token]
        {
            get { return s_indexer.GetValue(m_scope, new object[] { token }); }
        }*/

        public String AsString(int token)
        {
            return dynMethod.Module.ResolveString(token);
//            return this[token] as string;
        }

        public FieldInfo AsField(int token)
        {
            return dynMethod.Module.ResolveField(token);
//            return FieldInfo.GetFieldFromHandle((RuntimeFieldHandle) this[token]);
        }

        public Type AsType(int token)
        {
            return dynMethod.Module.ResolveType(token);
//            return Type.GetTypeFromHandle((RuntimeTypeHandle) this[token]);
        }

        public MethodBase AsMethod(int token)
        {
            return dynMethod.Module.ResolveMethod(token);
#if ORIGINALVERSION
            if(this[token] is DynamicMethod)
                return this[token] as DynamicMethod;

            if(this[token] is RuntimeMethodHandle)
                return MethodBase.GetMethodFromHandle((RuntimeMethodHandle) this[token]);

            if(this[token].GetType() == s_genMethodInfoType)
                return MethodBase.GetMethodFromHandle(
                    (RuntimeMethodHandle) s_genmethFi1.GetValue(this[token])/*,
                    (RuntimeTypeHandle)s_genmethFi2.GetValue(this[token])*/
                                                                           );

            if(this[token].GetType() == s_varArgMethodType)
                return (MethodInfo) s_varargFi1.GetValue(this[token]);

            Debug.Assert(false, string.Format("unexpected type: {0}", this[token].GetType()));
            return null;
#endif
        }

        public MemberInfo AsMember(int token)
        {
            if((token & 0x02000000) == 0x02000000)
                return this.AsType(token);
            if((token & 0x06000000) == 0x06000000)
                return this.AsMethod(token);
            if((token & 0x04000000) == 0x04000000)
                return this.AsField(token);

            Debug.Assert(false, string.Format("unexpected token type: {0:x8}", token));
            return null;
        }

        public byte[] AsSignature(int token)
        {
            Debug.Assert(false, string.Format("AsSignature not supported by Mono! Token: {0:x8}", token));
            return null;
//            return this[token] as byte[];
        }
    }
}
