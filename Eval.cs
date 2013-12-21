using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    public partial class otyRun
    {
        const int PlusPrece = 5;
        const int MinusPrece = 5;
        const int GreaterPrece = 7;
        const int GreaterEqualPrece = 7;
        const int LessPrece = 7;
        const int LessEqualPrece = 7;
        const int EqualEqualPrece = 8;
        const int MultiplyPrece = 4;
        const int ModuloPrece = 4;
        const int DivisionPrece = 4;
        const int PlusEqualPrece = 15;
        const int DotEqualPrece = 1;
        const int ArrayPrece = 2;//2;
        public static int Operator(otyParnum op)
        {
            switch (op)
            {
                case otyParnum.dot:
                    return 1;
                case otyParnum.leftbracket:
                    return ArrayPrece;
                case otyParnum.modulo:
                case otyParnum.multiply:
                case otyParnum.division:
                    return 4;
                case otyParnum.minus:
                case otyParnum.plus:

                    return 5;
                case otyParnum.greater:
                case otyParnum.greaterequal:
                case otyParnum.less:
                case otyParnum.lessequal:
                    return 7;
                case otyParnum.equalequal:
                    return 8;
                case otyParnum.equal:
                case otyParnum.plusequal:
                case otyParnum.minusequal:
                    return 15;
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

            if (index + 1 <= r.Count)
            {
            start:
                switch (k.otyParnum)
                {
                    case otyParnum.leftparent: bool test = false;
                        if (data.result[index - 1].otyParnum == otyParnum.identifier)
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
                                //this.Var[r[index].Name].result = r;
                                //this.Var[r[index].Name].index = index;
                                data = new otyObj(getObj(k), r, oo.index);//this.Var[r[index].Name];
                                if (r[index + 1].otyParnum == otyParnum.plusplus)
                                {
                                    this.Var/*iable*/[r[index].Name].Increment();
                                    index++;
                                }else
                                if (r[index + 1].otyParnum == otyParnum.minusminus)
                                {
                                    this.Var/*iable*/[r[index].Name].Decrement();
                                    index++;
                                }
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
            start2:
                index++;
                var j = r[index];

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
                        this.Var[k.Name] = data;
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
                    case otyParnum.equal:
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index));
                        index = obj.index;
                        //obj.result[index].Nu
                        
                        if (k.otyParnum != otyParnum.identifier) throw new FormatException(k.otyParnum + "に代入できません。これはidentifierである必要がありまあす。");
                        if (!isVar)
                        {
                            data = new otyObj(obj.Obj, data.result, index);
                            this.Var/*iable*/[k.Name] = data;
                        }
                        else
                        {
                            data.index = index;
                            data.Obj = obj.Obj;
                        }
                            index++; j = r[index]; if (Operator(j.otyParnum) >= PlusEqualPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                }
                switch (j.otyParnum)
                {
                    case otyParnum.equalequal:
                        if (opera < EqualEqualPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), EqualEqualPrece);
                        index = obj.index;
                        data.Equal(obj);
                        data.index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index];if (Operator(j.otyParnum) >= EqualEqualPrece) {index--; opera = 17;k = r[index-1]; goto start; }
                        break;
                }
                switch (j.otyParnum)
                {
                    case otyParnum.less:
                        if (opera < LessPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), LessPrece);
                        index = obj.index;
                        data.Less(obj);
                        data.index = obj.index;    
                    //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index];if (Operator(j.otyParnum) >= LessPrece) {index--; opera = 17;k = r[index-1]; goto start; }
                        break;
                    case otyParnum.greater:
                        if (opera < GreaterPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), GreaterPrece);
                        index = obj.index;
                        data.Greater(obj);
                        data.index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index];if (Operator(j.otyParnum) >= ModuloPrece) { index--;opera = 17;k = r[index-1]; goto start; }
                        break;
                    case otyParnum.lessequal:
                        if (opera < LessEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), LessPrece);
                        index = obj.index;
                        data.LessEqual(obj);
                        data.index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= LessPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;
                    case otyParnum.greaterequal:
                        if (opera < GreaterEqualPrece) break;
                        index++;
                        obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), GreaterPrece);
                        index = obj.index;
                        data.GreaterEqual(obj);
                        data.index = obj.index;
                        //data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index]; if (Operator(j.otyParnum) >= ModuloPrece) { index--; opera = 17; k = r[index - 1]; goto start; }
                        break;

                }
                switch (j.otyParnum)
                {
                    case otyParnum.multiply:
                        if (opera < MultiplyPrece) break;
                        index++;
                        var obj = Eval(new otyObj(/*getObj*/(data.result[index].Obj), data.result, index), MultiplyPrece);
                        index = obj.index;
                        data = new otyObj((int)data.Obj * ((int)obj.Obj), data.result, obj.index);
                        index++; j = r[index];if (Operator(j.otyParnum) >= ModuloPrece) {index--; opera = 17;k = r[index-1]; goto start; }
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
                    case otyParnum.plus:
                        if (opera < PlusPrece) break;
                        index++;
                        var obj = Eval(new otyObj(data.result[index].Obj, data.result, index), PlusPrece);
                        index = obj.index;//obj.result[index].Nu//.result[index].Num
                        data.Add(obj);
                        data.index = index;//data = new otyObj(/*r[index].Num*/(int)data.Obj + ((int)obj.Obj), data.result, index);
                        index++; j = r[index];if (Operator(j.otyParnum) >= PlusPrece) {index--; opera = 17;k = r[index-1]; goto start; }
                        break;
                    case otyParnum.minus:
                        if (opera < MinusPrece) break;
                        index++;
                        obj = Eval(new otyObj(data.result[index].Obj, data.result, index), MinusPrece);
                        index = obj.index;//obj.result[index].Nu//.result[index].Num
                        data.Sub(obj);
                        data.index = index;//data = new otyObj(/*r[index].Num*/(int)data.Obj + ((int)obj.Obj), data.result, index);
                        index++; j = r[index];if (Operator(j.otyParnum) >= MinusPrece) {index--; opera = 17;k = r[index-1]; goto start; }
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
                        data = data.Array[(int)obj.Num];
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
