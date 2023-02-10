using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleAppN2TVmConverterH5
{
    public class ArithmeticCommands
    {
        private const string stackPointer = "@SP";
        private const int staticBaseLocation = 16;
        private const int tempBaseLocation = 5;
        
        public static List<string> PushConstant(string command)
        {
            List<string> result = new List<string>();
            var memLocation = Regex.Replace(command, @"[^\d]", String.Empty);

            result.Add("@" + memLocation);
            result.Add("D=A");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");
            return result;
        }

        public static List<string> PushConstantHandleLable(string command)
        {
            List<string> result = new List<string>();
            LogicalCommands.needLable = false;
            var memLocation = Regex.Replace(command, @"[^\d]", String.Empty);

            result.Add($"({LogicalCommands.lable})");
            result.Add("@" + memLocation);
            result.Add("D=A");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");
            return result;
        }

        public static List<string> Add(string command)
        {
            List<string> result = new List<string>();

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("M=M+D");

            return result;
        }
        
        public static List<string> Sub()
        {
            List<string> result = new List<string>();

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("M=M-D");

            return result;
        }

        public static List<string> PopStatic(string command)
        {
            List<string> result = new List<string>();
            int staticMemLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
            var memLoccation = staticBaseLocation + staticMemLocation;

            result.Add("@" + memLoccation);
            result.Add("D=A");
            result.Add("@R13");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("@R13");
            result.Add("A=M");
            result.Add("M=D");

            return result;
        }

        public static List<string> PushStatic(string command)
        {
            List<string> result = new List<string>();
            int staticMemLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
            var memLoccation = staticBaseLocation + staticMemLocation;

            result.Add("@" + memLoccation);
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");


            return result;
        }

        public static List<string> PopPointer(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            if (memLocation.Equals(0))
            {
                result.Add("@THIS");
            }
            else
            {
                result.Add("@THAT");
            }

            result.Add("D=A");
            result.Add("@R13");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("@R13");
            result.Add("A=M");
            result.Add("M=D");

            return result;
        }

        public static List<string> PopThis(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            result.Add("@THIS");
            result.Add("D=M");
            result.Add("@" + memLocation);
            result.Add("D=D+A");
            result.Add("@R13");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("@R13");
            result.Add("A=M");
            result.Add("M=D");

            return result;
        }

        public static List<string> PopThat(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            result.Add("@THAT");
            result.Add("D=M");
            result.Add("@" + memLocation);
            result.Add("D=D+A");
            result.Add("@R13");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("@R13");
            result.Add("A=M");
            result.Add("M=D");

            return result;
        }

        public static List<string> PushPointer(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            if (memLocation.Equals(0))
            {
                result.Add("@THIS");
            }
            else
            {
                result.Add("@THAT");
            }

            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");

            return result;
        }

        public static List<string> PushThis(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            result.Add("@THIS");
            result.Add("D=M");
            result.Add("@" + memLocation);
            result.Add("A=D+A");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");

            return result;
        }

        public static List<string> PushThat(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            result.Add("@THAT");
            result.Add("D=M");
            result.Add("@" + memLocation);
            result.Add("A=D+A");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");

            return result;
        }

        public static List<string> PopLocal(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            result.Add("@LCL");
            result.Add("D=M");
            result.Add("@" + memLocation);
            result.Add("D=D+A");
            result.Add("@R13");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("@R13");
            result.Add("A=M");
            result.Add("M=D");


            return result;
        }

        public static List<string> PopArgument(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            result.Add("@ARG");
            result.Add("D=M");
            result.Add("@" + memLocation);
            result.Add("D=D+A");
            result.Add("@R13");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("@R13");
            result.Add("A=M");
            result.Add("M=D");


            return result;
        }

        public static List<string> PopTemp(string command)
        {
            List<string> result = new List<string>();
            int TempMemLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
            var memLoccation = tempBaseLocation + TempMemLocation;

            result.Add("@R5");
            result.Add("D=M");
            result.Add("@" + memLoccation);
            result.Add("D=D+A");
            result.Add("@R13");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("@R13");
            result.Add("A=M");
            result.Add("M=D");

            return result;
        }

        public static List<string> PushLocal(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            result.Add("@LCL");
            result.Add("D=M");
            result.Add("@" + memLocation);
            result.Add("A=D+A");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");

            return result;
        }

        public static List<string> PushArgument(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

            result.Add("@ARG");
            result.Add("D=M");
            result.Add("@" + memLocation);
            result.Add("A=D+A");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");

            return result;
        }

        public static List<string> PushTemp(string command)
        {
            List<string> result = new List<string>();
            int tempMemLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
            var memLoccation = tempBaseLocation + tempMemLocation;

            result.Add("@R5");
            result.Add("D=M");
            result.Add("@" + memLoccation);
            result.Add("A=D+A");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");

            return result;
        }
    }
}