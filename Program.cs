using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class 引きニートになってふともものきれいな女の子とイチャイチャして幸せに暮らせるようになりたいけどなれない
{
    public 引きニートになってふともものきれいな女の子とイチャイチャして幸せに暮らせるようになりたいけどなれない()
    {

    }
    public 引きニートになってふともものきれいな女の子とイチャイチャして幸せに暮らせるようになりたいけどなれない(引きニートになってふともものきれいな女の子とイチャイチャして幸せに暮らせるようになりたいけどなれない 引きニートになってふともものきれいな女の子とイチャイチャして幸せに暮らせるようになりたいけどなれない)
    {

    }
}
class 麿 : 引きニートになってふともものきれいな女の子とイチャイチャして幸せに暮らせるようになりたいけどなれない
{
}
namespace otypar
{

    class Program
    {
        //state????????????????
        //status!!!!!!!!!!!!!!!!

        static void Main(string[] args)
        {
            object hoge = new 麿();
            hoge = (引きニートになってふともものきれいな女の子とイチャイチャして幸せに暮らせるようになりたいけどなれない)hoge;
            new 引きニートになってふともものきれいな女の子とイチャイチャして幸せに暮らせるようになりたいけどなれない(new 引きニートになってふともものきれいな女の子とイチャイチャして幸せに暮らせるようになりたいけどなれない());
            //for(if(true);;);
            //for(Console.WriteLine("hello" + "world" + 1+("helllo"));true;Console.WriteLine("hello" + "world" + 1+("helllo")));
            for (int i = 0; i < 10; i++) Console.WriteLine("hello" + "world" + 1 + ("helllo{0}"),i%2);
            //int testt = (int)"";
            //Console.WriteLine(testt as int?);
            var op = new otypar();
            string prg =
@"pint i=0;int k=0;for(i=0;i<10;i++){
k=0;
for(int j=1;j<=i;j++){if(i%j==0){k++;}}
if(k==2){print(i);}
}";
          //prg = @"print(1.ToString());";
            //"var k=0;print(k<10);var test=\"scopetest0\";for(var i=0;i<10;i++){var test=\"scopetest1\";print(test);print(i);}print(test);";// "var test=\"helllovar\";print(\"hello\"+\"world\"+(\"ahelllo\"+test));print(tostr(1));print(2*2+1,2*(2+1),(2*2)+1);";//"print(add(add(1,2),2));";//print(2*2+1,2*(2+1),1+i=1,add(add(1,2),2));i=i+1;i=i+1;print(i);;;;";
            op.Parse(prg); int j=1;
            // Console.WriteLine(1 + j = 1);
            Console.WriteLine(prg);
            foreach (var i in op.result)
            {
                Console.WriteLine("{0}\t{1}", i.otyParnum, i.Name);
            }
            var or = new otyRun(op);
            try
            {
                or.Run();
            }
            catch (EntryPointNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (var i in or.Variable)
            {
               // Console.WriteLine("{0}\t{1}", i.Key,i.Value.Obj);
            }
            Console.ReadLine();
        }
    }
}
