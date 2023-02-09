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

            var asmFile = Loopthrough(cleanFile);
            Utilities.WriteToConsole(asmFile);
            Writefile(asmFile);


        }

        private List<string> Loopthrough(List<string> file)
        {
            List<string> output = new List<string>();

            foreach (var line in file)
            {
                if (line.Contains("push"))
                {
                    if (needLable)
                    {
                        var pushConst = PushConstantHandleLable(line);
                        output.AddRange(pushConst);
                    }
                    else
                    {
                        var pushConst = PushConstant(line);
                        output.AddRange(pushConst);
                    }

                }
                else if (line.Contains("add"))
                {
                    var addConst = AddConstant(line);
                    output.AddRange(addConst);
                }
                else if (line.Contains("eq"))
                {
                    var eq = Equal();
                    output.AddRange(eq);
                }
                else if (line.Contains("lt"))
                {
                    var lt = LessThen();
                    output.AddRange(lt);
                }
                else if (line.Contains("gt"))
                {
                    var gt = GreaterThan();
                    output.AddRange(gt);
                }
                else if (line.Contains("sub"))
                {
                    var sub = Sub();
                    output.AddRange(sub);
                }
                else if (line.Contains("neg"))
                {
                    var neg = Neg();
                    output.AddRange(neg);
                }
                else if (line.Contains("and"))
                {
                    var and = And();
                    output.AddRange(and);
                }
                else if (line.Contains("or"))
                {
                    var or = Or();
                    output.AddRange(or);
                }
                else if (line.Contains("not"))
                {
                    var not = Not();
                    output.AddRange(not);
                }
                else
                {
                    output.Add(line);
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
            //var fileContent = fileHandler.ReadVmFile(folderPathToSA + @"\SimpleAdd\SimpleAdd.vm");
            var fileContent = fileHandler.ReadVmFile(folderPathToSA + @"\StackTest\StackTest.vm");

            return fileContent;
        }

        private void Writefile(List<string> fileContent)
        {
            const string basePath = @"C:\Users\uncha\Desktop\";

            FileHandler fileHandler = new FileHandler();
            fileHandler.WriteAsmFile(fileContent, basePath + @"\MyStackTest.asm");
        }

        /// <summary>
        /// Command = push constant xx
        /// </summary>
        /// <returns></returns>
        private List<string> PushConstant(string command)
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

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

        private List<string> PushConstantHandleLable(string command)
        {
            List<string> result = new List<string>();
            needLable = false;
            const string stackPointer = "@SP";

            var memLocation = Regex.Replace(command, @"[^\d]", String.Empty);

            result.Add($"({lable})");
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

            return result;
        }


        private int falseCount_Eq = 0;
        private int continueCount_Eq = 0;
        private bool needLable = false;
        private string lable;
        private List<string> Equal(/*string command*/)
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

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

            needLable = true;
            lable = "CONTINUE" + continueCount_Eq;
            falseCount_Eq++;
            continueCount_Eq++;

            return result;
            #region asm kode example
            /*
             * @SP
             * AM=M-1
             * D=M
             * A=A-1
             * D=M-D
             * @FALSE0 // diff
             * D;JNE
             * @SP
             * A=M-1
             * M=-1
             * @CONTINUE0 // diff
             * 0;JMP
             * (FALSE0) // diff
             * @SP
             * A=M-1
             * M=0
            */
            #endregion
        }
        
        private List<string> LessThen()
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

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

            needLable = true;
            lable = "CONTINUE" + continueCount_Eq;
            falseCount_Eq++;
            continueCount_Eq++;

            return result;
            #region Asm not example
            /* 
             * // lt
             * @SP
             * AM=M-1
             * D=M
             * A=A-1
             * D=M-D
             * @FALSE3
             * D;JGE
             * @SP
             * A=M-1
             * M=-1
             * @CONTINUE3
             * 0;JMP
             * (FALSE3)
             * @SP
             * A=M-1
             * M=0
             */
            #endregion
        }

        private List<string> GreaterThan()
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

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

            needLable = true;
            lable = "CONTINUE" + continueCount_Eq;
            falseCount_Eq++;
            continueCount_Eq++;

            return result;
            #region Asm not example
            /* 
             * // gt
             * @SP
             * AM=M-1
             * D=M
             * A=A-1
             * D=M-D
             * @FALSE6
             * D;JLE
             * @SP
             * A=M-1
             * M=-1
             * @CONTINUE6
             * 0;JMP
             * (FALSE6)
             * @SP
             * A=M-1
             * M=0
             */
            #endregion
        }

        private List<string> Sub()
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("M=M-D");
            result.Add("D=0");

            return result;
            #region Asm sub example
            /* 
             * // sub
             * @SP
             * AM=M-1
             * D=M
             * A=A-1
             * M=M-D
             * D=0
             */
            #endregion
        }

        private List<string> Neg()
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=D-M");

            return result;
            #region Asm neg example
            /* 
             * //neg
             * @SP
             * A=M-1
             * M=D-M
             */
            #endregion
        }

        private List<string> And()
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("M=M&D");

            return result;
            #region Asm and example
            /* 
             * //and
             * @SP
             * AM=M-1
             * D=M
             * A=A-1
             * M=M&D
             */
            #endregion
        }
        
        private List<string> Or()
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

            result.Add(stackPointer);
            result.Add("AM=M-1");
            result.Add("D=M");
            result.Add("A=A-1");
            result.Add("M=M|D");

            return result;
            #region Asm or example
            /*
             * // or
             * @SP
             * AM=M-1
             * D=M
             * A=A-1
             * M=M|D
             */
            #endregion
        }

        private List<string> Not()
        {
            List<string> result = new List<string>();
            const string stackPointer = "@SP";

            result.Add(stackPointer);
            result.Add("A=M-1");
            result.Add("M=!M");

            return result;
            #region Asm not example
            /* 
             * // not
             * @SP
             * A = M - 1
             * M = !M
             */
            #endregion
        }


    }
}
