using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    public class otyObj
    {
        public bool isNull()
        {
            return this.Obj == null;
        }
        public static otyObj NULL = new otyObj();
        public static otyObj Void = new otyObj();//区別
        otyObj()
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
        public void Add(otyObj arg2)
        {

            switch (this.Type)
            {
                case otyType.Int32:
                    this.Num += arg2.Num;//new otyObj(arg1.Num + arg2.Num);
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
                case otyType.Int32:
                    this.Num -= arg2.Num;//new otyObj(arg1.Num + arg2.Num);
                    break;
                case otyType.Double:
                    this.Double -= arg2.Double;//new otyObj(arg1.Num + arg2.Num);
                    break;
            }
        }

        public otyType Type = otyType.Int32;
        object obj;
        public object Obj
        {
            get
            {
                return this.obj;
            }
            set
            {
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
        public int? Num
        {
            get
            {
                return this.Obj as Int32?;
            }
            set
            {
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
                case otyType.Int32:
                    if (this.Num < arg2.Num) return new otyObj(1); else return new otyObj(0);
                    break;
                case otyType.Double:
                    if (this.Double < arg2.Double) return new otyObj(1); else return new otyObj(0);
                    break;
            }
            throw new InvalidOperationException("oty型'" + this.Type + "'とoty型'" + arg2.Type + "'は比較できません。");
        }
        public otyObj Greater(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    if (this.Num > arg2.Num) return new otyObj(1); else return new otyObj(0);
                    break;
                case otyType.Double:
                    if (this.Double > arg2.Double) return new otyObj(1); else return new otyObj(0);
                    break;
            } 
            throw new InvalidOperationException("oty型'" + this.Type + "'とoty型'" + arg2.Type + "'は比較できません。");
        }
        public otyObj LessEqual(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    if (this.Num <= arg2.Num) return new otyObj(1); else return new otyObj(0);
                    break;
                case otyType.Double:
                    if (this.Double <= arg2.Double) return new otyObj(1); else return new otyObj(0);
                    break;
            }
            throw new InvalidOperationException("oty型'" + this.Type + "'とoty型'" + arg2.Type + "'は比較できません。");
        }
        public otyObj GreaterEqual(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    if (this.Num >= arg2.Num) return new otyObj(1); else return new otyObj(0);
                    break;
                case otyType.Double:
                    if (this.Double >= arg2.Double) return new otyObj(1); else return new otyObj(0);
                    break;
            }
            throw new InvalidOperationException("oty型'" + this.Type + "'とoty型'" + arg2.Type + "'は比較できません。");
        }
        public otyObj Equal(otyObj arg2)
        {
            switch (this.Type)
            {
                case otyType.Int32:
                    if (this.Num == arg2.Num) return new otyObj(1); else return new otyObj(0);
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
                case otyType.Double:
                    this.Double /= arg2.Double;
                    break;
            }
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
                    if (this.Type == otyType.Double)
                    {
                        this.Num = (Int32)this.Double;
                        
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
                case otyType.Double:
                    if (this.Type == otyType.Int32)
                    {
                        this.Double = (Double)this.Num;
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
                        case "ToString":
                            return new otyObj(/*this.Num.ToString()*/this.Obj.ToString());
                        default:
                            throw new MissingMethodException("oty型" + this.Type + "に" + name + "関数の定義がありません。");
                    }
            }
            //return otyObj.NULL;
            throw new MissingMethodException("oty型"+this.Type+"に"+name+"関数の定義がありません。");
        }

        
    }
}
