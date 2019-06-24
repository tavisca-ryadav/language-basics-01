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

            var tokens = new List<String>();
            tokens = equation.Split('=','*').ToList();

            if(tokens[0]==null || tokens[1]==null || tokens[2]==null)
                return -1;

            var A = tokens[0];
            var B = tokens[1];
            var C = tokens[2];


            // First check if C contains ? character 

            if(C.Contains("?")){
                var operand1 = Convert.ToInt32(A);
                var operand2 = Convert.ToInt32(B);
               
               
                return FindIndex(Convert.ToString(operand1 * operand2),C);
            }

           

            else{
                 // if B contains '?' then swap B with A to use same code for different condition

                if(B.Contains("?"))
                    Swap(ref A,ref B);
                    
                var operand3 = Convert.ToInt32(C);
                var operand2 = Convert.ToInt32(B);

                if(operand3 % operand2 !=0 || operand2 == 0)
                    return -1;
                
                 
                return FindIndex(Convert.ToString(operand3 / operand2) , A);
            }
        }

         // FindDigit method finds the missing digit by compairing calculated string with given string
        private static int FindIndex(string str1,string str2){
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

        private static void Swap(ref string A,ref string B){
            var temp = A;
            A = B;
            B=temp;
        }
    }
}
