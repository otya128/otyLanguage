using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    public class Int
    {
        public Int(int i)
        {
            this.Item = i;
        }
        public int Item;
    }
    public partial class otyRun
    {
        const int EqualPrece = 16;
        const int AndPrece = 10;
        const int LogicAndPrece = 13;
        const int XorPrece = 11;
        const int OrPrece = 12;
        const int LogicOrPrece = 14;
        const int PlusPrece = 6;
        const int MinusPrece = 6;
        const int GreaterPrece = 8;
        const int GreaterEqualPrece = 8;
        const int LessPrece = 8;
        const int LessEqualPrece = 8;
        const int EqualEqualPrece = 9;
        const int MultiplyPrece = 5;
        const int ModuloPrece = 5;
        const int DivisionPrece = 5;
        const int PlusEqualPrece = 16;
        const int DotEqualPrece = 1;
        const int ArrayPrece = 2;//2;
        const int IncrementPrece = 2;
        const int PointerPrece = 3;
        public static int Operator(otyParnum op)
        {
            switch (op)
            {
                case otyParnum.dot:
                    return 1;
                case otyParnum.leftbracket:
                    return ArrayPrece;
                case otyParnum.plusplus:
                case otyParnum.minusminus:
                    return IncrementPrece;
                case otyParnum.modulo:
                case otyParnum.multiply:
                case otyParnum.division:
                    return 5;
                case otyParnum.minus:
                case otyParnum.plus:
                    return 6;
                case otyParnum.leftshift:
                case otyParnum.rightshift:
                    return 7;
                case otyParnum.greater:
                case otyParnum.greaterequal:
                case otyParnum.less:
                case otyParnum.lessequal:
                    return 8;
                case otyParnum.equalequal:
                case otyParnum.notequal:
                    return 9;
                case otyParnum.and:
                    return 10;
                case otyParnum.xor:
                    return 11;
                case otyParnum.or:
                    return 12;
                case otyParnum.andand:
                    return 13;
                case otyParnum.oror:
                    return 14;
                case otyParnum.equal://=    ＼
                case otyParnum.plusequal://+= ＼
                case otyParnum.minusequal://-   ＼
                case otyParnum.divisionequal:// /=＼
                case otyParnum.leftshiftequal://<<= ＼
                case otyParnum.rightshiftequal://>>= /
                case otyParnum.multiplyequal: //*=  /
                case otyParnum.moduloequal:  //%=  /
                case otyParnum.andequal:    //&=  /
                case otyParnum.xorequal:   //^=  /
                case otyParnum.orequal:   //|=  /
                    return 16;
                case otyParnum.semicolon:
                    return 0;
                case otyParnum.identifier:
                    return 0;
                default:
                    return 0;
            }
        }
        unsafe otyObj Eval(otyObj oo, int opera = 17, bool scoped = false, Int varvar = null,bool notvar=false)//opera16
        {
            bool isVar = false;//testcode
            var index = oo.index;
            var r = oo.result;
            var data = oo;
            var k = r[index];
            bool func = false;//, setPointerAddress = true;
            if (index + 1 <= r.Count)
            {
            start:
                switch (k.otyParnum)
                {
                    case otyParnum.leftparent:
                        if (data.result[index - 1].otyParnum == otyParnum.identifier || func)
                        {
                            otyObj df = otyObj.NULL;//  if (scoped) 
                            df = data; List<otyObj> param = new List<otyObj>(); //index++;
                            int parent = 0;
                            int ii = index, iii = 0;//void関数ようにindexを保持
                            ii = this.ParentSkip(index,-1);
                            if (index + 1 == ii)
                            {//引数なし
                                iii = index;
                                data.index = oo.index;
                            }
                            else
                            {
                                ii++;
                                //関数!!
                                while (true)
                                {
                                    index++;
                                    if (index >= ii - 1)
                                    {
                                        index = ii - 1;//+1;
                                        break;
                                    }
                                    if (r[index].otyParnum == otyParnum.comma) continue;
                                    if (r[index].otyParnum == otyParnum.rightparent) continue;
                                    otyObj obj;
                                    obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index));
                                    index = obj.index;
                                    data = new otyObj(obj.Obj, data.result, index);
                                    param.Add(data);
                                }
                            }
                            otyObj result;
                            if (!scoped)
                            {
                                if (this.Var.ContainsKey(oo.result[oo.index].Name) || df.Type == otyType.Function)
                                {
                                    otyObj funcobj;
                                    if (df.Type == otyType.Function)
                                        funcobj = df;
                                    else
                                        funcobj = this.Var[oo.result[oo.index].Name];
                                    var scope = new otyRun(new otypar
                                    {
                                        result = this.result//result = this.result.GetRange(i + 1, this.result.Count - i - 1)
                                    }, funcobj.Function.index + 1, this);
                                    int jk = 0;
                                    foreach (var ik in param)
                                    {
                                        scope.Variable.Add(funcobj.Function.Param[jk], ik);
                                        jk++;
                                    }
                                    scope.FuncFlg = true;
                                    result = scope.Run();
                                }
                                else
                                    result = this.DefFunc.RunFunc(oo.result[oo.index].Name, param, this);//result = this.DefFunc.RunFunc(oo.result[oo.index/*-1*/].Name, param,this);
                            }
                            else
                            {
                                result = df.Func(oo.result[oo.index/*-1*/].Name, param);
                            }
                            if (iii + 1 == ii)
                            {//引数なし
                                data.index = ii; index = ii;
                            }
                            data.index = index;
                            result.index = data.index;
                            result.result = data.result;
                            data = result;// return data;
                            if (index + 2 <= r.Count) ;//{ data.index++; index++; }
                            else
                            {
                                return data;
                            }
                        }
                        else
                        {
                            if (index + 2 <= r.Count)
                            {
                                if (r[index + 1].otyParnum == otyParnum.identifier)
                                {
                                    if (r[index + 2].otyParnum == otyParnum.rightparent)
                                    {
                                        string type = r[index + 1].Name;
                                        index += 3;
                                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index));
                                        index = obj.index;
                                        data = new otyObj(obj.Obj, data.result, index);
                                        data.Cast(type);
                                        break;
                                    }
                                    else if (r[index + 2].otyParnum == otyParnum.multiply)
                                    {
                                        if (index + 3 <= r.Count)
                                        {
                                            if (r[index + 3].otyParnum == otyParnum.rightparent)
                                            {
                                                string type = r[index + 1].Name;
                                                index += 4;
                                                var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index));
                                                index = obj.index;
                                                data = new otyObj(obj.Obj, data.result, index);
                                                data = data.PtrCast(type); data.result = r; data.index = index;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            var iii = this.ParentSkip(oo.index, -1) + 0;//-1????
                           // if (notvar) iii = this.ParentSkip(index, 0) + 1;
                            while (true)//r[index + 1].otyParnum != otyParnum.rightparent)
                            {
                                index++;
                                var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index));
                                index = obj.index;
                                data = new otyObj(obj.Obj, data.result, index);
                                if (index >= iii - 2 - 1)
                                {
                                    index = iii - 2 - 0;//多分これで+1;
                                    break;
                                }
                            }//if (r[index + 1].otyParnum != otyParnum.rightparent) goto case otyParnum.leftparent;
                            data.index++;
                            index++;
                            if (index + 1 <= r.Count)
                            {//  return data;sd
                            }
                        }
                        break;
                    case otyParnum.identifier:
                        if (index + 1 <= r.Count)
                            if (r[index + 1].otyParnum == otyParnum.leftparent)
                            {
                                index++;
                                goto case otyParnum.leftparent;
                            }
                            else
                            {
                                if (scoped)
                                {
                                    data = data.GetMember(k.Name);
                                    data.result = r; data.index = index;
                                }
                                else
                                {
                                    if (!notvar)
                                    {
                                        data = this.Var[k.Name];
                                        data.result = r;
                                        data.index = oo.index;
                                        //data = new otyObj(getObj(k), r, oo.index);//this.Var[r[index].Name];
                                    }
                                }
                            }
                        break;
                    case otyParnum.and:
                        if (opera < PointerPrece) break;
                        unsafe
                        {
                            data = Eval(new otyObj(r[index + 1].Obj, r, index + 1), PointerPrece);//new otyObj(getObj(j), r, index);
                            index = data.index;
                            switch (data.Type)
                            {
                                case otyType.Pointer:
                                    data = new otyObj(data.Ptr.Address, r, index);
                                    break;
                                case otyType.Int32:
                                    int p1 = (int)data.Num;
                                    data = new otyObj((int)&p1, r, index);
                                    break;
                                case otyType.Double:
                                    double p1d = (double)data.Num;
                                    data = new otyObj((int)&p1d, r, index);
                                    break;
                                case otyType.String:
                                    fixed (char* p2s = data.Str)
                                    {
                                        data = new otyObj((int)p2s, r, index);
                                    }
                                    break;
                                default:
                                    throw new ArgumentException("oty型'" + data.Type + "'のポインタを取得できませんでした!?");
                            }
                        }
                        break;
                    case otyParnum.multiply:
                        if (opera < PointerPrece) break;
                        unsafe
                        {
                            data = Eval(new otyObj(r[index + 1].Obj, r, index + 1), PointerPrece);//new otyObj(getObj(j), r, index);
                            index = data.index;
                            switch (data.Type)
                            {
                                case otyType.Int32:
                                    data = new otyObj((void*)(int)data.Num, r, index);
                                    break;
                                case otyType.Pointer:
                                    data = new otyObj(data.Ptr, r, index);
                                    break;
                                default:
                                    throw new ArgumentException("*演算子を適用できません。" + data.Type);
                            }
                        }
                        break;
                    case otyParnum.minus:
                        if (opera < PointerPrece) break;
                        data = Eval(new otyObj(r[index + 1].Obj, r, index + 1), PointerPrece);//new otyObj(getObj(j), r, index);
                        index = data.index;
                        data = otyOpera.UnaryMinus(data);
                        data.index = index;
                        data.result = r;
                        break;
                    case otyParnum.plus:
                        if (opera < PointerPrece) break;
                        data = Eval(new otyObj(r[index + 1].Obj, r, index + 1), PointerPrece);//new otyObj(getObj(j), r, index);
                        index = data.index;
                        data = otyOpera.UnaryPlus(data);
                        data.index = index;
                        data.result = r;
                        break;
                    case otyParnum.not:
                        if (opera < PointerPrece) break;
                        data = Eval(new otyObj(r[index + 1].Obj, r, index + 1), PointerPrece);//new otyObj(getObj(j), r, index);
                        index = data.index;
                        data = otyOpera.Not(data);
                        data.index = index;
                        data.result = r;
                        break;
                    case otyParnum.notnot:
                        if (opera < PointerPrece) break;
                        data = Eval(new otyObj(r[index + 1].Obj, r, index + 1), PointerPrece);//new otyObj(getObj(j), r, index);
                        index = data.index;
                        data = otyOpera.LogicNot(data);
                        data.index = index;
                        data.result = r;
                        break;
                    case otyParnum.num:
                    case otyParnum.chr:
                    case otyParnum.str:
                        //許す
                        break;
                    default:
                        throw new FormatException("認識できないトークン'" + k.Name + "'" + k.otyParnum + "これは演算子、変数、リテラルである必要があります。" + getSource(index));
                }
                if (data.Type == otyType.Function)
                {
                    if (index + 1 <= r.Count)
                        if (r[index + 1].otyParnum == otyParnum.leftparent)
                        {
                            index++;
                            func = true; k = r[index];
                            goto start;
                        }
                }
            start2:
                index++;
            otyParc j;
            try
            {
                j = r[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new FormatException("文法エラー'" + k.Name + "'" + k.otyParnum + "。" + getSource(index));
            }
                switch (j.otyParnum)
                {
                    case otyParnum.plusequal:
                        if (opera < PlusEqualPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), PlusEqualPrece);
                        index = obj.index;
                        data.index = obj.index;
                        if (k.otyParnum != otyParnum.identifier && data.Type != otyType.Pointer) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierかPointerである必要がありまあす。");
                        if (data.Type == otyType.Pointer)
                            unsafe
                            {
                                var ptr = data.Ptr.Pointer;
                                //var hoge = *ptr;
                                *((int*)ptr) = (int)otyOpera.Add(new otyObj(*(int*)ptr), obj).Num;
                            }
                        else
                        {
                            data.Add(obj);
                            if (!isVar) this.Var[k.Name] = data;
                        }
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.minusequal:
                        if (opera < PlusEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), PlusEqualPrece);
                        index = obj.index;
                        data.Sub(obj);
                        data.index = obj.index;
                        if (k.otyParnum != otyParnum.identifier && data.Type != otyType.Pointer) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierかPointerである必要がありまあす。");
                        this.Var[k.Name] = data;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece){ k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.divisionequal:
                        if (opera < PlusEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), PlusEqualPrece);
                        index = obj.index;
                        data.Division(obj);
                        data.index = obj.index;
                        if (k.otyParnum != otyParnum.identifier && data.Type != otyType.Pointer) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierかPointerである必要がありまあす。");
                        this.Var[k.Name] = data;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece){ k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.multiplyequal:
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.moduloequal:
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece){ k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.equal:
                        if (opera < EqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index));
                        index = obj.index;
                        if (k.otyParnum != otyParnum.identifier && data.Type != otyType.Pointer) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierかPointerである必要がありまあす。");
                        if (!isVar)
                        {
                            if (data.Type == otyType.Pointer)
                                unsafe
                                {
                                    otyOpera.PtrSetObj(data, obj);
                                }
                            else
                            {
                                data = new otyObj(obj.Obj, data.result, index);
                                this.Var[k.Name] = data;
                            }
                        }
                        else
                        {
                            if (data.Type == otyType.Pointer)
                                unsafe
                                {
                                    otyOpera.PtrSetObj(data, obj);
                                }
                            else
                            {
                                data.index = index;
                                data.Obj = obj.Obj;
                            }
                        }
                        index++; data.index = index; j = r[index]; if (Operator(j.otyParnum) >= EqualPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    //}
                    //switch (j.otyParnum)
                    //{
                    case otyParnum.equalequal:
                        if (opera < EqualEqualPrece) break;
                        index++;
                        /*var*/
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), EqualEqualPrece);
                        index = obj.index;
                        data = otyOpera.Equal(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= EqualEqualPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.notequal:
                        if (opera < EqualEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), EqualEqualPrece);
                        index = obj.index;
                        data = otyOpera.NotEqual(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= EqualEqualPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    //}
                    //switch (j.otyParnum)
                    //{
                    case otyParnum.less:
                        if (opera < LessPrece) break;
                        index++;
                        /*var*/
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), LessPrece);
                        index = obj.index;
                        data = otyOpera.Less(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= LessPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.greater:
                        if (opera < GreaterPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), GreaterPrece);
                        index = obj.index;
                        data = otyOpera.Greater(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= GreaterPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.lessequal:
                        if (opera < LessEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), LessEqualPrece);
                        index = obj.index;
                        data = otyOpera.LessEqual(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= LessEqualPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.greaterequal:
                        if (opera < GreaterEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), GreaterEqualPrece);
                        index = obj.index;
                        data = otyOpera.GreaterEqual(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= GreaterEqualPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.and:
                        if (opera < AndPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), AndPrece);
                        index = obj.index;
                        data = otyOpera.And(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= AndPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.andand:
                        if (opera < LogicAndPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), LogicAndPrece);
                        index = obj.index;
                        data = otyOpera.LogicAnd(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= LogicAndPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.or:
                        if (opera < OrPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), OrPrece);
                        index = obj.index;
                        data = otyOpera.Or(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= OrPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.oror:
                        if (opera < LogicOrPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), LogicOrPrece);
                        index = obj.index;
                        data = otyOpera.LogicOr(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= LogicOrPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.xor:
                        if (opera < XorPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), XorPrece);
                        index = obj.index;
                        data = otyOpera.Xor(data, obj); data.result = r;
                        data.index = obj.index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= XorPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    //}
                    //switch (j.otyParnum)
                    //{
                    case otyParnum.leftshift:
                        if (opera < PlusPrece) break;
                        index++;
                        /*var*/
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), PlusPrece);
                        index = obj.index;
                        data = otyOpera.LeftShift(data, obj); data.result = r;
                        data.index = index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.rightshift:
                        if (opera < MinusPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), MinusPrece);
                        index = obj.index;
                        data = otyOpera.RightShift(data, obj); data.result = r;
                        data.index = index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= MinusPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    //}
                    //switch (j.otyParnum)
                    //{
                    case otyParnum.plus:
                        if (opera < PlusPrece) break;
                        index++;
                        /*var*/
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), PlusPrece);
                        index = obj.index;
                        data = otyOpera.Add(data, obj); data.result = r;//data.Add(obj);
                        data.index = index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    case otyParnum.minus:
                        if (opera < MinusPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), MinusPrece);
                        index = obj.index;
                        data = otyOpera.Sub(data, obj); data.result = r;
                        data.index = index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= MinusPrece) { k = r[index + 1];index--; data.index = index; opera = 17; goto start; }
                        break;
                    //}
                    //switch (j.otyParnum)
                    //{
                    case otyParnum.multiply:
                        if (opera < ModuloPrece) break;
                        index++;
                        /*var*/
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), ModuloPrece);
                        index = obj.index;
                        
                        data.index = index;
                        data = otyOpera.Multply(data, obj); data.result = r; data.index = index;
                        index++; j = r[index];
                        if (Operator(j.otyParnum) >= ModuloPrece)
                        {
                            data = Eval(data, 17,false,null,true);
                            index = obj.index;
                        }
                        if (Operator(j.otyParnum) >= ModuloPrece){ data = Eval(data, 17, false, null, true); index = obj.index; }//
                        //if (Operator(j.otyParnum) >= ModuloPrece)
                        //{
                        //    //data.index--;
                        //    data = Eval(data, 17);
                        //    index = data.index;
                        //    /*k = r[index + 1];
                        //    index--; opera = 17;
                        //    goto start;*/
                        //    //--してるからindex減らさなくてよかった!!!!!!
                        //}    //if (opera < MultiplyPrece) break;
                        //index++;
                        //var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), MultiplyPrece);
                        //index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        //index++; j = r[index];if (Operator(j.otyParnum) >= ModuloPrece) {index--; opera = 17;k = r[index-1]; goto start; }


                        //if (Operator(r[index + 1].otyParnum) > Operator(otyParnum.multiply))//演算順位が低い場合
                        //{
                        //    data = new otyObj((int)data.Obj * ((int)/*getObj*/(data.result[index].Num)), data.result, index);
                        //    index++; j = r[index];
                        //}
                        //else
                        //{
                        //    var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index));
                        //    index = obj.index;
                        //    data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        //}
                        break;
                    case otyParnum.modulo:
                        if (opera < ModuloPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), ModuloPrece);
                        index = obj.index;
                        data = otyOpera.Modulo(data, obj); data.result = r; data.index = index;
                        index++; j = r[index];
                        if (Operator(j.otyParnum) >= ModuloPrece){ data = Eval(data, 17, false, null, true); index = obj.index; }//
                        //{
                        //    k = r[index + 1];
                        //    index--; opera = 17;
                        //    goto start;
                        //}
                        break;
                    case otyParnum.division:
                        if (opera < DivisionPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), DivisionPrece);
                        index = obj.index;
                        data = otyOpera.Division(data, obj); data.result = r; data.index = index;
                        //data.Division(obj);
                        index++; j = r[index];
                        if (Operator(j.otyParnum) >= DivisionPrece){ data = Eval(data, 17, false, null, true); index = obj.index; }//
                        //{
                        //    k = r[index + 1];
                        //    index--; opera = 17;
                        //    goto start;
                        //}
                        break;
                    //}
                    //switch (j.otyParnum)
                    //{
                    case otyParnum.leftbracket:
                        if (opera < ArrayPrece) break;
                        index++;
                        /*var*/
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), ArrayPrece);
                        data = data.Indexer((int)obj.Num);//.Array[(int)obj.Num];
                        data.result = r;
                        isVar = true;
                        index = this.ArraySkip(index, 0);
                        
                        index++;
                        j = r[index];data.index = index;
                        if (Operator(j.otyParnum) >= ArrayPrece) { index--; opera = 17;/* k = r[index - 1];*/ goto start2; }
                        break;
                    case otyParnum.plusplus:
                        if (opera < IncrementPrece) break;
                        if (k.otyParnum != otyParnum.identifier) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierである必要がありまあす。");
                        if (!isVar) this.Var[k.Name].Increment(); else data.Increment();
                        //index++;
                        index++; j = r[index]; data.index = index;
                        if (Operator(j.otyParnum) >= IncrementPrece) { index--; opera = 17;/* k = r[index - 1];*/ goto start2; }
                        break;
                    case otyParnum.minusminus:
                        if (opera < IncrementPrece) break;
                        if (k.otyParnum != otyParnum.identifier) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierである必要がありまあす。");
                        if (!isVar) this.Var[k.Name].Decrement(); else data.Decrement();
                        index++; j = r[index]; data.index = index;
                        if (Operator(j.otyParnum) >= IncrementPrece) { index--; opera = 17;/* k = r[index - 1];*/ goto start2; }
                        //index++;
                        break;
                    //}
                    //switch (j.otyParnum)
                    //{
                    case otyParnum.dot:
                        //最後だからいらないif (opera < PlusEqualPrece) break;
                        index++;
                        /*var*/
                        obj = Eval(new otyObj(/*getObj*/(data/*.result[index]*/.Obj), data.result, index), PlusEqualPrece, true);
                        index = obj.index + 1;
                        data = obj;
                        data.index = obj.index + 1;
                        //最後だからいらないindex++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.rightparent:
                    case otyParnum.rightbracket:
                    case otyParnum.semicolon:
                    case otyParnum.comma:
                        //許す
                        break;
                    default:
                        throw new FormatException("認識できない演算子'" + j.Name + "'" + j.otyParnum + "これは演算子である必要があります。" + getSource(index));
                }
            }
            return data;
        }
        string getSource(int index)
        {
            string ret = "";
            int startindex = 0, endindex = 0;
            for (int i = index; i >= 0 && i < result.Count; i--)
            {
                if (i < 0) break;
                var j = this.result[i];
                if (j.otyParnum == otyParnum.semicolon || j.otyParnum == otyParnum.blockstart || j.otyParnum == otyParnum.blockend)
                {
                    startindex = i + 1;
                    break;
                }
            }
            for (int i = index; i >= 0 && i < result.Count; i++)
            {
                var j = this.result[i];
                if (j.otyParnum == otyParnum.semicolon || j.otyParnum == otyParnum.blockstart || j.otyParnum == otyParnum.blockend)
                {
                    endindex = i;
                    break;
                }
            }
            for (int i = startindex; i <= endindex; i++)
            {
                var j = this.result[i];
                ret += j.Name + " ";
            }
            return ret;
        }
    }
}
