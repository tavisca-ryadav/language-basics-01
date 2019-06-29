using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
class FixedMultiplication
    {
        
        public static int FindDigit(string equation)
        {
           Equation parsedEquation = ParseEquation(equation);
           return parsedEquation.CorrectEquationAndReturnMissingDigit();
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
                if (string.IsNullOrEmpty(operand1)==false && string.IsNullOrEmpty(operand2)==false && string.IsNullOrEmpty(result)==false)
                    return new Equation(operand1, operand2, "*", result);
                return null;
            }
            return null;
        }

    }
}