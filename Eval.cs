using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    public partial class otyRun
    {
        const int EqualPrece = 16;
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
                default:
                    return 0;
            }
            return 0;
        }
        otyObj Eval(otyObj oo, int opera = 17,bool scoped=false)//opera16
        {
            bool isVar = false;//testcode
            var index = oo.index;
            var r = oo.result;
            var data = oo;
            var k = r[index];
            bool func = false;
            if (index + 1 <= r.Count)
            {
            start:
                switch (k.otyParnum)
                {
                    case otyParnum.leftparent:
                        bool test = false;
                        if (data.result[index - 1].otyParnum == otyParnum.identifier||func)
                        {
                        
                            otyObj df = otyObj.NULL;
                          //  if (scoped) 
                                df = data;
                            List<otyObj> param = new List<otyObj>(); //index++;
                            int parent = 0;
                            int ii = index;
                            while (true)
                            {
                                var jj = data.result[ii];
                                if (jj.otyParnum == otyParnum.leftparent)
                                {
                                    parent++;
                                }
                                if (jj.otyParnum == otyParnum.rightparent)
                                {

                                    parent--;
                                    if (parent == 0) 
                                        break;
                                }
                                ii++;
                            }
                            ii++;
                            //関数!!
                            while (true)
                            {
                                index++;
                                
                                if (index >= ii - 1)
                                {
                                    index = ii-1;//+1;
                                    break;
                                    if (r[index].otyParnum == otyParnum.rightparent) { index++; break; } if (r[index + 1].otyParnum == otyParnum.rightparent) { index++; break; }
                                    index++;
                                    //
                                } 
                                if (r[index].otyParnum == otyParnum.comma) { 
                                    continue;
                                }
                                if (r[index].otyParnum == otyParnum.rightparent)
                                {
                                    continue;
                                }
                                /*if (r[index].otyParnum == otyParnum.rightparent)
                                {
                                    index = ii + 1;
                                    break;
                                }*///{
                                //    continue;//
                                //    //index++;
                                //    //break;
                                //}
                                otyObj obj;
                                
                                    obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index));
                                index = obj.index;

                                //if (!obj.isNull())
                                {
                                    data = new otyObj(obj.Obj, data.result, index);
                                    param.Add(data);
                                }

                                //if (r[index].otyParnum == otyParnum.rightparent) { index++; break; }
                                    
                                
                                //if (r[index + 1].otyParnum == otyParnum.rightparent) {  index++; break; }
                                
                                //index++;
                                //if (r[index].otyParnum == otyParnum.rightparent && r[index + 1].otyParnum != otyParnum.comma) { break; }

                                //index++;
                                /*if (index + 1 <= r.Count)
                                {
                                    if (r[index + 1].otyParnum == otyParnum.comma || r[index].otyParnum == otyParnum.comma)
                                        index++;
                                    else if (r[index].otyParnum == otyParnum.rightparent) break; 
                                }
                                else
                                {
                                    if (r[index].otyParnum == otyParnum.comma)
                                        index++;
                                    else if (r[index].otyParnum == otyParnum.rightparent) break;
                                } */




                            }
                            otyObj result;
                            if (!scoped)
                            {

                                if (this.Var.ContainsKey(oo.result[oo.index].Name)||df.Type == otyType.Function)
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
                                //index++;
                                result = df.Func(oo.result[oo.index/*-1*/].Name,param);
                                //index++;
                                //data.index++;
                            }/*if (!result.isNull())
                            {
                                result.index = data.index;
                                result.result = data.result;
                                data = result;
                            }*/
                            // return data;
                            //if (!result.isNull())
                            {
                                result.index = data.index;
                                result.result = data.result;

                            } data = result;// return data;

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
                            var iii = this.ParentSkip(index,-1)+1;
                            while (true)//r[index + 1].otyParnum != otyParnum.rightparent)
                            {
                                index++;
                                if (index >= iii - 2)
                                {
                                    index = iii - 2;//多分これで+1;
                                    break;
                                }
                                var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index));
                                index = obj.index;
                                data = new otyObj(obj.Obj, data.result, index);
                            }//if (r[index + 1].otyParnum != otyParnum.rightparent) goto case otyParnum.leftparent;

                            data.index++;
                            index++;
                            if (index + 1 <= r.Count)
                            {
                                //  return data;sd
                            }
                        }
                        break;
                    case otyParnum.identifier:

                        if (index + 1 <= r.Count)

                            if (r[index + 1].otyParnum == otyParnum.leftparent)
                            {
                                //data.index++;
                                test = true;
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
                                //this.Var[r[index].Name].result = r;
                                //this.Var[r[index].Name].index = index;
                                data = new otyObj(getObj(k), r, oo.index);//this.Var[r[index].Name];
                                
                            }
                        /*else if (index + 2 <= r.Count)

                            if (r[index + 1].otyParnum == otyParnum.plus && r[index + 1].otyParnum == otyParnum.plus)
                            {
                                //data.index++;
                                test = true;
                                index++;
                                goto case otyParnum.leftparent;
                            }*/


                        try
                        {
                            //new otyObj(getObj(k), r, oo.index);
                        }
                        catch
                        {
                        }
                        break;
                }
            if (data.Type == otyType.Function)
            {
                if (index + 1 <= r.Count)

                    if (r[index + 1].otyParnum == otyParnum.leftparent)
                    {
                        //data.index++;
                        index++;
                        func = true; k = r[index];
                        goto start;
                    }
                
            }
            start2:
                index++;
                var j = r[index];
                switch (k.otyParnum)
                {
                    case otyParnum.and:
                        unsafe
                        {
                            //data.index++;
                            data = Eval(new otyObj(j.Obj, r, index), IncrementPrece);//new otyObj(getObj(j), r, index);
                            index = data.index;
                            switch (data.Type)
                            {
                                case otyType.Pointer:
                                    data = new otyObj(data.Ptr.Address, r, index);
                                    index++;
                                    j = r[index];
                                    break;
                                case otyType.Int32:
                                    int p1 = (int)data.Num;
                                    //int p3 = *p2;
                                    data = new otyObj((int)&p1, r, index);
                                    index++;
                                    j = r[index];
                                    // goto start2;
                                    break;
                                case otyType.Double:
                                    double p1d = (double)data.Num;
                                    data = new otyObj((int)&p1d, r, index);
                                    index++;
                                    j = r[index];
                                    break;
                                case otyType.String:
                                    fixed (char* p2s = data.Str)
                                    {
                                        data = new otyObj((int)p2s, r, index);
                                        index++;
                                        j = r[index];
                                    }
                                    break;
                                default:
                                    throw new ArgumentException("oty型'" + data.Type + "'のポインタを取得できませんでした!?");
                            }
                        }
                        break;
                    case otyParnum.multiply:
                        unsafe
                        {
                            //data.index++;
                            data = Eval(new otyObj(j.Obj, r, index), IncrementPrece);//new otyObj(getObj(j), r, index);
                            index = data.index;
                            switch (data.Type)
                            {
                                case otyType.Int32:
                                    data = new otyObj((void*)(int)data.Num, r, index);
                                    index++;
                                    j = r[index];
                                    break;
                                    /*int p1 = (int)data.Num;
                                    int* pp = (int*)p1;
                                    int* p2 = &p1;    
                                //int p3 = *p1;
                                    data = new otyObj(*pp, r, index);
                                    index++;
                                    j = r[index];
                                    // goto start2;
                                    break;*/
                                case otyType.Pointer:
                                    //int p3 = *p1;
                                    data = new otyObj(data.Ptr, r, index);
                                    index++;
                                    j = r[index];
                                    // goto start2;
                                    break;
                                default:
                                    throw new ArgumentException("*演算子をてきようできませｎ"+data.Type);
                            }
                        }
                        break;
                }
                switch (j.otyParnum)
                {
                    case otyParnum.plusequal:
                        if (opera < PlusEqualPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), PlusEqualPrece);
                        index = obj.index;
                        data.Add(obj);
                        data.index = obj.index;
                        if (k.otyParnum != otyParnum.identifier) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierである必要がありまあす。");
                       
                        if (!isVar) this.Var[k.Name] = data;/* else
                            data.Increment();*/
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.minusequal:
                        if (opera < PlusEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), PlusEqualPrece);
                        index = obj.index;
                        data.Sub(obj);
                        data.index = obj.index;
                        if (k.otyParnum != otyParnum.identifier) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierである必要がありまあす。");
                        this.Var[k.Name] = data;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.divisionequal:
                        if (opera < PlusEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), PlusEqualPrece);
                        index = obj.index;
                        data.Division(obj);
                        data.index = obj.index;
                        if (k.otyParnum != otyParnum.identifier) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierである必要がありまあす。");
                        this.Var[k.Name] = data;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.multiplyequal:
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.moduloequal:
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.equal:
                        if (opera < EqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index));
                        index = obj.index;
                        //obj.result[index].Nu
                        
                        if (k.otyParnum != otyParnum.identifier&&data.Type!=otyType.Pointer) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierかPointerである必要がありまあす。");
                        if (!isVar)
                        {
                            if (data.Type == otyType.Pointer)
                                unsafe
                                {
                                    var ptr = data.Ptr.Pointer;
                                    //var hoge = *ptr;
                                    *((int*)ptr) = (int)obj.Num;
                                }
                            else
                            {
                                data = new otyObj(obj.Obj, data.result, index);
                                this.Var/*iable*/[k.Name] = data;
                            }
                        }
                        else
                        {
                            if (data.Type == otyType.Pointer)
                                unsafe
                                {
                                    var ptr = data.Ptr.Pointer;
                                    //var hoge = *ptr;
                                    *((int*)ptr) = (int)obj.Num;
                                }
                            else
                            {
                                data.index = index;
                                data.Obj = obj.Obj;
                            }
                        }
                            index++; j = r[index]; if (Operator(j.otyParnum) >= EqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                }
                switch (j.otyParnum)
                {
                    case otyParnum.equalequal:
                        if (opera < EqualEqualPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), EqualEqualPrece);
                        index = obj.index;
                        data = data.Equal(obj); data.result = r;
                        data.index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= EqualEqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                }
                switch (j.otyParnum)
                {
                    case otyParnum.less:
                        if (opera < LessPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), LessPrece);
                        index = obj.index;
                        data = data.Less(obj); data.result = r;
                        data.index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= LessPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.greater:
                        if (opera < GreaterPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), GreaterPrece);
                        index = obj.index;
                        data = data.Greater(obj); data.result = r;
                        data.index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= ModuloPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.lessequal:
                        if (opera < LessEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), LessPrece);
                        index = obj.index;
                        data = data.LessEqual(obj); data.result = r;
                        data.index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= LessPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.greaterequal:
                        if (opera < GreaterEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), GreaterPrece);
                        index = obj.index;
                        data = data.GreaterEqual(obj); data.result = r;
                        data.index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= ModuloPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;

                }
                switch (j.otyParnum)
                {
                    case otyParnum.leftshift:
                        if (opera < PlusPrece) break;
                        index++;
                        var obj = Eval(new otyObj(data.result[index].Obj, data.result, index), PlusPrece);
                        index = obj.index;
                        data.LeftShift(obj);
                        data.index = index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.rightshift:
                        if (opera < MinusPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), MinusPrece);
                        index = obj.index;
                        data.RightShift(obj);
                        data.index = index;
                        index++; j = r[index]; if (Operator(j.otyParnum) >= MinusPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                }
                switch (j.otyParnum)
                {
                    case otyParnum.plus:
                        if (opera < PlusPrece) break;
                        index++;
                        var obj = Eval(new otyObj(data.result[index].Obj, data.result, index), PlusPrece);
                        index = obj.index;//obj.result[index].Nu//.result[index].Num
                        data=otyOpera.Add(data,obj);data.result=r;//data.Add(obj);
                        data.index = index;//data = new otyObj(/*r[index].Num*/(int)data.Obj + ((int)obj.Obj), data.result, index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= PlusPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.minus:
                        if (opera < MinusPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), MinusPrece);
                        index = obj.index;//obj.result[index].Nu//.result[index].Num
                        data.Sub(obj);
                        data.index = index;//data = new otyObj(/*r[index].Num*/(int)data.Obj + ((int)obj.Obj), data.result, index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= MinusPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;


                }
                switch (j.otyParnum)
                {
                    case otyParnum.multiply:
                    if (opera < ModuloPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), ModuloPrece);
                        index = obj.index;
                        data.index = index;
                        data.Multply(obj);
                        index++; j = r[index];
                        if (Operator(j.otyParnum) >= ModuloPrece) { index--; opera = 17;
                        k = r[index-1]; goto start;
                        }    //if (opera < MultiplyPrece) break;
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
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), ModuloPrece);
                        index = obj.index;
                        data.index = index;
                        data.Modulo(obj);
                        index++; j = r[index];
                        if (Operator(j.otyParnum) >= ModuloPrece) { index--; opera = 17;
                        k = r[index-1]; goto start;
                        }
                        break;
                    case otyParnum.division:
                        if (opera < DivisionPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), DivisionPrece);
                        index = obj.index;
                        data.index = index;
                        data.Division(obj);
                        index++; j = r[index];
                        if (Operator(j.otyParnum) >= DivisionPrece)
                        {
                            index--; opera = 17;
                            k = r[index - 1]; goto start;
                        }
                        break;
                }
                
                switch (j.otyParnum)
                {
                    case otyParnum.leftbracket:
                        if (opera < ArrayPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), ArrayPrece);
                        //index = obj.index;
                        //index++;
                        data = data.Indexer((int)obj.Num);//.Array[(int)obj.Num];
                        data.result = r;
                        isVar = true;
                        index = this.ArraySkip(index, 0);
                        data.index = index;
                        // if (k.otyParnum != otyParnum.identifier) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierである必要がありまあす。");
                        // this.Var[k.Name].Array[(int)obj.Num] = data;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index];
                        if (Operator(j.otyParnum) >= ArrayPrece) { index--; opera = 17;/* k = r[index - 1];*/ goto start2; }
                        break;
                    case otyParnum.plusplus:
                        if (opera < IncrementPrece) break;
                        if (k.otyParnum != otyParnum.identifier) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierである必要がありまあす。");
                        if (!isVar) this.Var/*iable*/[k.Name].Increment(); else data.Increment();

                        index++;
                        index++; j = r[index];
                        if (Operator(j.otyParnum) >= IncrementPrece) { index--; opera = 17;/* k = r[index - 1];*/ goto start2; }
                        break;
                    case otyParnum.minusminus:
                        if (opera < IncrementPrece) break;
                        if (k.otyParnum != otyParnum.identifier) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierである必要がありまあす。");
                        if (!isVar) this.Var/*iable*/[k.Name].Decrement(); else data.Decrement();
                        index++; j = r[index];
                        if (Operator(j.otyParnum) >= IncrementPrece) { index--; opera = 17;/* k = r[index - 1];*/ goto start2; }
                        index++;
                        break;
                }
                switch (j.otyParnum)
                {
                    case otyParnum.dot:
                        //最後だからいらないif (opera < PlusEqualPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data/*.result[index]*/.Obj), data.result, index), PlusEqualPrece,true);
                        index = obj.index;
                        data = obj;
                        //data.Add(obj);
                        data.index = obj.index+1;
                        
                        //最後だからいらないindex++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    
                }

            }


            return data;

        }
    }
}
