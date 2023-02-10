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
            var fileContent = GetFile();
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
                else if (line.Contains("add"))
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
                return ArithmeticCommands.PushArgument(line);
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

        private string[] GetFile()
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

        private void Writefile(List<string> fileContent)
        {
            const string basePath = @"C:\Users\uncha\Desktop\";

            FileHandler fileHandler = new FileHandler();
            fileHandler.WriteAsmFile(fileContent, basePath + @"\MyStaticTest.asm");
        }

    }
}
