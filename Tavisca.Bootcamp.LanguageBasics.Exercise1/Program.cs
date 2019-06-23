using System;
using System.Collections.Generic;
using System.Linq;
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
            // Splitting the equation into three parts A,B,C

            List<string> tokens = new List<string>();
            tokens = equation.Split('=','*').ToList();
            var A = tokens[0];
            var B = tokens[1];
            var C = tokens[2];

            //null check on tokens
            if(A==null || B==null || C==null)
                return -1;

            // First checking if C contains ? character 

            if(C.Contains("?")){
                var numeric_A = Convert.ToInt32(A);
                var numeric_B = Convert.ToInt32(B);
               
                // findingDigit method will compare two strings  for different characters or length */
                return FindDigit(Convert.ToString(numeric_A * numeric_B),C);
            }

            // Checking for A only if it contains '?' if B contains then swap B with A */

            else{
                if(B.Contains("?"))
                    Swap(ref A,ref B);
                    
                var numeric_C = Convert.ToInt32(C);
                var numeric_B = Convert.ToInt32(B);

                if(numeric_C % numeric_B !=0 || numeric_B == 0)
                    return -1;
                
                 // findingDigit method will compare two strings  for different characters or length 
                return FindDigit(Convert.ToString(numeric_C / numeric_B) , A);
            }
        }

        public static int FindDigit(string str1,string str2){
            var answer = -1;
            if(str1.Length!=str2.Length)
                return answer;
            for(var i=0;i<str1.Length;i++){
                if(str1[i]!=str2[i]){
                    if(str2[i].Equals('?')){
                        answer = str1[i]-'0';
                    }
                    break;
                }
            }
            return answer;
        }

        public static void Swap(ref string A,ref string B){
            var temp = A;
            A = B;
            B=temp;
        }
    }
}