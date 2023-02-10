using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppN2TVmConverterH5
{
    public class LogicalCommands
    {
        private static int falseCount_Eq = 0;
        private static int continueCount_Eq = 0;
        const string stackPointer = "@SP";
        public static bool needLable = false;
        public static string lable;

        public static List<string> Equal()
        {
            List<string> result = new List<string>();

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("D=M-D");
            result.Add("@FALSE" + falseCount_Eq);
            result.Add("D;JNE");
            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=-1");
            result.Add("@CONTINUE" + continueCount_Eq);
            result.Add("0;JMP");
            result.Add($"(FALSE{falseCount_Eq})");
            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=0");

            AddLableToNext();

            return result;
        }

        public static List<string> LessThen()
        {
            List<string> result = new List<string>();

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("D=M-D");
            result.Add("@FALSE" + falseCount_Eq);
            result.Add("D;JGE");
            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=-1");
            result.Add("@CONTINUE" + continueCount_Eq);
            result.Add("0;JMP");
            result.Add($"(FALSE{falseCount_Eq})");
            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=0");

            AddLableToNext();

            return result;
        }

        public static List<string> GreaterThan()
        {
            List<string> result = new List<string>();

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("D=M-D");
            result.Add("@FALSE" + falseCount_Eq);
            result.Add("D;JLE");
            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=-1");
            result.Add("@CONTINUE" + continueCount_Eq);
            result.Add("0;JMP");
            result.Add($"(FALSE{falseCount_Eq})");
            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=0");

            AddLableToNext();

            return result;
        }
        
        private static void AddLableToNext()
        {
            needLable = true;
            lable = "CONTINUE" + continueCount_Eq;
            falseCount_Eq++;
            continueCount_Eq++;
        }
        
        public static List<string> Neg()
        {
            List<string> result = new List<string>();
            
            result.Add("D=0");
            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=D-M");

            return result;
        }

        public static List<string> And()
        {
            List<string> result = new List<string>();

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("M=M&D");

            return result;

        }

        public static List<string> Or()
        {
            List<string> result = new List<string>();

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("M=M|D");

            return result;
        }

        public static List<string> Not()
        {
            List<string> result = new List<string>();

            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=!M");

            return result;
        }
    }
}