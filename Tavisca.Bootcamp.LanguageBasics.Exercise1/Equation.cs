using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Equation
    {

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
            EquationParts needsCorrection = GetEquationPartToBeCorrected();
            string expectedValue;
            switch(needsCorrection)
            {
                case EquationParts.Operand1:
                    expectedValue = Convert.ToString( Int32.Parse(Result) / Int32.Parse(Operand2) );
                    if(IsValidValue(expectedValue,Operand1) && IsValidEquation(expectedValue,Operand2,Result))
                        return GetIndex(expectedValue,Operand1);
                    return -1;

                case EquationParts.Operand2:
                    expectedValue = Convert.ToString(Int32.Parse(Result) / Int32.Parse(Operand1));
                    if(IsValidValue(expectedValue,Operand2) && IsValidEquation(Operand1,expectedValue,Result))
                        return GetIndex(expectedValue,Operand2);
                    return -1;

                case EquationParts.Result:
                    expectedValue = Convert.ToString(Int32.Parse(Operand1) * Int32.Parse(Operand2));
                    if(IsValidValue(expectedValue,Result) && IsValidEquation(Operand1,Operand2,expectedValue))
                        return GetIndex(expectedValue,Result);
                    return -1;

                default: 
                    return -1;

            }
        }

        private bool IsValidValue(string expectedValue,string givenValue)
        {
            return expectedValue.Length == givenValue.Length;
        }

        private bool IsValidEquation(string operand1,string operand2,string operand3)
        {
            return Int32.Parse(operand1)*Int32.Parse(operand2) == Int32.Parse(operand3);
            
        }

        private int GetIndex(string expected, string actual)
        {
            return Int32.Parse(expected[actual.IndexOf('?')].ToString());
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