using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    public partial class otyFunc
    {
        public otyObj RunFunc(string name,List<otyObj> oo,otyRun or)
        {
            try
            {
                switch (name)
                {
                    case "print":
                        foreach (var i in oo)
                        {
                            if (i.isNull())
                            {
                                Console.Write("[Null]\t");
                            }
                            else
                            {
                                Console.Write(i.Obj + "\t");
                            }
                        }
                        // Console.WriteLine();
                        break;
                    case "printf":
                        List<Object> format=new List<object>();
                        for (int i = 1; i < oo.Count; i++)
                        {
                            format.Add(oo[i].Obj);
                        }
                        Console.Write(String.Format(Convert.ToString(oo[0].Obj),format.ToArray()));

                        // Console.WriteLine();
                        break;
                    case "add":
                        return new otyObj(oo[0].Num + oo[1].Num, null, 0);
                    case "tostr":
                        return new otyObj(oo[0].Obj.ToString());
                    case "tonum":
                        return new otyObj(Convert.ToInt32(oo[0].Obj));
                    case "todbl":
                        return new otyObj(Convert.ToDouble(oo[0].Obj));
                    case "abs":
                        return new otyObj(Math.Abs((double)oo[0].Double));
                    case "acos":
                        return new otyObj(Math.Acos((double)oo[0].Double));
                    case "asin":
                        return new otyObj(Math.Asin((double)oo[0].Double));
                    case "atan":
                        return new otyObj(Math.Atan((double)oo[0].Double));
                    case "atan2":
                        return new otyObj(Math.Atan2((double)oo[0].Double, (double)oo[1].Double));
                    case "bigmul":
                        return new otyObj(Math.BigMul((int)oo[0].Num, (int)oo[1].Num));
                    case "ceiling":
                        return new otyObj(Math.Ceiling((double)oo[0].Double));
                    case "cos":
                        return new otyObj(Math.Cos((double)oo[0].Double));
                    case "cosh":
                        return new otyObj(Math.Cosh((double)oo[0].Double));
                    //case "divrem":
                    //    return new otyObj(Math.DivRem((double)oo[0].Double));
                    case "exp":
                        return new otyObj(Math.Exp((double)oo[0].Double));
                    case "floor":
                        return new otyObj(Math.Floor((double)oo[0].Double));
                    case "IEEERemainder":
                        return new otyObj(Math.IEEERemainder((double)oo[0].Double, (double)oo[1].Double));
                    case "log":
                        return new otyObj(Math.Log((double)oo[0].Double));
                    case "log10":
                        return new otyObj(Math.Log10((double)oo[0].Double));
                    case "max":
                        return new otyObj(Math.Max((double)oo[0].Double, (double)oo[1].Double));
                    case "min":
                        return new otyObj(Math.Min((double)oo[0].Double, (double)oo[1].Double));
                    case "pow":
                        return new otyObj(Math.Pow((double)oo[0].Double, (double)oo[1].Double));
                    case "round":
                        return new otyObj(Math.Round((double)oo[0].Double));
                    case "sign":
                        return new otyObj(Math.Sign((double)oo[0].Double));
                    case "sin":
                        return new otyObj(Math.Sin((double)oo[0].Double));
                    case "sinh":
                        return new otyObj(Math.Sinh((double)oo[0].Double));
                    case "sqrt":
                        return new otyObj(Math.Sqrt((double)oo[0].Double));
                    case "tan":
                        return new otyObj(Math.Tan((double)oo[0].Double));
                    case "tanh":
                        return new otyObj(Math.Tanh((double)oo[0].Double));
                    case "truncate":
                        return new otyObj(Math.Truncate((double)oo[0].Double));
                    case "malloc":
                        unsafe
                        {
                            var ptr = new byte[(int)oo[0].Num];
                            fixed (byte* ptr2 = ptr)
                                return new otyObj(ptr2);
                        }
                    default:
                        var scope = new otyRun(new otypar
                        {
                            result = or.result//result = this.result.GetRange(i + 1, this.result.Count - i - 1)
                        }, this.Function[name].index + 1, or);
                        int j = 0;
                        foreach (var i in oo)
                        {
                            scope.Variable.Add(this.Function[name].Param[j], i);
                            j++;
                        }
                        scope.FuncFlg = true;
                        var result = scope.Run();
                        return result;

                }
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("引数が足りません。" + name + "関数");
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("引数の型が違います。" + name + "関数");
            }
            return otyObj.NULL;
        }
        public static otyObj RunFunc(string name, List<otyObj> oo,otyObj obj)
        {
            try
            {
                switch (name)
                {
                    case "print":
                        foreach (var i in oo)
                        {
                            Console.Write(i.Obj + "\t");
                        }
                        // Console.WriteLine();
                        break;
                    
                    case "add":
                        return new otyObj(oo[0].Num + oo[1].Num, null, 0);
                    case "tostr":
                        return new otyObj(oo[0].Obj.ToString());
                    case "tonum":
                        return new otyObj(Convert.ToInt32(oo[0].Obj));
                    case "todbl":
                        return new otyObj(Convert.ToDouble(oo[0].Obj));
                }
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("引数が足りません。" + name + "組み込み関数");
            }
            return otyObj.NULL;
        }
        public static string Decode(string arg)
        {
            return arg.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\a","\a")
                .Replace("\\t", "\t")
                .Replace("\\a", "\a")
                .Replace("\\a", "\a");
        }

        public otyFunc()
        {
            // TODO: Complete member initialization
        }
    }
}
