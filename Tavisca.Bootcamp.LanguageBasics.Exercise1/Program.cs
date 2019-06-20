using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("42*47=1?74", 9);
            Test("4?*47=1974", 2);
            Test("42*?7=1974", 4);
            Test("42*?47=1974", -1);
            Test("2*12?=247", -1);
            Console.ReadKey(true);
        }

        private static void Test(string args, int expected)
        {
            var result = FindDigit(args).Equals(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"{args} : {result}");
        }

        public static int FindDigit(string equation)
        {
            /* Splitting the equation into three parts A,B,C */

            string[] token = equation.Split("=");
            string[] equ = token[0].Split("*");
            string a = equ[0],b=equ[1],c=token[1];
            int an,bn,cn;

            /*First checking if C contains ? character */

            if(c.Contains("?")){
                an = Convert.ToInt32(a);
                bn = Convert.ToInt32(b);
                string cnew= Convert.ToString(an*bn);

                /* findingDigit method will compare two strings  for different characters or length */
                return findingDigit(cnew,c);
            }

            /*Checking for A only if it contains '?' if B contains then swap B with A */

            else{
                if(b.Contains("?")){
                    string t= a;
                    a=b;
                    b=t;
                }
                cn = Convert.ToInt32(c);
                bn = Convert.ToInt32(b);
                if(cn%bn!=0 || bn==0)
                    return -1;
                string anew= Convert.ToString(cn/bn);
                
                 /* findingDigit method will compare two strings  for different characters or length */
                return findingDigit(anew,a);
            }
            throw new NotImplementedException();
        }

        public static int findingDigit(string st,string c){
            int ans = -1;
            if(st.Length!=c.Length)
                return ans;
            for(int i=0;i<c.Length;i++){
                if(st[i]!=c[i]){
                    if(c[i].Equals('?')){
                        ans = st[i]-'0';
                    }
                    break;
                }
            }
            return ans;
        }
    }
}