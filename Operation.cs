using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    static unsafe public class otyOpera
    {
        public static otyObj Bool(bool arg1)
        {
            if (arg1) return new otyObj(1); else return new otyObj(0);
        }
        public static otyObj UnaryPlus(otyObj arg1)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                    return new otyObj((void*)(arg1.Ptr.Address + (int)arg2.Num));
                */
                case otyType.Int32:
                    return new otyObj(+arg1.Num);
                case otyType.Double:
                    return new otyObj(+arg1.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'に+単項演算子を適用できません。");
        }
        public static otyObj UnaryMinus(otyObj arg1)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer: return new otyObj((void*)(arg1.Ptr.Address - (int)arg2.Num));
                */
                case otyType.Int32:
                    return new otyObj(-arg1.Num);
                case otyType.Double:
                    return new otyObj(-arg1.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'に-単項演算子を適用できません。");
        }
        public static otyObj Add(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                    return new otyObj((void*)(arg1.Ptr.Address + (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num + arg2.Num);
                case otyType.String:
                    return new otyObj(arg1.Str + arg2.Str);
                case otyType.Double:
                    return new otyObj(arg1.Double + arg2.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj Sub(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer: return new otyObj((void*)(arg1.Ptr.Address - (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num - arg2.Num);
                case otyType.Double: return new otyObj(arg1.Double - arg2.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj Multipli(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer: return new otyObj((void*)(arg1.Ptr.Address * (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num * arg2.Num);
                case otyType.Double: return new otyObj(arg1.Double * arg2.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj Division(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer: return new otyObj((void*)(arg1.Ptr.Address / (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num / arg2.Num);
                case otyType.Double: return new otyObj(arg1.Double / arg2.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj Modulo(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer: return new otyObj((void*)(arg1.Ptr.Address % (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num % arg2.Num);
                case otyType.Double: return new otyObj(arg1.Double % arg2.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj LeftShift(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer: return new otyObj((void*)(arg1.Ptr.Address << (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num << arg2.Num);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj RightShift(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer: return new otyObj((void*)(arg1.Ptr.Address >> (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num >> arg2.Num);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj Less(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                */case otyType.Int32:
                    return Bool(arg1.Num < arg2.Num);
                case otyType.Double: return Bool(arg1.Double < arg2.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj LessEqual(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                */case otyType.Int32:
                    return Bool(arg1.Num <= arg2.Num);
                case otyType.Double: return Bool(arg1.Double <= arg2.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj Greater(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                */case otyType.Int32:
                    return Bool(arg1.Num > arg2.Num);
                case otyType.Double: return Bool(arg1.Double > arg2.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj GreaterEqual(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                */case otyType.Int32:
                    return Bool(arg1.Num >= arg2.Num);
                case otyType.Double: return Bool(arg1.Double >= arg2.Double);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj Equal(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                */case otyType.Int32:
                    return Bool(arg1.Num == arg2.Num);
                case otyType.String:
                    return Bool(arg1.Str == arg2.Str);
                case otyType.Double:
                    return Bool(arg1.Double == arg2.Double);
            }
            return Bool(arg1.Obj == arg2.Obj);
        }
        public static otyObj NotEqual(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                */case otyType.Int32:
                    return Bool(arg1.Num != arg2.Num);
                case otyType.String:
                    return Bool(arg1.Str != arg2.Str);
                case otyType.Double: return Bool(arg1.Double != arg2.Double);
            }
            return Bool(arg1.Obj != arg2.Obj);
        }
        public static otyObj Not(otyObj arg1)
        {
            switch (arg1.Type)
            {
                case otyType.Int32:
                    return new otyObj(~arg1.Num);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'は演算できません。");
        }
        public static otyObj LogicNot(otyObj arg1)
        {
            switch (arg1.Type)
            {
                case otyType.Int32:
                    if (arg1.Num == 0 || arg1.Num == null)
                        return new otyObj(1);
                    return new otyObj(0);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'は演算できません。");
        }
        public static otyObj And(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                    return new otyObj((void*)(arg1.Ptr.Address & (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num & arg2.Num);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj LogicAnd(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                case otyType.Int32:
                    if ((arg1.Num == 0 || arg1.Num == null) && (arg2.Num == 0 || arg2.Num == null))
                        return new otyObj(1);
                    return new otyObj(0);
                case otyType.Double:
                    if ((arg1.Double == 0 || arg1.Double == null) && (arg2.Double == 0 || arg2.Double == null))
                        return new otyObj(1);
                    return new otyObj(0);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj Xor(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                    return new otyObj((void*)(arg1.Ptr.Address ^ (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num ^ arg2.Num);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj Or(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                /*case otyType.Pointer:
                    return new otyObj((void*)(arg1.Ptr.Address | (int)arg2.Num));
                */case otyType.Int32:
                    return new otyObj(arg1.Num | arg2.Num);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static otyObj LogicOr(otyObj arg1, otyObj arg2)
        {
            switch (arg1.Type)
            {
                case otyType.Int32:
                    if ((arg1.Num == 0 || arg1.Num == null) || (arg2.Num == 0 || arg2.Num == null))
                        return new otyObj(1);
                    return new otyObj(0);
                case otyType.Double:
                    if ((arg1.Double == 0 || arg1.Double == null) || (arg2.Double == 0 || arg2.Double == null))
                        return new otyObj(1);
                    return new otyObj(0);
            }
            throw new ArgumentException("oty型'" + arg1.Type + "'とoty型'" + arg2.Type + "'は演算できません。");
        }
        public static void PtrSetObj(otyObj arg1, otyObj arg2)
        {
            var ptr = arg1.Ptr.Pointer;
            //var hoge = *ptr;
            switch (arg2.Type)
            {
                case otyType.Int32:
                    *((int*)ptr) = (int)arg2.Num;
                    break;
                case otyType.Double:
                    *((Double*)ptr) = (Double)arg2.Double;
                    break;
                case otyType.Char:
                    *((char*)ptr) = (char)arg2.Char;
                    break;
                case otyType.String:
                    for (int i = 0; i < arg2.Str.Length; i++)
                    {
                        var c = arg2.Str[i];
                        *((char*)ptr + (i/**sizeof(char)*/)) = c;
                    }
                    *((char*)ptr + arg2.Str.Length/* * sizeof(char)*/) = '\0';
                    break;
            }
        }
    }
}
