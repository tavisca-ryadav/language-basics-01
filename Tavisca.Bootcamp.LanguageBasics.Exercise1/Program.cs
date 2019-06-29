using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
           return FixedMultiplication.FindDigit(equation);
        }
    }

    enum EquationParts{
        Operand1,
        Operand2,
        Result,
        Invalid

    }

    class FixedMultiplication
    {
        
        public static int FindDigit(string equation)
        {
           Equation ParsedEquation = ParseEquation(equation);
           return ParsedEquation.CorrectEquationAndReturnMissingDigit();
        }

         private static Equation ParseEquation(string equation)
        {
            var regex = new Regex(@"(\d*\?*\d*)\*(\d*\?*\d*)=(\d*\?*\d*)", RegexOptions.Compiled);
            var equationData = regex.Matches(equation);
            if(equationData != null && equationData[0].Success)
            {
                var groups = equationData[0].Groups;
                var operand1 = groups[1].Value;
                var operand2 = groups[2].Value;
                var result = groups[3].Value;

 

                if (string.IsNullOrEmpty(operand1) || string.IsNullOrEmpty(operand2) || string.IsNullOrEmpty(result))
                    return null;
                return new Equation(operand1, operand2, "*", result);
            }
            return null;
        }

    }


    class Equation{

        public Equation(string operand1,string operand2,string operation,string result)
        {
            Operand1 = operand1;
            Operand2 = operand2;
            Operation = operation;
            Result = result;
        }

        public string Operand1 { get; private set; }
        public string Operand2 { get; private set; }
        public string Operation { get; private set; }
        public string Result { get; private set; }

        public int CorrectEquationAndReturnMissingDigit()
        {
            EquationParts needCorrection = GetEquationPartToBeCorrected();
            string expectedValue;
            bool validValue,validEquation;
            switch(needCorrection)
            {
                case EquationParts.Operand1:
                    expectedValue = Convert.ToString( int.Parse(Result) / int.Parse(Operand2) );
                    validValue = IsValidValue(expectedValue,Operand1);
                    validEquation = IsValidEquation(expectedValue,Operand2,Result);

                    if(validValue && validEquation)
                        return GetIndex(expectedValue,Operand1);
                    else
                        return -1;

                case EquationParts.Operand2:
                    expectedValue = Convert.ToString(int.Parse(Result) / int.Parse(Operand1));
                    validValue = IsValidValue(expectedValue,Operand2);
                    validEquation = IsValidEquation(Operand1,expectedValue,Result);

                    if(validValue && validEquation)
                        return GetIndex(expectedValue,Operand2);
                    else
                        return -1;

                case EquationParts.Result:
                    expectedValue = Convert.ToString(int.Parse(Operand1) * int.Parse(Operand2));
                    validValue = IsValidValue(expectedValue,Result);
                    validEquation = IsValidEquation(Operand1,Operand2,expectedValue);

                    if(validValue && validEquation)
                        return GetIndex(expectedValue,Result);
                    else
                        return -1;

                default: 
                    return -1;

            }
        }

        private bool IsValidValue(string expectedValue,string givenValue)
        {
            return expectedValue.Length==givenValue.Length;
        }

        private bool IsValidEquation(string operand1,string operand2,string operand3)
        {
            return int.Parse(operand1)*int.Parse(operand2) == int.Parse(operand3);
            
        }

        private int GetIndex(string str1, string str2){
            int i = str2.IndexOf('?');
            return str1[i]-'0';
        }


        private EquationParts GetEquationPartToBeCorrected()
        {
            if(Operand1.Contains('?'))
                return EquationParts.Operand1;
            if(Operand2.Contains('?'))
                return EquationParts.Operand2;
            if(Result.Contains('?'))
                return EquationParts.Result;
            else
                return EquationParts.Invalid;
        }
    }
}
