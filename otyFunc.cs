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
                    case "add":
                        return new otyObj(oo[0].Num + oo[1].Num, null, 0);
                    case "tostr":
                        return new otyObj(oo[0].Obj.ToString());
                    case "tonum":
                        return new otyObj(Convert.ToInt32(oo[0].Obj));
                    case "todbl":
                        return new otyObj(Convert.ToDouble(oo[0].Obj));
                    default:
                        var scope = new otyRun(new otypar
                        {
                            result = or.result//result = this.result.GetRange(i + 1, this.result.Count - i - 1)
                        }, this.Function[name].index+1, or);
                        int j=0;
                        foreach (var i in oo)
                        {
                            scope.Variable.Add(this.Function[name].Param[j],i);
                                j++;
                        }
                        scope.FuncFlg = true;
                        var result = scope.Run();
                        return result;

                }
            }
            catch (FormatException)//(ArgumentException)
            {
                throw new ArgumentException("引数が足りません。"+name+"組み込み関数");
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
