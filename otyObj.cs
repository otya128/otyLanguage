using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    public unsafe class otyPtrObj
    {
        public otyPtrObj(void* ptr)
        {
            
            this.Pointer = ptr;
        }
        public void* Pointer;
        /// <summary>
        /// ポインタのアドレスを取得または設定します。
        /// </summary>
        public int Address {
            get
            {
                return (int)this.Pointer;
            }
            set
            {
                this.Pointer = (void*)value;
            }
        }
        public override string ToString()
        {
            return this.Address.ToString();
        }
    }
    public unsafe class otyObjNull:otyObj
    {
        public override object Obj
        {
            get
            {
                return base.obj;
            }
            set
            {
                if (this.IsConst) throw new InvalidOperationException("この値は書き換えられません。" + Obj + "=" + value);
            }
        }
    }
    public unsafe class otyObj
    {
        public bool isNull()
        {
            return this.Obj == null;
        }
        public static otyObjNull NULL = new otyObjNull { IsConst = true };
        public static otyObj Void = new otyObj { IsConst = true };//区別
        public otyObj()
        {
        }
        public static otyObj CreateNullObj(List<otyParc> r, int i)
        {
            return new otyObj((object)null,r,i,otyType.Object);
        }
        public static otyObj CreateArrayObj(int length,List<otyParc> r, int i,otyType Type)
        {
            var obj = new otyObj[length];
            switch (Type)
            {
                case otyType.Int32:
                    for (int j = 0; j < length; j++)
                    {
                        obj[j] = new otyObj(0);
                    }
                    break;
                case otyType.Object:
                    for (int j = 0; j < length; j++)
                    {
                        obj[j] = new otyObj((object)null);
                    }
                    break;
            }
            return new otyObj(obj, r, i, otyType.Array);
        }
        public otyObj(object obj, List<otyParc> r, int i, otyType Type = otyType.Object)
        {
            this.Type = Type;
            this.Obj = obj;
            this.result = r;
            this.index = i;
        }
        public otyObj(int obj, List<otyParc> r, int i, otyType Type = otyType.Int32)
        {
            this.Type = Type;
            this.Num = obj;
            this.result = r;
            this.index = i;
        }
        public otyObj(string obj, List<otyParc> r, int i, otyType Type = otyType.String)
        {
            this.Type = Type;
            this.Str = obj;
            this.result = r;
            this.index = i;
        }
        public otyObj(otyObj[] obj, List<otyParc> r, int i, otyType Type = otyType.Array)
        {
            this.Type = Type;
            this.Array = obj;
            this.result = r;
            this.index = i;
        }
        public unsafe otyObj(void* obj, List<otyParc> r, int i, otyType Type = otyType.Pointer)
        {
            this.Type = Type;
            this.Ptr = new otyPtrObj(obj);
            this.result = r;
            this.index = i;
        }
        public otyObj(char obj, List<otyParc> r, int i, otyType Type = otyType.Array)
        {
            this.Type = Type;
            this.Char = obj;
            this.result = r;
            this.index = i;
        }
        public otyObj(object obj)
        {
            this.Obj = obj;
        }
        public otyObj(int obj)
        {
            this.Num = obj;
        }
        public otyObj(string obj)
        {
            this.Str = obj;
        }
        public otyObj(otyObj[] obj)
        {
            this.Array = obj;
        }
        public otyObj(otyFuncObj obj)
        {
            this.Function = obj;
        }
        public unsafe otyObj(void* obj)
        {
            this.Ptr = new otyPtrObj(obj);
        }
        public otyObj(char obj)
        {
            this.Char = obj;
        }
        public void Add(otyObj arg2)
        {

            switch (this.Type)
            {
                case otyType.Pointer:
                case otyType.Int32:
                    this.Num += arg2.Num;//new otyObj(arg1.Num + arg2.Num);
                    break;
                case otyType.Char:
                    this.Char += arg2.Char;
                    break;
                case otyType.Double:
                    this.Double += arg2.Double;//new otyObj(arg1.Num + arg2.Num);
                    break;
                case otyType.String:
                    if (arg2.Obj != null) this.Str += arg2.Obj.ToString();
                    break;
            }
        }
        public void Sub(otyObj arg2)
        {

            switch (this.Type)
            {
                case otyType.Pointer:
                case otyType.Int32:
                    this.Num -= arg2.Num;//new otyObj(arg1.Num + arg2.Num);
                    break;
                case otyType.Char:
                    this.Char -= arg2.Char;
                    break;
                case otyType.Double:
                    this.Double -= arg2.Double;//new otyObj(arg1.Num + arg2.Num);
                    break;
            }
        }
        public otyObjType ObjType;
        public bool IsConst
        {
            get
            {
                return this.ObjType.HasFlag(otyObjType.Const);
            }
            set
            {
                if (value)
                    this.ObjType |= otyObjType.Const;
                else this.ObjType &= this.ObjType | ~otyObjType.Const;
            }
        }
        public otyType Type = otyType.Int32;
        protected object obj;
        public virtual object Obj
        {
            get
            {
                return this.obj;
            }
            set
            {
                if (this.IsConst) throw new InvalidOperationException("この値は書き換えられません。" + Obj + "=" + value);
                if (value != null)
                    if (value.GetType().Name == "Int32")
                        Type = otyType.Int32;
                    else
                        if (value.GetType().Name == "Double")
                            Type = otyType.Double;
                        else
                            if (value.GetType().Name == "String")
                                Type = otyType.String;
                            else if (value.GetType().Name == "otyObj[]")
                                Type = otyType.Array;
                            else if (value.GetType().Name == "otyFuncObj")
                                Type = otyType.Function;
                            else if (value.GetType().Name == "otyPtrObj")
                                Type = otyType.Pointer;
                            else if (value.GetType().Name == "Char")
                                Type = otyType.Char;
                obj = value;
            }
        }
        public List<otyParc> result;
        public int index;
        public otyObj[] Array
        {
            get
            {
                return (otyObj[])this.Obj;
            }
            set
            {
                this.Type = otyType.Array;
                this.Obj = value;
            }
        }
        public otyPtrObj Ptr
        {
            get
            {
                return (otyPtrObj)this.Obj;
            }
            set
            {
                this.Type = otyType.Pointer;
                this.Obj = value;
            }
        }
        public char Char
        {
            get
            {
                return (char)this.Obj;
            }
            set
            {
                this.Type = otyType.Char;
                this.Obj = value;
            }
        }
        public otyFuncObj Function
        {
            get
            {
                return (otyFuncObj)this.Obj;
            }
            set
            {
                this.Type = otyType.Function;
                this.Obj = value;
            }
        }
        public unsafe int? Num
        {
            get
            {
                if (this.Type == otyType.Pointer) return this.Ptr.Address;
                return this.Obj as Int32?;
            }
            set
            {
                if (this.Type == otyType.Pointer) { 
                    var ptr = this.Ptr; 
                    ptr.Address = (int)value; return; 
                }
                this.Type = otyType.Int32;
                this.Obj = value;
            }
        }
        public string Str
        {
            get
            {
                return this.Obj as string;
            }
            set
            {
                this.Type = otyType.String;
                this.Obj = value;
            }
        }
        public Double? Double
        {
            get
            {
                return this.Obj as Double?;
            }
            set
            {
                this.Type = otyType.Double;
                this.Obj = value;
            }
        }

        internal void Increment()
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    this.Num++;
                    break;
                case otyType.Double:
                    this.Double++;
                    break;
            }
        }
        public void Decrement()
        {
            switch (this.Type)
            {
                case otyType.Pointer:
                case otyType.Int32:
                    this.Num--;
                    break;
                case otyType.Double:
                    this.Double--;
                    break;
            }
        }

        public otyObj Less(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Pointer:
                case otyType.Int32:
                    if (this.Num < arg2.Num) return new otyObj(1); else return new otyObj(0);
                case otyType.Double:
                    if (this.Double < arg2.Double) return new otyObj(1); else return new otyObj(0);
            }
            throw new InvalidOperationException("oty型'" + this.Type + "'とoty型'" + arg2.Type + "'は比較できません。");
        }
        public otyObj Greater(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Pointer:
                case otyType.Int32:
                    if (this.Num > arg2.Num) return new otyObj(1); else return new otyObj(0);
                case otyType.Double:
                    if (this.Double > arg2.Double) return new otyObj(1); else return new otyObj(0);
            } 
            throw new InvalidOperationException("oty型'" + this.Type + "'とoty型'" + arg2.Type + "'は比較できません。");
        }
        public otyObj LessEqual(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Pointer:
                case otyType.Int32:
                    if (this.Num <= arg2.Num) return new otyObj(1); else return new otyObj(0);
                case otyType.Double:
                    if (this.Double <= arg2.Double) return new otyObj(1); else return new otyObj(0);
            }
            throw new InvalidOperationException("oty型'" + this.Type + "'とoty型'" + arg2.Type + "'は比較できません。");
        }
        public otyObj GreaterEqual(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Pointer:
                case otyType.Int32:
                    if (this.Num >= arg2.Num) return new otyObj(1); else return new otyObj(0);
                case otyType.Double:
                    if (this.Double >= arg2.Double) return new otyObj(1); else return new otyObj(0);
            }
            throw new InvalidOperationException("oty型'" + this.Type + "'とoty型'" + arg2.Type + "'は比較できません。");
        }
        public otyObj Equal(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Pointer:
                case otyType.Int32:
                    if (this.Num == arg2.Num) return new otyObj(1); else return new otyObj(0);
                case otyType.Char:
                    if (this.Char == arg2.Char) return new otyObj(1); else return new otyObj(0);
                case otyType.Double:
                    if (this.Double == arg2.Double) return new otyObj(1); else return new otyObj(0);
                case otyType.String:
                    if (this.Str == arg2.Str) return new otyObj(1); else return new otyObj(0);
            }
            if (this.Obj == arg2.Obj) return new otyObj(1); else return new otyObj(0);
        }
        public void Modulo(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    this.Num %= arg2.Num;
                    break;
                case otyType.Pointer:
                    this.Num %= arg2.Num;
                    break;
                case otyType.Double:
                    this.Double %= arg2.Double;
                    break;
            }
        }
        public void Division(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    this.Num /= arg2.Num;
                    break;
                case otyType.Pointer:
                    this.Num /= arg2.Num;
                    break;
                case otyType.Double:
                    this.Double /= arg2.Double;
                    break;
            }
        }
        public void Multply(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    this.Num *= arg2.Num;
                    break;
                case otyType.Pointer:
                    this.Num *= arg2.Num;
                    break;
                case otyType.Double:
                    this.Double *= arg2.Double;
                    break;
            }
        }
        /// <summary>
        /// &lt;&lt;
        /// </summary>
        /// <param name="arg2"></param>
        public void LeftShift(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    this.Num <<= arg2.Num;
                    break;
            }
           // throw new InvalidOperationException("演算子<<をoty型'" + this.Type + "'とoty型'" + arg2.Type + "'に適用できません。");
        }
        /// <summary>
        /// &gt;&gt;
        /// </summary>
        /// <param name="arg2"></param>
        public void RightShift(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    this.Num >>= arg2.Num;
                    break;
            }
           // throw new InvalidOperationException("演算子>>をoty型'" + this.Type + "'とoty型'" + arg2.Type + "'に適用できません。");
        }
        public static otyType ToType(string type)
        {
            switch (type)
            {
                case "Double":
                case "double":
                    return otyType.Double;
                case "Int32":
                case "int":
                    return otyType.Int32;
                case "Char":
                case "char":
                    return otyType.Char;
                case "String":
                case "string":
                    return otyType.String;
                case "Object":
                case "object":
                    return otyType.Object;
            }
            throw new InvalidCastException("型" + type + "は存在しません。");
        }
        public void Cast(string type)
        {
            var t = otyObj.ToType(type);
            switch (t)
            {
                case otyType.Int32:
                    if (this.Type == otyType.Double || this.Type == otyType.Int32 || this.Type == otyType.Char)
                    {
                        this.Num = Convert.ToInt32(this.Obj);
                    }
                    else
                    {
                        if (this.Type == otyType.Object)
                        {
                            this.Num = (Int32)this.Obj;
                        }
                        throw new InvalidCastException("oty型" + this.Type + "を" + t + "にキャストできません。");
                    }
                    break;
                case otyType.Char:
                    if (this.Type == otyType.Double || this.Type == otyType.Int32 || this.Type == otyType.Char)
                    {
                        this.Char = Convert.ToChar(this.Obj);
                    }
                    else
                    {
                        if (this.Type == otyType.Object)
                        {
                            this.Char = (Char)this.Obj;
                        }
                        throw new InvalidCastException("oty型" + this.Type + "を" + t + "にキャストできません。");
                    }
                    break;
                case otyType.Double:
                    if (this.Type == otyType.Double || this.Type == otyType.Int32 || this.Type == otyType.Char)
                    {
                        this.Double = Convert.ToDouble(this.Obj);
                    }
                    else
                    {
                        if (this.Type == otyType.Object)
                        {
                            this.Double = (Double)this.Obj;
                        }
                        throw new InvalidCastException("oty型" + this.Type + "を" + t + "にキャストできません。");
                    }
                    break;
                case otyType.Object:
                    this.obj = (Object)this.Obj;
                    this.Type = otyType.Object;
                    break;
                default:
                    throw new InvalidCastException("oty型" + this.Type + "を" + t + "にキャストできません。");
                    break;
            }
        }
        public unsafe otyObj PtrCast(string type)
        {
            var t = otyObj.ToType(type);
            if (this.Type != otyType.Int32 && this.Type != otyType.Pointer)
            {
                throw new InvalidCastException("oty型" + this.Type + "を" + t + "にキャストできません。");
            }
            switch (t)
            {
                case otyType.Int32:
                    return new otyObj(*((Int32*)((int)this.Num)));
                case otyType.Double:
                    return new otyObj(*((double*)((int)this.Num)));
                case otyType.String:
                    return new otyObj(new String((char*)((int)this.Num)));
                default:
                    throw new InvalidCastException("oty型" + this.Type + "を" + t + "にキャストできません。");
            }
        }
        public otyObj Func(string name, List<otyObj> arg)
        {
            switch (name)
            {
                case"GetType":
                    return new otyObj(this.Type.ToString());
                case "GetCSType":
                    return new otyObj(this.Obj.GetType().ToString());
            }
            switch (this.Type)
            {
                case otyType.Int32:
                    switch (name)
                    {
                        case "Add":
                            if (arg.Count > 0)
                            {
                                return new otyObj(/*this.Num.ToString()*/this.Num + arg[0].Num);
                            }
                            //this.Str = this.Num.ToString();
                            return new otyObj(/*this.Num.ToString()*/this.Num + 1);
                        case "ToString":
                            return new otyObj(/*this.Num.ToString()*/this.Obj.ToString());
                        default:
                            throw new MissingMethodException("oty型" + this.Type + "に" + name + "関数の定義がありません。");
                    }
                case otyType.String:
                    switch (name)
                    {
                        case "Replace":
                            if (arg.Count > 1)
                            {
                                return new otyObj(this.Str.Replace(arg[0].Str, arg[1].Str));
                            }

                            throw new ArgumentException("引数が足りません");
                        case "IndexOf":
                            if (arg.Count > 0)
                            {
                                return new otyObj(this.Str.IndexOf(arg[0].Str));
                            }

                            throw new ArgumentException("引数が足りません");
                        case "Substring":
                            if (arg.Count == 1)
                            {
                                return new otyObj(this.Str.Substring((int)arg[0].Num));
                            }
                            if (arg.Count > 1)
                            {
                                return new otyObj(this.Str.Substring((int)arg[0].Num, (int)arg[1].Num));
                            }

                            throw new ArgumentException("引数が足りません");
                        case "ToString":
                            return new otyObj(/*this.Num.ToString()*/this.Obj.ToString());
                        default:
                            throw new MissingMethodException("oty型" + this.Type + "に" + name + "関数の定義がありません。");
                    }
                case otyType.Array:
                    switch (name)
                    {
                        case "Length":
                            return new otyObj(this.Array.Length);
                        case "ToString":
                            return new otyObj(/*this.Num.ToString()*/this.Obj.ToString());
                        case "ReAlloc":
                            var re = new otyObj[(int)arg[0].Num];
                            for (int i = 0; i < this.Array.Length; i++)
                            {
                                re[i] = this.Array[i];
                            }
                            for (int i = this.Array.Length; i < arg[0].Num; i++)
                            {
                                re[i] = new otyObj();
                            }
                            return new otyObj(re);
                        default:
                            throw new MissingMethodException("oty型" + this.Type + "に" + name + "関数の定義がありません。");
                    }
            }
            //return otyObj.NULL;
            throw new MissingMethodException("oty型"+this.Type+"に"+name+"関数の定義がありません。");
        }
        public otyObj GetMember(string name)
        {
           
            switch (this.Type)
            {
                case otyType.String:
                    switch (name)
                    {
                        case "Length":
                            return new otyObj(this.Str.Length);
                        default:
                            throw new MissingMethodException("oty型" + this.Type + "に" + name + "関数の定義がありません。");
                    }
                case otyType.Array:
                    switch (name)
                    {
                        case "Length":
                            return new otyObj(this.Array.Length);
                        default:
                            throw new MissingMethodException("oty型" + this.Type + "に" + name + "関数の定義がありません。");
                    }
            }
            //return otyObj.NULL;
            throw new MissingMethodException("oty型" + this.Type + "に" + name + "関数の定義がありません。");
        }
        public otyObj Indexer(int index)
        {
            switch (this.Type)
            {
                case otyType.Array:
                    return this.Array[index];
                case otyType.String:
                    return new otyObj(this.Str[index]);
                case otyType.Pointer:
                    return new otyObj((void*)(this.Ptr.Address + index));
            }
            throw new ArgumentException("[]を"+this.Type+"に適用できません?");
        }
    }
}
