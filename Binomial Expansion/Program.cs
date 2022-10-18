using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Binomial_Expansion
{
    public class KataSolution
    {
        public static string Expand(string expr)
        {
            string p = RemoveBracket(expr);

            if (exponentValue(p) == 0)
                return "1";
            else if (exponentValue(p) == 1)
            {
                if (SecondVal(p) < 0)
                return FirstStringVal(p) + SecondVal(p).ToString();
                else
                    return FirstStringVal(p) + Operators(p) + SecondVal(p).ToString();
            }
            else if (SecondVal(p) == 0)
            {
                BigInteger x = (BigInteger) Math.Pow(firstNum(p), exponentValue(p));
                if (x == 1)
                    return Unknown(p) + "^" + exponentValue(p).ToString();
                else
                    return x.ToString() + Unknown(p) + "^" + exponentValue(p).ToString();
            }
            else
            {
                string sr = string.Empty;
                string sr2 = string.Empty;
                string[] ar1 = FinalNum(Expansion( exponentValue(p) )   ,   BinomialEx(firstNum(p), SecondVal(p), exponentValue(p)) ).Split(' ');
                string[] ar2 = UnkownExpansion(Unknown(p), exponentValue(p)).Split(' ');

                for (int i = 0; i < ar2.Length; i++)
                {
                    string q = ar1[i] + ar2[i];

                    sr = sr + q + " ";
                }
                sr = sr + ar1[ar1.Length - 1];
                // ADDING THE OPERATOR              
                for (int i = 0; i < sr.Length; i++)
                {
                    if (sr[i] != ' ')
                        sr2 = sr2 + sr[i];
                    else
                    {
                        if(sr[i+1] != '-')
                            sr2 = sr2 + "+";
                    }
                }

                if (sr2[0] == '1' && sr2[1] == char.Parse(Unknown(p)))
                    return sr2.Substring(1);
                else if (sr2[0] == '-' && sr2[1] == '1' && sr2[2] == char.Parse(Unknown(p)))
                    return '-' + sr2.Substring(2);
                else
                    return sr2;                

            }
               
        }
        public static string BinomialEx(int firstVal, int secondV, int Ex)
        {
            string str = string.Empty;

            for (int i = Ex, j = 0; i >= 0 && j <= Ex; i--, j++)
            {
                str = str + (Math.Pow(firstVal, i) * Math.Pow(secondV, j)).ToString() + " ";
            }
            return str;
        }
        public static string FinalNum(string a, string b)
        {
            string[] arr1 = a.Split(' ');
            string[] arr2 = b.Split(' ');
            BigInteger[] ints = new BigInteger[arr1.Length];

            for (int i = 0; i < arr1.Length; i++)
            {
                ints[i] = BigInteger.Parse(arr1[i]) * BigInteger.Parse(arr2[i]);
            }
            return string.Join(" ", ints);

        }
        public static string UnkownExpansion(string u, int s)
        {
            string wh = string.Empty;

            for (int i = s; i >= 2; i--)
            {
                wh = wh + u + "^" + i.ToString() + " ";

            }
            wh = wh + u;

            return wh;
        }
        public static int position(string u, char z)
        {
            int answer = 0;
            char[] textarray = u.ToCharArray();


            for (int i = 1; i < u.Length; i++)
            {
                if (textarray[i].Equals(z))
                {
                    answer = i;
                    break;
                }
            }
            return answer;
        }
        public static int exponentValue(string u)
        {
            string s = u.Substring(position(u, '^') + 1);
            int q = int.Parse(s);
            return q;
        }
        public static string RemoveBracket(string u)
        {
            string str = string.Empty;
            for (int i = 0; i < u.Length; i++)
            {
                if (u[i] == '(' || u[i] == ')')
                    continue;
                else
                    str = str + u[i];
            }
            return str;

        }
        // SECOND VAL CAN BE NEGETIVE, FOR BINOMIAL PURPOSES.
        public static int SecondVal(string u)
        {
            if (u.Contains('+'))
            {
                int q = position(u, '+');
                string s = u.Substring(q , (position(u, '^') - q ));
                int z = int.Parse(s);
                return z;

            }          
            else
            {
                int q = position(u, '-');
                string s = u.Substring(q , position(u, '^') - q );
                int z = int.Parse(s);
                return z;
            }
        }  
        public static string Operators(string u)
        {
            if (u.Contains('+'))
                return "+";
            else
            {
                return "-";
            }
        }
        public static string FirstStringVal(string u)
        {
            string no1 = string.Empty;
            no1 = no1 + u[0];
            for (int i = 1; i < u.Length; i++)
            {
                if (u[i] == '*' || u[i] == '+' || u[i] == '-')
                    break;
                else
                    no1 = no1 + u[i];
            }
            return no1;
        }
        public static int firstNum(string u)
        {
            string no1 = string.Empty;
            no1 = no1 + u[0];
            for (int i = 1; i < u.Length; i++)
            {
                if (u[i] == '*' || u[i] == '+' || u[i] == '-')
                    break;
                else
                    no1 = no1 + u[i];
            }
            if (FirstStringVal(u).Length == 1)
            {
                return 1;
            }
            else if (FirstStringVal(u).Length == 2 && u[0] == '-')
            {
                return -1;
            }
            else
            {
                int s = int.Parse(no1.Substring(0, no1.Length - 1));
                return s;
            }
        }
        public static string Unknown(string u)
        {
            return FirstStringVal(u)[FirstStringVal(u).Length-1].ToString();
        }
        public static string Expansion (int s)
        {
            const string OG = "1 2 1";
            if (s == 2)
                return OG;
            else if (s == 1 || s == 0)
                return "1";           
            else
            {
                string s2 = "1 ";
                string[] ch = (Expansion(s-1).Split(' '));
                BigInteger[] num = new BigInteger[ch.Length];

                // CONVERTING THE STRING TO int
                for (int i = 0; i < ch.Length; i++)
                {
                    num[i] = (BigInteger.Parse(ch[i]));
                }

                // THE NEXT BINOMIAL EXPANSION, STRING FORMAT
                for (int i = 0; i < num.Length - 1; i++)
                {
                    s2 = s2 + ((num[i] + num[i + 1]).ToString()) + " ";
                }

                s2 = s2 + "1";

                // RECURING FUNTION CHECK
                string[] no2 = (s2.Split(' '));

                if (no2.Length == s + 1)
                    return s2;
                else
                    return Expansion(s);             
            }
        }
       

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(KataSolution.Expand("(x+1)^2"));      // returns "x^2+2x+1"
            Console.WriteLine(KataSolution.Expand("(p-1)^3"));      // returns "p^3-3p^2+3p-1"
            Console.WriteLine(KataSolution.Expand("(2f+4)^6"));     // returns "64f^6+768f^5+3840f^4+10240f^3+15360f^2+12288f+4096"
            Console.WriteLine(KataSolution.Expand("(-2a-4)^0"));    // returns "1"
            Console.WriteLine(KataSolution.Expand("(-12t+43)^2"));  // returns "144t^2-1032t+1849"
            Console.WriteLine(KataSolution.Expand("(r+0)^203"));    // returns "r^203"
            Console.WriteLine(KataSolution.Expand("(-x-1)^2"));     // returns "x^2+2x+1"




        }
    }
}
