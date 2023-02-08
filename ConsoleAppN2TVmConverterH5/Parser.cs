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
            //const string basePath = @"C:\Users\uncha\Desktop\nand2tetris\projects\07\";
            //const string folderPathToSA = basePath + @"StackArithmetic\";
            //const string folderPathToMA = basePath + @"MemoryAccess\";

            //FileHandler fileHandler = new FileHandler();
            //var fileContent = fileHandler.ReadVmFile(folderPathToSA + @"\SimpleAdd\SimpleAdd.vm");

            var fileContent = GetFile();
            var cleanFile = Utilities.CleanUpFile(fileContent);


            var asmFile = Loopthow(cleanFile); 
            Utilities.WriteToConsole(asmFile);


        }

        private List<string> Loopthow(List<string> file)
        {
            List<string> output = new List<string>();

            foreach (var line in file)
            {
                if (line.Contains("push"))
                {
                    var pushConst = PushConstant(line);
                    output.AddRange(pushConst);
                }
                else
                {
                    //output.Add(line);
                    var addConst = AddConstant(line);
                    output.AddRange(addConst);
                }
            }

            return output;
        }

        private string[] GetFile()
        {
            const string basePath = @"C:\Users\uncha\Desktop\nand2tetris\projects\07\";
            const string folderPathToSA = basePath + @"StackArithmetic\";
            const string folderPathToMA = basePath + @"MemoryAccess\";

            FileHandler fileHandler = new FileHandler();
            var fileContent = fileHandler.ReadVmFile(folderPathToSA + @"\SimpleAdd\SimpleAdd.vm");

            return fileContent;
        }

        /// <summary>
        /// Command = push constant xx
        /// </summary>
        /// <returns></returns>
        private List<string> PushConstant(string command)
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

            //int MemLocation = command.Replace();
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

        private List<string> AddConstant(string command)
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("M=M+D");
            //int MemLocation = command.Replace();
            //var memLocation = Regex.Replace(command, @"[^\d]", String.Empty);

            //result.Add("@" + memLocation);
            //result.Add("D=A");
            //result.Add(stackPointer);
            //result.Add("A=M");
            //result.Add("M=D");
            //result.Add(stackPointer);
            //result.Add("M=M+1");
            return result;
        }

    }
}
