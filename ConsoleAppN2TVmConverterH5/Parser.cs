using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleAppN2TVmConverterH5
{
    public class Parser
    {
        public void ParseVmFile()
        {
            //var fileContent = GetFileOpg7();
            var fileContent = GetFileOpg8();
            var cleanFile = Utilities.CleanUpFile(fileContent);

            var asmFile = LoopthroughSwitch(cleanFile);
            Utilities.WriteToConsole(asmFile);
            Writefile(asmFile);

        }

        private List<string> LoopthroughSwitch(List<string> file)
        {
            List<string> output = new List<string>();

            foreach (var line in file)
            {
                if (line.Contains("push"))
                {
                    output.AddRange(PushSwitch(line));
                }
                else if (line.Contains("pop"))
                {
                    output.AddRange(PopSwitch(line));
                }
                else if (line.Contains("add") && !line.Contains("."))
                {
                    output.AddRange(ArithmeticCommands.Add(line));
                }
                else if (line.Contains("eq"))
                {
                    output.AddRange(LogicalCommands.Equal());
                }
                else if (line.Contains("lt"))
                {
                    output.AddRange(LogicalCommands.LessThen());
                }
                else if (line.Contains("gt"))
                {
                    output.AddRange(LogicalCommands.GreaterThan());
                }
                else if (line.Contains("sub"))
                {
                    output.AddRange(ArithmeticCommands.Sub());
                }
                else if (line.Contains("neg"))
                {
                    output.AddRange(LogicalCommands.Neg());
                }
                else if (line.Contains("and"))
                {
                    output.AddRange(LogicalCommands.And());
                }
                else if (line.Contains("or"))
                {
                    output.AddRange(LogicalCommands.Or());
                }
                else if (line.Contains("not"))
                {
                    output.AddRange(LogicalCommands.Not());
                }
                else if (line.Contains("if-goto"))
                {
                    output.AddRange(IfGoto(line));
                }
                else if (line.Contains("label"))
                {
                    output.Add(SetLabel(line));
                }
                else if (line.Contains("goto"))
                {
                    output.AddRange(Goto(line));
                }
                else if (line.Contains("return"))
                {
                    output.AddRange(Return());
                }
                else if (line.Contains("function"))
                {
                    output.AddRange(Function(line));
                }
                else if (line.Contains("call"))
                {
                    output.AddRange(Call(line));
                }
                else
                {
                    output.Add(line);
                }
            }

            return output;
        }

        private List<string> PushSwitch(string line)
        {
            if (line.Contains("push") && line.Contains("constant"))
            {
                if (LogicalCommands.needLable)
                {
                    return ArithmeticCommands.PushConstantHandleLable(line);
                }
                else
                {
                    return ArithmeticCommands.PushConstant(line);
                }
            }
            else if (line.Contains("push") && line.Contains("static"))
            {
                return ArithmeticCommands.PushStatic(line);
            }
            else if (line.Contains("push") && line.Contains("pointer"))
            {
                return ArithmeticCommands.PushPointer(line);
            }
            else if (line.Contains("push") && line.Contains("static"))
            {
                return ArithmeticCommands.PushStatic(line);
            }
            else if (line.Contains("push") && line.Contains("this"))
            {
                return ArithmeticCommands.PushThis(line);
            }
            else if (line.Contains("push") && line.Contains("that"))
            {
                return ArithmeticCommands.PushThat(line);
            }
            else if (line.Contains("push") && line.Contains("local"))
            {
                return ArithmeticCommands.PushLocal(line);
            }
            else if (line.Contains("push") && line.Contains("argument"))
            {
                //if (LogicalCommands.needLable)
                //{
                //    return PushArgumentLabelHandel(line);
                //}
                //else
                //{
                    return ArithmeticCommands.PushArgument(line);

                //}
                
            }
            else if (line.Contains("push") && line.Contains("temp"))
            {
                return ArithmeticCommands.PushTemp(line);
            }
            else
            {
                return null;
            }
        }

        private List<string> PopSwitch(string line)
        {
            if (line.Contains("pop") && line.Contains("static"))
            {
                return ArithmeticCommands.PopStatic(line);
            }
            else if (line.Contains("pop") && line.Contains("pointer"))
            {
                return ArithmeticCommands.PopPointer(line);
            }
            else if (line.Contains("pop") && line.Contains("this"))
            {
                return ArithmeticCommands.PopThis(line);
            }
            else if (line.Contains("pop") && line.Contains("that"))
            {
                return ArithmeticCommands.PopThat(line);
            }
            else if (line.Contains("pop") && line.Contains("local"))
            {
                return ArithmeticCommands.PopLocal(line);
            }
            else if (line.Contains("pop") && line.Contains("argument"))
            {
                return ArithmeticCommands.PopArgument(line);
            }
            else if (line.Contains("pop") && line.Contains("temp"))
            {
                return ArithmeticCommands.PopTemp(line);
            }
            else
            {
                return null;
            }
        }


        private string[] GetFileOpg7()
        {
            const string basePath = @"C:\Users\uncha\Desktop\nand2tetris\projects\07\";
            const string folderPathToSA = basePath + @"StackArithmetic\";
            const string folderPathToMA = basePath + @"MemoryAccess\";

            FileHandler fileHandler = new FileHandler();
            //var fileContent = fileHandler.ReadVmFile(folderPathToSA + @"\SimpleAdd\SimpleAdd.vm");
            //var fileContent = fileHandler.ReadVmFile(folderPathToSA + @"\StackTest\StackTest.vm");
            //var fileContent = fileHandler.ReadVmFile(folderPathToMA + @"\StaticTest\StaticTest.vm");
            //var fileContent = fileHandler.ReadVmFile(folderPathToMA + @"\PointerTest\PointerTest.vm");
            var fileContent = fileHandler.ReadVmFile(folderPathToMA + @"\BasicTest\BasicTest.vm");

            return fileContent;
        }

        private string[] GetFileOpg8()
        {
            const string basePath = @"C:\Users\uncha\Desktop\nand2tetris\projects\08\";
            const string folderPathToFC = basePath + @"FunctionCalls\";
            const string folderPathToPF = basePath + @"ProgramFlow\";

            FileHandler fileHandler = new FileHandler();
            //var fileContent = fileHandler.ReadVmFile(folderPathToPF + @"\BasicLoop\BasicLoop.vm");
            //var fileContent = fileHandler.ReadVmFile(folderPathToPF + @"\FibonacciSeries\FibonacciSeries.vm");
            //var fileContent = fileHandler.ReadVmFile(folderPathToFC + @"\SimpleFunction\SimpleFunction.vm");
            var fileContent = fileHandler.ReadVmFile(folderPathToFC + @"NestedCall\Sys.vm");

            return fileContent;
        }

        private void Writefile(List<string> fileContent)
        {
            const string basePath = @"C:\Users\uncha\Desktop\";

            FileHandler fileHandler = new FileHandler();
            fileHandler.WriteAsmFile(fileContent, basePath + @"\MyNestedcall.asm");
        }




        private List<string> IfGoto(string line)
        {
            List<string> result = new List<string>();
            const string stakpointer = "@SP";

            var startIndex = line.Replace("goto", "goto ").IndexOf(" ");
            var whereToGoto = line.Substring(startIndex);

            result.Add(stakpointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("@" + whereToGoto);
            result.Add("D;JNE");

            //LogicalCommands.needLable = true;
            //LogicalCommands.lable = $"({whereToGoto})";
            return result;
        }

        public static List<string> PushArgumentLabelHandel(string command)
        {
            List<string> result = new List<string>();
            int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
            const string stakpointer = "@SP";

            LogicalCommands.needLable = false;

            result.Add(LogicalCommands.lable);
            result.Add("@ARG");
            result.Add("D=M");
            result.Add("@" + memLocation);
            result.Add("A=D+A");
            result.Add("D=M");
            result.Add(stakpointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stakpointer);
            result.Add("M=M+1");

            return result;
        }

        public string SetLabel(string line)
        {
            var startIndex = line.Replace("label", "label ").IndexOf(" ");
            var lable = $"({line.Substring(startIndex)})";
            return lable;
        }

        public List<string> Goto(string line)
        {
            List<string> result = new List<string>();
            //const string stakpointer = "@SP";

            var startIndex = line.Replace("goto", "goto ").IndexOf(" ");
            var whereToGoto = line.Substring(startIndex);

            result.Add("@" + whereToGoto);
            result.Add("0;JMP");

            //LogicalCommands.needLable = true;
            //LogicalCommands.lable = $"({whereToGoto})";
            return result;
        }

        public List<string> Return()
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

            result.Add("@LCL");
            result.Add("D=M");
            result.Add("@R11");
            result.Add("M=D");
            result.Add("@5");
            result.Add("A=D-A");
            result.Add("D=M");
            result.Add("@R12");
            result.Add("M=D");
            result.Add("@ARG");
            result.Add("D=M");
            result.Add("@0");
            result.Add("D=D+A");
            result.Add("@R13");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("@R13");
            result.Add("A=M");
            result.Add("M=D");
            result.Add("@ARG");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("M=D+1");
            result.Add("@R11");
            result.Add("D=M-1");
            result.Add("AM=D");
            result.Add("D=M");
            result.Add("@THAT");
            result.Add("M=D");
            result.Add("@R11");
            result.Add("D=M-1");
            result.Add("AM=D");
            result.Add("D=M");
            result.Add("@THIS");
            result.Add("M=D");
            result.Add("@R11");
            result.Add("D=M-1");
            result.Add("AM=D");
            result.Add("D=M");
            result.Add("@ARG");
            result.Add("M=D");
            result.Add("@R11");
            result.Add("D=M-1");
            result.Add("AM=D");
            result.Add("D=M");
            result.Add("@LCL");
            result.Add("M=D");
            result.Add("@R12");
            result.Add("A=M");
            result.Add("0;JMP");

            return result;
        }

        public List<string> Function(string line)
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";
            //var parmCount = Convert.ToInt32(Regex.Replace(line, @"[^\d]", String.Empty));
            var allNums = Convert.ToInt32(Regex.Replace(line, @"[^\d]", String.Empty));
            var parmCount = (allNums.ToString().Length > 1) ? allNums.ToString().Substring(2) : allNums.ToString();
            var numFormName = (allNums.ToString().Length < 2) ? string.Empty : allNums.ToString().Substring(0, 2);

            var rmFuncKeyword = line.Replace("function", String.Empty);
            var functionName = Regex.Replace(rmFuncKeyword, @"\d", String.Empty);

            result.Add($"({functionName}{numFormName})");
            for (int i = 0; i < Convert.ToInt32(parmCount); i++)
            {
                result.Add("@" + i);
                result.Add("D=A");
                result.Add(stackPointer);
                result.Add("A=M");
                result.Add("M=D");
                result.Add(stackPointer);
                result.Add("M=M+1");
            }


            return result;
        }

        int lableCount = 0;
        public List<string> Call (string line)
        {
            var result = new List<string>();
            const string stackPointer = "@SP";
            lableCount++;
            var allNums = Convert.ToInt32(Regex.Replace(line, @"[^\d]", String.Empty));
            var parmCount = (allNums > 1) ? allNums.ToString().Substring(2) : allNums.ToString();
            var numFormName = (allNums < 1) ? string.Empty : allNums.ToString().Substring(0,2);

            var rmKeyword = line.Replace("call", String.Empty);
            var calledName = Regex.Replace(rmKeyword, @"\d", String.Empty);

            result.Add("@RETURN_LABEL" + lableCount);
            result.Add("D=A");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");
            result.Add("@LCL");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");
            result.Add("@ARG");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");
            result.Add("@THIS");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");
            result.Add("@THAT");
            result.Add("D=M");
            result.Add(stackPointer);
            result.Add("A=M");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("M=M+1");
            result.Add(stackPointer);
            result.Add("D=M");
            result.Add("@5");
            result.Add("D=D-A");
            result.Add("@" + parmCount);
            result.Add("D=D-A");
            result.Add("@ARG");
            result.Add("M=D");
            result.Add(stackPointer);
            result.Add("D=M");
            result.Add("@LCL");
            result.Add("M=D");
            result.Add("@" + calledName + numFormName);
            result.Add("0;JMP");
            result.Add($"(RETURN_LABEL{lableCount})");

            return result;
        }
    }
}
