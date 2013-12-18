using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    public class otyFunc
    {
        public otyObj RunFunc(string name,List<otyObj> oo)
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
    }
}
