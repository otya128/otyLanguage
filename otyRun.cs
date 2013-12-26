using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    public enum otyType
    {
        Object, Int32, Double, String, Array, Function, Pointer, Char
    }
    /*
    public class otyType
    {
        public const string Int32 = "int";
        public const string Double = "double";
        public const string String = "string";
    }*/
    public class otyVar
    {
        public static otyVar Empty = new otyVar();
        public otyVar Parent=Empty;
        public otyRun Variable;
        //public Dictionary<string, otyObj> Variable = new Dictionary<string, otyObj>();
        public otyObj this[string name]
        {
            get
            {
                if (this.Variable.Variable.ContainsKey(name))
                {
                    //return getDic(this.Variable.Variable, name);
                    return this.Variable.Variable[name];
                }
                if (this.Variable.Parent != otyRun.Empty)
                {
                    var ret=getVariable(this.Variable.Parent, name);
                    return ret;
                }
                throw new ArgumentException("変数" + name + "が存在しません。");
            }
            set
            {
                if (this.Variable.Variable.ContainsKey(name))
                {
                    this.Variable.Variable[name]=value;
                }
                if (this.Variable.Parent != otyRun.Empty)
                {
                    if (setVariable(this.Variable, name, value)) return;
                }
                this.Variable.Variable[name] = value;
            }
        }
        bool setVariable(otyRun ov, string name,otyObj value)
        {
            if (ov.Variable.ContainsKey(name))
            {
                ov.Variable[name] = value;
                return true;
            }
            if (ov.Parent != otyRun.Empty)
            {
                return setVariable(ov.Parent, name,value);
            }
            return false;
        }
        otyObj getDic(Dictionary<string, otyObj> dic, string Key)
        {
            foreach (var fic in dic)
            {
                Console.WriteLine(fic);
                if (fic.Key == Key) 
                    return fic.Value;
            }
            throw new KeyNotFoundException();
        }
        otyObj getVariable(otyRun ov,string name)
        {
            if (ov.Variable.ContainsKey(name))
            {
                var ret = ov.Variable[name];//getDic(ov.Variable, name);
                return ret;
                try
                {
                    return ov.Variable[name];
                }
                catch
                {
                    Console.WriteLine("DEBUG!!!!!!!!!!!");
                }
            }
            if (ov.Parent != otyRun.Empty)
            {
                return getVariable(ov.Parent, name);
            }
            throw new ArgumentException("変数" + name + "が存在しません。");
        }
        public bool ContainsKey(string name)
        {
            if (this.Variable.Variable.ContainsKey(name))
            {
                return true;

            }
            if (this.Variable.Parent != otyRun.Empty)
            {
                return ContainsKey(this.Variable.Parent, name);
            }
            return false;
        }
        public bool ContainsKey(otyRun ov, string name)
        {
            if (ov.Variable.ContainsKey(name))
            {
                return true;
                
            }
            if (ov.Parent != otyRun.Empty)
            {
                return ContainsKey(ov.Parent, name);
            }
            return false;
        }
    }
    public partial class otyRun
    {
        public otyRun Parent=Empty;
        public static otyRun Empty=new otyRun();
        public Dictionary<string, otyObj> Variable;
        public otyVar Var;
        public List<otyParc> result;
        //public otyFunc DefFunc;
        otyFunc defFunc;
        public bool FuncFlg = false;
        public otyFunc DefFunc
        {
            get
            {
                if (this.Parent == Empty)
                {
                    return this.defFunc;
                }
                return this.Parent.DefFunc;
            }
        }
        public otyRun()
        {
            Var = new otyVar { Variable = this };
            this.Variable = new Dictionary<string, otyObj>();
            this.result = new List<otyParc>();
            defFunc = new otyFunc();
        }
        otypar otypar;
        public otyRun(otypar r)
        {
            Var = new otyVar { Variable = this };
            this.Variable = new Dictionary<string, otyObj>();
            this.result = r.result;
            this.defFunc = new otyFunc(r,this);
            if (defFunc.Function.ContainsKey("main"))
            {
                //main!!!!!!!!!!11
                this.startindex = defFunc.Function["main"].index;
            }
            this.otypar = r;
            //DefFunc = new otyFunc(r);
        }
        public otyRun(otypar r,int index,otyRun p)
        {
            Var = new otyVar { Variable = this };
            this.Variable = new Dictionary<string, otyObj>();
            this.result = r.result;
            this.startindex = index;
            this.index = index;
            this.Parent = p; this.otypar = r;
            //DefFunc = new otyFunc();
        }
        int startindex=0;
        public int index = 0;
        public bool FuncReturn=false;
        private enum otyrunstate
        {
            None, IdenRun, NumRun, StrRun, FuncRun, ForRun, IfRun, IfSkip,
            Return,
        }
        int BlockSkip(int index,int block=0)
        {
            while (index < result.Count)
            {
                var j = result[index];
                switch (j.otyParnum)
                {
                    case otyParnum.blockstart:
                        block++;
                        break;
                    case otyParnum.blockend:
                        block--;
                        if (block == -1)
                            return index;
                        break;
                }

                index++;

            }
            return index;
        }
        //カッコ飛ばし
        int ParentSkip(int index, int block = 0)
        {
            while (index < result.Count)
            {
                var j = result[index];
                switch (j.otyParnum)
                {
                    case otyParnum.leftparent:
                        block++;
                        break;
                    case otyParnum.rightparent:
                        block--;
                        if (block == -1)
                            return index;
                        break;
                }

                index++;

            }
            return index;
        }
        int ArraySkip(int index, int block = 0)
        {
            while (index < result.Count)
            {
                var j = result[index];
                switch (j.otyParnum)
                {
                    case otyParnum.leftbracket:
                        block++;
                        break;
                    case otyParnum.rightbracket:
                        block--;
                        if (block == -1)
                            return index;
                        break;
                }

                index++;

            }
            return index;
        }
        int StateSkip(int index)
        {
            while (index < result.Count)
            {
                var j = result[index];
                switch (j.otyParnum)
                {
                    case otyParnum.semicolon:
                        return index;
                }

                index++;

            }
            return index;
        }
        int If(int index)
        {
            int block = 0;
            while (index < result.Count)
            {
                var j = result[index];
                if (j.otyParnum == otyParnum.leftparent)
                {
                    var cond = Eval(otyObj.CreateNullObj(result, index));//new otyObj(null, result, index));
                    index = cond.index;
                    if (cond.Num != 0)
                    {
                        Console.WriteLine("IFtrue!");
                        return index;
                    }
                    else
                        return index;
                }
                else
                {
                    return index;
                    //error!!
                }
                index++;

            }
            return index;
        }
        public otyObj Run()
        {
            int i = startindex;
            int forstart = 0, forstate1 = -1, forstate2 = -1, forstate3 = -1;
            
            otyRun forscope = otyRun.Empty;
            var state = otyrunstate.None;
            var status2 = otyrunstate.None;
            while (i < result.Count)
            {
                var j = result[i];
                switch (state)
                {
                    case otyrunstate.IfRun:
                        if (j.otyParnum == otyParnum.leftparent)
                        {
                            var ifindex = ParentSkip(i,-1);
                            var cond = Eval(new otyObj(result[i+1].Obj, result, i+1));
                            i = ifindex;//cond.index;
                            if (cond.Num != 0)
                            {
                                state = otyrunstate.None;
                                //Console.WriteLine("IFtrue!");
                            }
                            else
                            {
                                if (result[i].otyParnum == otyParnum.blockstart || result[i + 1].otyParnum == otyParnum.blockstart)
                                {
                                    i = this.BlockSkip(i, -1);//手抜き
                                }
                                else
                                {
                                    i = this.StateSkip(i);
                                }
                                state = otyrunstate.None;
                            }
                        }
                        else
                        {
                            state = otyrunstate.None;
                            //error!!
                        }

                        break;
                    case otyrunstate.ForRun:
                        switch (j.otyParnum)
                        {
                            case otyParnum.identifier:

                                break;
                            case otyParnum.semicolon:
                                if (forstate1 != -1 && forstate2 != -1 && forstate3 == -1) forstate3 = i + 1;
                                if (forstate1 != -1 && forstate2 == -1 && forstate3 == -1) forstate2 = i + 1;
                                break;
                            case otyParnum.leftparent:
                                if (forstate1 == -1) forstate1 = i + 1;
                                //i = this.StateRun(i+1);
                                //開始位置
                                break;
                            case otyParnum.blockstart:
                                if (forscope == otyRun.Empty)
                                {
                                    forstart = i - 1;
                                    forscope = new otyRun(new otypar
                                    {
                                        result = this.result//result = this.result.GetRange(i + 1, this.result.Count - i - 1)
                                    }, i + 1, this);
                                    forscope.StateRun(forstate1);
                                    if (forstate2 == forstate3 - 1)
                                    {
                                        //true
                                        var scopereslut = forscope.Run();
                                        if (forscope.FuncReturn)
                                        {
                                            this.FuncReturn = true;
                                            return scopereslut;
                                        }
                                        i = forscope.index - 1;
                                        forscope.Eval(otyObj.CreateNullObj(forscope.result, forstate3));
                                    }
                                    else
                                    {
                                        var res = forscope.Eval(otyObj.CreateNullObj(forscope.result, forstate2));//new otyObj(null, forscope.result, forstate2));
                                        if ((int)res.Num != 0)
                                        {

                                            var scopereslut = forscope.Run();
                                            if (forscope.FuncReturn)
                                            {
                                                this.FuncReturn = true;
                                                return scopereslut;
                                            }
                                            i = forscope.index - 1;
                                            forscope.Eval(otyObj.CreateNullObj(forscope.result, forstate3));//new otyObj(null, forscope.result, forstate3));
                                        }
                                        else
                                        {
                                            //最初から実行できない
                                            forscope = otyRun.Empty;
                                            forstate1 = -1;
                                            forstate2 = -1;
                                            forstate3 = -1;
                                            i = this.BlockSkip(i + 1);
                                            state = otyrunstate.None;
                                        }
                                    }
                                }
                                else
                                {
                                    if (forstate2 == forstate3 - 1)
                                    {
                                        //true
                                        var scopereslut = forscope.Run();
                                        if (forscope.FuncReturn)
                                        {
                                            this.FuncReturn = true;
                                            return scopereslut;
                                        }
                                        i = forscope.index - 1;
                                        forscope.Eval(otyObj.CreateNullObj(forscope.result, forstate3));
                                    }else
                                    if ((int)forscope.Eval(/*new otyObj(null, forscope.result, forstate2)*/otyObj.CreateNullObj(forscope.result, forstate2)).Num != 0)
                                    {

                                        var scopereslut = forscope.Run();
                                        if (forscope.FuncReturn)
                                        {
                                            this.FuncReturn = true;
                                            return scopereslut;
                                        }
                                        i = forscope.index - 1;
                                        forscope.Eval(otyObj.CreateNullObj(forscope.result, forstate3));//new otyObj(null, forscope.result, forstate3));
                                    }
                                    else
                                    {
                                        i = this.index;
                                        forscope = otyRun.Empty;
                                        forstate1 = -1;
                                        forstate2 = -1;
                                        forstate3 = -1;
                                        state = otyrunstate.None;
                                    }
                                }
                                break;
                            // case otyParnum.semicolon:
                            case otyParnum.blockend:
                                this.index = i;
                                i = forstart;
                                j = result[i];
                                //state = otyrunstate.None;
                                break;
                        }
                        break;
                        goto case otyrunstate.None;
                    case otyrunstate.None:
                        switch (j.otyParnum)
                        {
                            case otyParnum.semicolon:
                                break;
                            case otyParnum.debbug_stop:
                                break;
                            case otyParnum.identifier:
                                switch (j.Name)
                                {
                                    case "for":
                                        state = otyrunstate.ForRun;
                                        forstart = i;
                                        //Sub
                                        break;
                                    case "if":
                                        state = otyrunstate.IfRun;
                                        break;
                                    case "return":
                                        state = otyrunstate.Return;
                                        break;
                                    case "continue":
                                        state = otyrunstate.Return;//未実装
                                        break;
                                    default:
                                        state = otyrunstate.IdenRun;
                                        break;
                                }

                                break;

                            /*case otyParnum.num:
                                var obj = Eval(new otyObj(j.Num, result, i));
                                i = obj.index;
                                //Console.Write(j.otyParnum);Console.WriteLine(obj.Obj);
                                break;*/
                            case otyParnum.blockstart:
                                var scope = new otyRun(new otypar
                                {
                                    result = this.result//result = this.result.GetRange(i + 1, this.result.Count - i - 1)
                                }, i + 1, this);
                                var scopereslut_ = scope.Run();
                                if (scope.FuncReturn)
                                {
                                    i = scope.index+1;
                                    this.index = i;
                                    this.FuncReturn = true;
                                    return scopereslut_;
                                }
                                i = scope.index;
                                break;
                            case otyParnum.blockend:
                                this.index = i;
                                return otyObj.Void;
                            case otyParnum.multiply:
                                var obj = Eval(new otyObj(result[i].Obj, result, i));
                                i = obj.index;
                                break;
                            default:
                                throw new FormatException("認識できないトークン'" + j.Name + "'" + j.otyParnum);
                                //var obj = Eval(new otyObj(result[i].Obj, result, i));
                                //i = obj.index;
                                break;
                        }

                        break;
                    case otyrunstate.IdenRun:
                        switch (j.otyParnum)
                        {
                            case otyParnum.identifier:
                                if (result[i + 1].otyParnum == otyParnum.leftparent)
                                {
                                    i = this.BlockSkip(i, -1);//手抜き
                                    state = otyrunstate.None;
                                    break;
                                }
                                otyObj obj;
                                ///////////////////////debug!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
                                try
                                {
                                    this.Variable[j.Name] = otyObj.NULL;//this.Variable.Add(j.Name, otyObj.NULL);
                                }
                                catch { }
                                obj = Eval(new otyObj(this.Variable[j.Name], result, i));
                                i = obj.index;
                                //Console.Write(j.otyParnum);Console.WriteLine(obj.Obj);
                                //変数宣言

                                state = otyrunstate.None; break;
                            case otyParnum.equal:
                                obj = Eval(new otyObj(this.Var/*iable*/[result[i - 1].Name], result, i - 1));
                                i = obj.index;
                                //Console.Write(j.otyParnum);Console.WriteLine(obj.Obj);
                                //代入
                                state = otyrunstate.None; break;
                            case otyParnum.leftparent:
                                //関数
                                obj = Eval(new otyObj(result[i - 1].Obj, result, i - 1));
                                i = obj.index;
                                //Console.Write(j.otyParnum);Console.WriteLine(obj.Obj);
                                state = otyrunstate.None;
                                break;
                            case otyParnum.leftbracket:
                                int ar = this.ArraySkip(i, -1) + 1;
                                if (result[ar].otyParnum != otyParnum.identifier)
                                {
                                    obj = Eval(new otyObj(this.Var/*iable*/[result[i - 1].Name], result, i - 1));
                                    i = obj.index;
                                    state = otyrunstate.None;
                                    break;
                                }
                                //配列宣言
                                obj = Eval(new otyObj(result[i + 1].Obj, result, i + 1));
                                i = ar;
                                this.Variable[result[i].Name] = otyObj.CreateArrayObj((int)obj.Num, this.result, i, otyType.Object);//new otyObj(new otyObj[(int)obj.Num], this.result, i, otyType.Array);
                                break;
                            default:
                                obj = Eval(new otyObj(this.Var/*iable*/[result[i - 1].Name], result, i - 1));
                                i = obj.index;
                                state = otyrunstate.None;
                                break;
                        }

                        break;
                    case otyrunstate.Return:
                        this.FuncReturn = true;
                            var ret = Eval(new otyObj(j.Obj, result, i));

                            if (ret.isNull())
                            {
                                return otyObj.Void;
                            }
                            else
                                return ret;
                        
                }
                i++;
            }
            return otyObj.Void;
        }
        int StateRun(int index)
        {
            var state = otyrunstate.None;
            otyParc j = result[index];
            while (j.otyParnum != otyParnum.semicolon)
            {
                j = result[index];
                switch (state)
                {
                    case otyrunstate.None:
                        switch (j.otyParnum)
                        {
                            case otyParnum.identifier:
                                state = otyrunstate.IdenRun;
                                break;
                            default:
                                //error!!
                                break;
                        }
                        break;
                    case otyrunstate.IdenRun:
                        switch (j.otyParnum)
                        {
                            case otyParnum.identifier:
                                otyObj obj;
                                ///////////////////////debug!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
                                try
                                {
                                    this.Variable[j.Name] = otyObj.NULL;//this.Variable.Add(j.Name, otyObj.NULL);
                                }
                                catch { }
                                obj = Eval(new otyObj(this.Variable[j.Name], result, index));
                                index = obj.index;
                                //Console.Write(j.otyParnum);Console.WriteLine(obj.Obj);
                                //変数宣言

                                state = otyrunstate.None; break;
                            case otyParnum.equal:
                                obj = Eval(new otyObj(this.Var/*iable*/[result[index - 1].Name], result, index - 1));
                                index = obj.index;
                                //Console.Write(j.otyParnum);Console.WriteLine(obj.Obj);
                                //代入
                                state = otyrunstate.None; break;
                            case otyParnum.leftparent:
                                //関数
                                obj = Eval(new otyObj(result[index - 1].Obj, result, index - 1));
                                index = obj.index;
                                //Console.Write(j.otyParnum);Console.WriteLine(obj.Obj);
                                state = otyrunstate.None;
                                break;
                            default: obj = Eval(new otyObj(this.Var/*iable*/[result[index - 1].Name], result, index - 1));
                                index = obj.index;
                                state = otyrunstate.None;
                                break;
                        }
                        break;
                }

                index++;
            }
            return index;
        }
        [Obsolete]
        object getObj(otyParc o)
        {
            switch (o.otyParnum)
            {
                case otyParnum.num:
                    return o.Num;
                case otyParnum.identifier:
                    return this.Var[o.Name].Obj;//return this.Variable[o.Name].Obj;
            }
            return null;
        }
    }
}
