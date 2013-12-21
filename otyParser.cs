using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otypar
{

    public enum otyParnum
    {
        identifier,
        num,
        doublequote, str,
        leftparent,//(
        rightparent,//)
        comma,//,
        plus,//+
        minus,//-
        multiply,//*
        equal,//=
        equalequal,//==
        semicolon,//;
        blockstart,//{
        blockend,//}
        plusplus,//++
        minusminus,//--
        greater,//>
        less,//<
        greaterequal,//>=
        lessequal,//<=
        modulo,//%
        plusequal,//+=
        minusequal,//-=
        dot,//.
        division,///
        leftbracket,//[
        rightbracket,//]
        debbug_stop,//$
        //追加演算子
        multiplyequal,//*=
        divisionequal,// /=
        moduloequal,//%=
        or,//|
        oror,//||
        and,//&
        andand,//&&
        xor,//^
        notequal,//!
        not,//~
        notnot,//!LogicalNot
        leftshift,//<<
        rightshift,//>>
        leftshiftequal,//<<=
        rightshiftequal,//>>=
        andequal,//&=
        orequal,//|=
        xorequal,//^=
    }
    public class otyParc
    {
        public otyParc()
        {
        }
        public otyParc(otyParnum o, string name)
        {
            this.otyParnum = o;
            this.Name = name;
            if (o == otyParnum.num)
                this.Num = int.Parse(name);
            if (o == otyParnum.str)
                this.Str = name;
        }
        public otyParnum otyParnum;
        public string Name;
        public int? Num
        {
            get
            {
                return this.Obj as Int32?;
            }
            set
            {
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
                this.Obj = value;
            }
        }
        public object Obj;
    }
    public class otypar
    {
        private enum otyparstate
        {
            None, IdenRead, NumRead, StrRead, DoubleRead,
            EscapeRead,LineCommentRead,CommentRead,
        }
        public List<otyParc> result = new List<otyParc>();
        
        public void Parse(string p)
        {
            var state = otyparstate.None;
            string iden = "";
            int i = 0;
            while (i < p.Length)
            {
                bool last = i + 1 < p.Length;
                char j = p[i];
                switch (state)
                {
                    case otyparstate.IdenRead:
                        if (IsAlphaOrNum(j))
                        {
                            iden += j;
                        }
                        else
                        {
                            result.Add(new otyParc(otyParnum.identifier, iden));
                            iden = "";
                            state = otyparstate.None;
                            continue;
                        }
                        break;
                    case otyparstate.NumRead:
                        if (isNum(j))
                        {
                            iden += j;
                        }
                        else
                        {
                            if (j == '.')
                            {
                                iden += j;
                                state = otyparstate.DoubleRead;
                                break;
                            }
                            result.Add(new otyParc(otyParnum.num, iden));
                            iden = "";
                            state = otyparstate.None;
                            continue;
                        }
                        break;
                    case otyparstate.DoubleRead:
                        if (isNum(j))
                        {
                            iden += j;
                        }
                        else
                        {
                            if (iden[iden.Length - 1] == '.')
                            {
                                //最後だとドット演算子
                                
                                result.Add(new otyParc(otyParnum.num, iden.Substring(0, iden.Length - 1))); result.Add(new otyParc(otyParnum.dot, "."));
                                iden = "";
                                state = otyparstate.None;
                                continue;
                            }
                            result.Add(new otyParc { otyParnum = otyParnum.num, Double = Convert.ToDouble(iden) });
                            iden = "";
                            state = otyparstate.None;
                            continue;
                        }
                        break;
                    case otyparstate.StrRead:
                        if (j == '"')
                        {
                            result.Add(new otyParc(otyParnum.str, iden));
                            iden = "";
                            state = otyparstate.None;
                        }
                        else
                        {
                            if (j == '\\') { state = otyparstate.EscapeRead; break; }
                            iden += j;
                        }
                        break;
                    case otyparstate.EscapeRead:
                        switch (j)
                        {
                            case 'n':
                                iden += "\n";
                                break;
                            case 'r':
                                iden += "\r";
                                break;
                            case 'a':
                                iden += "\a";
                                break;
                            case 't':
                                iden += "\t";
                                break;
                            case '\"':
                                iden += "\"";
                                break;
                            case '\'':
                                iden += "\'";
                                break;
                            case '\\':
                                iden += "\\";
                                break;
                            case '0':
                                iden += "\0";
                                break;
                            case 'b':
                                iden += "\b";
                                break;
                            case 'f':
                                iden += "\f";
                                break;
                            default:
                                throw new FormatException("認識できないエスケープシーケンス'\\"+j+"'");
                        }
                        state=otyparstate.StrRead;
                        break;
                    case otyparstate.LineCommentRead:
                        if (j == '\n') state = otyparstate.None;
                        break;
                    case otyparstate.None:
                        if (IsAlpha(j))
                        {
                            state = otyparstate.IdenRead;
                            continue;
                        }
                        else
                            if (isNum(j))
                            {
                                state = otyparstate.NumRead;
                                continue;
                            }
                            else
                            {
                                switch (j)
                                {
                                    case '(':
                                        result.Add(new otyParc(otyParnum.leftparent, "("));
                                        break;
                                    case ')':
                                        result.Add(new otyParc(otyParnum.rightparent, ")"));
                                        break;
                                    case ',':
                                        result.Add(new otyParc(otyParnum.comma, ","));
                                        break;
                                    case '"':
                                        state = otyparstate.StrRead;
                                        break;
                                    case '+':
                                        if (i + 1 < p.Length)
                                            if (p[i + 1] == '+')
                                            {
                                                result.Add(new otyParc(otyParnum.plusplus, "++"));
                                                i++;
                                                break;
                                            }
                                            else if (p[i + 1] == '=')
                                            {
                                                result.Add(new otyParc(otyParnum.plusequal, "+="));
                                                i++;
                                                break;
                                            }
                                        result.Add(new otyParc(otyParnum.plus, "+"));

                                        break;
                                    case '-':
                                        if (i + 1 < p.Length)
                                            if (p[i + 1] == '-')
                                            {
                                                result.Add(new otyParc(otyParnum.minusminus, "--"));
                                                i++;
                                                break;
                                            }
                                            else if (p[i + 1] == '=')
                                            {
                                                result.Add(new otyParc(otyParnum.minusequal, "-="));
                                                i++;
                                                break;
                                            }
                                        result.Add(new otyParc(otyParnum.minus, "-"));

                                        break;
                                    case '*':
                                        result.Add(new otyParc(otyParnum.multiply, "*"));
                                        break;
                                    case '=':
                                        if (i + 1 < p.Length)
                                            if (p[i + 1] == '=')
                                            {
                                                result.Add(new otyParc(otyParnum.equalequal, "=="));
                                                i++;
                                                break;
                                            }
                                        result.Add(new otyParc(otyParnum.equal, "="));
                                        break;
                                    case ';':
                                        result.Add(new otyParc(otyParnum.semicolon, ";"));
                                        break;
                                    case '{':
                                        result.Add(new otyParc(otyParnum.blockstart, "{"));
                                        break;
                                    case '}':
                                        result.Add(new otyParc(otyParnum.blockend, "}"));
                                        break;
                                    case '<':
                                        if (i + 1 < p.Length)
                                            if (p[i + 1] == '=')
                                            {
                                                result.Add(new otyParc(otyParnum.lessequal, "<="));
                                                i++;
                                                break;
                                            }
                                        result.Add(new otyParc(otyParnum.less, "<"));
                                        break;
                                    case '>':
                                        if (i + 1 < p.Length)
                                            if (p[i + 1] == '=')
                                            {
                                                result.Add(new otyParc(otyParnum.greaterequal, ">="));
                                                i++;
                                                break;
                                            }
                                        result.Add(new otyParc(otyParnum.greater, ">"));
                                        break;
                                    case '%':
                                        result.Add(new otyParc(otyParnum.modulo, "%"));
                                        break;
                                    case '.':
                                        result.Add(new otyParc(otyParnum.dot, "."));
                                        break;
                                    case '/':
                                        if (i + 1 < p.Length)
                                            if (p[i + 1] == '/') { i++; state = otyparstate.LineCommentRead; break; }
                                        result.Add(new otyParc(otyParnum.division, "/"));
                                        break;
                                    case '[':
                                        result.Add(new otyParc(otyParnum.leftbracket, "["));
                                        break;
                                    case ']':
                                        result.Add(new otyParc(otyParnum.rightbracket, "]"));
                                        break;
                                    case '$':
                                        result.Add(new otyParc(otyParnum.debbug_stop, "$"));
                                        break;
                                    case '\r':
                                    case '\n':
                                    case ' ':
                                    case '　':
                                    case '\a':
                                    case '\f':
                                    case '\t':
                                        break;
                                    default:
                                        throw new FormatException("認識できない文字'" + j + "'");
                                }
                            }
                        break;
                }
                i++;

            }
            switch (state)
            {
                case otyparstate.IdenRead:
                    result.Add(new otyParc(otyParnum.identifier, iden));
                    iden = "";
                    state = otyparstate.None;

                    break;
                case otyparstate.NumRead:
                    result.Add(new otyParc(otyParnum.num, iden));
                    iden = "";
                    state = otyparstate.None;
                    break;
            }
        }
        bool isIden(char p)
        {
            return IsAlphaOrNum(p) || p == '_';
        }
        bool IsAlphaOrNum(char p)
        {
            return isalpha(p) || isAlpha(p) || isNum(p);
        }
        bool isNum(char p)
        {
            return p >= '0' && p <= '9';
        }
        bool isalpha(char p)
        {
            return p >= 'a' && p <= 'z';
        }
        bool isAlpha(char p)
        {
            return p >= 'A' && p <= 'Z';
        }
        bool IsAlpha(char p)
        {
            return isalpha(p) || isAlpha(p);
        }
    }
}
