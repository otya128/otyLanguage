using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{
    public class otyFuncObj
    {
        public int index = 0;
        public string type = "";
        public string name = "";
        public List<string> Param;
        public otyFuncObj(int i, string t, string n)
        {
            this.index = i;
            this.type = t;
            this.name = n;
            Param = new List<string>();
        }
        public otyFuncObj(int i, string t, string n,List<string> p)
        {
            this.index = i;
            this.type = t;
            this.name = n;
            this.Param = p;
        }
    }

    partial class otyFunc
    {
        public Dictionary<string, otyFuncObj> Function = new Dictionary<string, otyFuncObj>();
        public otyFunc(otypar op,otyRun or)
        {
            int i = 0, j = 0; string name = "",type="";
            List<string> param = new List<string>();
            while(op.result.Count>j)
            {
                var r = op.result[j];
                if (i > 0)
                {
                    if (r.otyParnum == otyParnum.comma && i == 5)
                    { i = 3; j++; continue; }
                    if (r.otyParnum == otyParnum.rightparent && i == 5)
                    {
                        var obj = new otyFuncObj(j + 1, type, name, param);
                        or.Variable.Add(name,new otyObj(obj));
                        i = 0; this.Function.Add(name, obj);
                        param = new List<string>();
                    }
                    if (r.otyParnum != otyParnum.comma && i == 5) i = 0; //errrrrror
                    if (r.otyParnum == otyParnum.identifier && i == 4)
                    {
                        i = 5;
                        param.Add(r.Name);
                    }
                    if (r.otyParnum != otyParnum.identifier && i == 4) i = 0; //errrrrror
                    if (r.otyParnum == otyParnum.identifier && i == 3) i = 4; //蟲
                    if (r.otyParnum == otyParnum.rightparent && i == 3)
                    {
                        i = 0; var obj = new otyFuncObj(j + 1, type, name, param); or.Variable.Add(name, new otyObj(obj)); this.Function.Add(name, obj);//this.Function.Add(name, new otyFuncObj(j+1, type, name,param));
                        param = new List<string>();
                    }
                    if (r.otyParnum != otyParnum.identifier && i == 3) 
                        i = 0; //errrrrror



                    if (r.otyParnum == otyParnum.leftparent && i == 2)
                    {
                        i = 3;
                        
                    }
                    if (r.otyParnum != otyParnum.leftparent && i == 2) i = 0;
                    if (r.otyParnum != otyParnum.identifier && i <= 1) i = 0;
                    if (r.otyParnum == otyParnum.identifier && i == 1) { i = 2; name = r.Name; }
                }
                else
                if (r.otyParnum == otyParnum.identifier && i == 0) { i = 1; type = r.Name; }
                j++;
            }
        }
    }
}
