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

        private List<string> LoopthroughSwitch(List<string> file)
        {
            List<string> output = new List<string>();

            foreach (var line in file)
            {
                //if (line.Contains("push") && line.Contains("constant"))
                //{
                //    if (LogicalCommands.needLable)
                //    {
                //        output.AddRange(ArithmeticCommands.PushConstantHandleLable(line));
                //    }
                //    else
                //    {
                //        output.AddRange(ArithmeticCommands.PushConstant(line));
                //    }
                //}
                if (line.Contains("push"))
                {
                    output.AddRange(PushSwitch(line));
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
                //else if (line.Contains("pop") && line.Contains("static"))
                //{
                //    output.AddRange(ArithmeticCommands.PopStatic(line));
                //}
                //else if (line.Contains("push") && line.Contains("static"))
                //{
                //    output.AddRange(ArithmeticCommands.PushStatic(line));
                //}
                //else if (line.Contains("pop") && line.Contains("pointer"))
                //{
                //    output.AddRange(ArithmeticCommands.PopPointer(line));
                //}
                //else if (line.Contains("pop") && line.Contains("this"))
                //{
                //    output.AddRange(ArithmeticCommands.PopThis(line));
                //}
                //else if (line.Contains("pop") && line.Contains("that"))
                //{
                //    output.AddRange(ArithmeticCommands.PopThat(line));
                //}
                //else if (line.Contains("push") && line.Contains("pointer"))
                //{
                //    output.AddRange(ArithmeticCommands.PushPointer(line));
                //}
                //else if (line.Contains("push") && line.Contains("this"))
                //{
                //    output.AddRange(ArithmeticCommands.PushThis(line));
                //}
                //else if (line.Contains("push") && line.Contains("that"))
                //{
                //    output.AddRange(ArithmeticCommands.PushThat(line));
                //}
                //else if (line.Contains("pop") && line.Contains("local"))
                //{
                //    output.AddRange(ArithmeticCommands.PopLocal(line));
                //}
                //else if (line.Contains("pop") && line.Contains("argument"))
                //{
                //    output.AddRange(ArithmeticCommands.PopArgument(line));
                //}
                //else if (line.Contains("pop") && line.Contains("temp"))
                //{
                //    output.AddRange(ArithmeticCommands.PopTemp(line));
                //}
                //else if (line.Contains("push") && line.Contains("local"))
                //{
                //    output.AddRange(ArithmeticCommands.PushLocal(line));
                //}
                //else if (line.Contains("push") && line.Contains("argument"))
                //{
                //    output.AddRange(ArithmeticCommands.PushArgument(line));
                //}
                //else if (line.Contains("push") && line.Contains("temp"))
                //{
                //    output.AddRange(ArithmeticCommands.PushTemp(line));
                //}
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


        //private List<string> Loopthrough(List<string> file)
        //{
        //    List<string> output = new List<string>();

        //    foreach (var line in file)
        //    {
        //        if (line.Contains("push") && line.Contains("constant"))
        //        {
        //            if (LogicalCommands.needLable)
        //            {
        //                var pushConst = ArithmeticCommands.PushConstantHandleLable(line);
        //                output.AddRange(pushConst);
        //            }
        //            else
        //            {
        //                var pushConst = ArithmeticCommands.PushConstant(line);
        //                output.AddRange(pushConst);
        //            }

        //        }
        //        else if (line.Contains("add"))
        //        {
        //            var addConst = ArithmeticCommands.AddConstant(line);
        //            output.AddRange(addConst);
        //        }
        //        else if (line.Contains("eq"))
        //        {
        //            var eq = LogicalCommands.Equal();
        //            output.AddRange(eq);
        //        }
        //        else if (line.Contains("lt"))
        //        {
        //            var lt = LogicalCommands.LessThen();
        //            output.AddRange(lt);
        //        }
        //        else if (line.Contains("gt"))
        //        {
        //            var gt = LogicalCommands.GreaterThan();
        //            output.AddRange(gt);
        //        }
        //        else if (line.Contains("sub"))
        //        {
        //            var sub = ArithmeticCommands.Sub();
        //            output.AddRange(sub);
        //        }
        //        else if (line.Contains("neg"))
        //        {
        //            var neg = LogicalCommands.Neg();
        //            output.AddRange(neg);
        //        }
        //        else if (line.Contains("and"))
        //        {
        //            var and = LogicalCommands.And();
        //            output.AddRange(and);
        //        }
        //        else if (line.Contains("or"))
        //        {
        //            var or = LogicalCommands.Or();
        //            output.AddRange(or);
        //        }
        //        else if (line.Contains("not"))
        //        {
        //            var not = LogicalCommands.Not();
        //            output.AddRange(not);
        //        }
        //        else if (line.Contains("pop") && line.Contains("static"))
        //        {
        //            var pop = ArithmeticCommands.PopStatic(line);
        //            output.AddRange(pop);
        //        }
        //        else if (line.Contains("push") && line.Contains("static"))
        //        {
        //            var pushStatic = ArithmeticCommands.PushStatic(line);
        //            output.AddRange(pushStatic);
        //        }
        //        else if (line.Contains("pop") && line.Contains("pointer"))
        //        {
        //            var popPointer = ArithmeticCommands.PopPointer(line);
        //            output.AddRange(popPointer);
        //        }
        //        else if (line.Contains("pop") && line.Contains("this"))
        //        {
        //            var popThis = ArithmeticCommands.PopThis(line);
        //            output.AddRange(popThis);
        //        }
        //        else if (line.Contains("pop") && line.Contains("that"))
        //        {
        //            var popThat = ArithmeticCommands.PopThat(line);
        //            output.AddRange(popThat);
        //        }
        //        else if (line.Contains("push") && line.Contains("pointer"))
        //        {
        //            var pushPointer = ArithmeticCommands.PushPointer(line);
        //            output.AddRange(pushPointer);
        //        }
        //        else if (line.Contains("push") && line.Contains("this"))
        //        {
        //            var pushThis = ArithmeticCommands.PushThis(line);
        //            output.AddRange(pushThis);
        //        }
        //        else if (line.Contains("push") && line.Contains("that"))
        //        {
        //            var pushThat = ArithmeticCommands.PushThat(line);
        //            output.AddRange(pushThat);
        //        }
        //        else if (line.Contains("pop") && line.Contains("local"))
        //        {
        //            var popLocal = ArithmeticCommands.PopLocal(line);
        //            output.AddRange(popLocal);
        //        }
        //        else if (line.Contains("pop") && line.Contains("argument"))
        //        {
        //            var popArgument = ArithmeticCommands.PopArgument(line);
        //            output.AddRange(popArgument);
        //        }
        //        else if (line.Contains("pop") && line.Contains("temp"))
        //        {
        //            var popTemp = ArithmeticCommands.PopTemp(line);
        //            output.AddRange(popTemp);
        //        }
        //        else if (line.Contains("push") && line.Contains("local"))
        //        {
        //            var pushLocal = ArithmeticCommands.PushLocal(line);
        //            output.AddRange(pushLocal);
        //        }
        //        else if (line.Contains("push") && line.Contains("argument"))
        //        {
        //            var pushArgument = ArithmeticCommands.PushArgument(line);
        //            output.AddRange(pushArgument);
        //        }
        //        else if (line.Contains("push") && line.Contains("temp"))
        //        {
        //            var pushTemp = ArithmeticCommands.PushTemp(line);
        //            output.AddRange(pushTemp);
        //        }
        //        else
        //        {
        //            output.Add(line);
        //        }
        //    }

        //    return output;
        //}

        //private List<string> Loopthrough(List<string> file)
        //{
        //    List<string> output = new List<string>();

        //    foreach (var line in file)
        //    {
        //        if (line.Contains("push") && line.Contains("constant"))
        //        {
        //            if (needLable)
        //            {
        //                var pushConst = PushConstantHandleLable(line);
        //                output.AddRange(pushConst);
        //            }
        //            else
        //            {
        //                var pushConst = PushConstant(line);
        //                output.AddRange(pushConst);
        //            }

        //        }
        //        else if (line.Contains("add"))
        //        {
        //            var addConst = AddConstant(line);
        //            output.AddRange(addConst);
        //        }
        //        else if (line.Contains("eq"))
        //        {
        //            var eq = Equal();
        //            output.AddRange(eq);
        //        }
        //        else if (line.Contains("lt"))
        //        {
        //            var lt = LessThen();
        //            output.AddRange(lt);
        //        }
        //        else if (line.Contains("gt"))
        //        {
        //            var gt = GreaterThan();
        //            output.AddRange(gt);
        //        }
        //        else if (line.Contains("sub"))
        //        {
        //            var sub = Sub();
        //            var subStatic = subWOutD0();
        //            //output.AddRange(sub);
        //            output.AddRange(subStatic);
        //        }
        //        else if (line.Contains("neg"))
        //        {
        //            var neg = Neg();
        //            output.AddRange(neg);
        //        }
        //        else if (line.Contains("and"))
        //        {
        //            var and = And();
        //            output.AddRange(and);
        //        }
        //        else if (line.Contains("or"))
        //        {
        //            var or = Or();
        //            output.AddRange(or);
        //        }
        //        else if (line.Contains("not"))
        //        {
        //            var not = Not();
        //            output.AddRange(not);
        //        }
        //        else if (line.Contains("pop") && line.Contains("static"))
        //        {
        //            var pop = PopStatic(line);
        //            output.AddRange(pop);
        //        }
        //        else if (line.Contains("push") && line.Contains("static"))
        //        {
        //            var pushStatic = PushStatic(line);
        //            output.AddRange(pushStatic);
        //        }
        //        else if (line.Contains("pop") && line.Contains("pointer"))
        //        {
        //            var popPointer = PopPointer(line);
        //            output.AddRange(popPointer);
        //        }
        //        else if (line.Contains("pop") && line.Contains("this"))
        //        {
        //            var popThis = PopThis(line);
        //            output.AddRange(popThis);
        //        }
        //        else if (line.Contains("pop") && line.Contains("that"))
        //        {
        //            var popThat = PopThat(line);
        //            output.AddRange(popThat);
        //        }
        //        else if (line.Contains("push") && line.Contains("pointer"))
        //        {
        //            var pushPointer = PushPointer(line);
        //            output.AddRange(pushPointer);
        //        }
        //        else if (line.Contains("push") && line.Contains("this"))
        //        {
        //            var pushThis = PushThis(line);
        //            output.AddRange(pushThis);
        //        }
        //        else if (line.Contains("push") && line.Contains("that"))
        //        {
        //            var pushThat = PushThat(line);
        //            output.AddRange(pushThat);
        //        }
        //        else if (line.Contains("pop") && line.Contains("local"))
        //        {
        //            var popLocal = PopLocal(line);
        //            output.AddRange(popLocal);
        //        }
        //        else if (line.Contains("pop") && line.Contains("argument"))
        //        {
        //            var popArgument = PopArgument(line);
        //            output.AddRange(popArgument);
        //        }
        //        else if (line.Contains("pop") && line.Contains("temp"))
        //        {
        //            var popTemp = PopTemp(line);
        //            output.AddRange(popTemp);
        //        }
        //        else if (line.Contains("push") && line.Contains("local"))
        //        {
        //            var pushLocal = PushLocal(line);
        //            output.AddRange(pushLocal);
        //        }
        //        else if (line.Contains("push") && line.Contains("argument"))
        //        {
        //            var pushArgument = PushArgument(line);
        //            output.AddRange(pushArgument);
        //        }
        //        else if (line.Contains("push") && line.Contains("temp"))
        //        {
        //            var pushTemp = PushTemp(line);
        //            output.AddRange(pushTemp);
        //        }
        //        else
        //        {
        //            output.Add(line);
        //        }
        //    }

        //    return output;
        //}

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

        /// <summary>
        /// Command = push constant xx
        /// </summary>
        /// <returns></returns>
        //private List<string> PushConstant(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    var memLocation = Regex.Replace(command, @"[^\d]", String.Empty);

        //    result.Add("@" + memLocation);
        //    result.Add("D=A");
        //    result.Add(stackPointer);
        //    result.Add("A=M");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("M=M+1");
        //    return result;
        //}

        //private List<string> PushConstantHandleLable(string command)
        //{
        //    List<string> result = new List<string>();
        //    needLable = false;
        //    const string stackPointer = "@SP";

        //    var memLocation = Regex.Replace(command, @"[^\d]", String.Empty);

        //    result.Add($"({lable})");
        //    result.Add("@" + memLocation);
        //    result.Add("D=A");
        //    result.Add(stackPointer);
        //    result.Add("A=M");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("M=M+1");
        //    return result;
        //}

        //private List<string> AddConstant(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("A=A-1");
        //    result.Add("M=M+D");

        //    return result;
        //}


        //private int falseCount_Eq = 0;
        //private int continueCount_Eq = 0;
        //private bool needLable = false;
        //private string lable;
        //private List<string> Equal(/*string command*/)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("A=A-1");
        //    result.Add("D=M-D");
        //    result.Add("@FALSE" + falseCount_Eq);
        //    result.Add("D;JNE");
        //    result.Add(stackPointer);
        //    result.Add("A=M-1");
        //    result.Add("M=-1");
        //    result.Add("@CONTINUE" + continueCount_Eq);
        //    result.Add("0;JMP");
        //    result.Add($"(FALSE{falseCount_Eq})");
        //    result.Add(stackPointer);
        //    result.Add("A=M-1");
        //    result.Add("M=0");

        //    needLable = true;
        //    lable = "CONTINUE" + continueCount_Eq;
        //    falseCount_Eq++;
        //    continueCount_Eq++;

        //    return result;
        //    #region asm kode example
        //    /*
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * A=A-1
        //     * D=M-D
        //     * @FALSE0 // diff
        //     * D;JNE
        //     * @SP
        //     * A=M-1
        //     * M=-1
        //     * @CONTINUE0 // diff
        //     * 0;JMP
        //     * (FALSE0) // diff
        //     * @SP
        //     * A=M-1
        //     * M=0
        //    */
        //    #endregion
        //}
        
        //private List<string> LessThen()
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("A=A-1");
        //    result.Add("D=M-D");
        //    result.Add("@FALSE" + falseCount_Eq);
        //    result.Add("D;JGE");
        //    result.Add(stackPointer);
        //    result.Add("A=M-1");
        //    result.Add("M=-1");
        //    result.Add("@CONTINUE" + continueCount_Eq);
        //    result.Add("0;JMP");
        //    result.Add($"(FALSE{falseCount_Eq})");
        //    result.Add(stackPointer);
        //    result.Add("A=M-1");
        //    result.Add("M=0");

        //    needLable = true;
        //    lable = "CONTINUE" + continueCount_Eq;
        //    falseCount_Eq++;
        //    continueCount_Eq++;

        //    return result;
        //    #region Asm not example
        //    /* 
        //     * // lt
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * A=A-1
        //     * D=M-D
        //     * @FALSE3
        //     * D;JGE
        //     * @SP
        //     * A=M-1
        //     * M=-1
        //     * @CONTINUE3
        //     * 0;JMP
        //     * (FALSE3)
        //     * @SP
        //     * A=M-1
        //     * M=0
        //     */
        //    #endregion
        //}

        //private List<string> GreaterThan()
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("A=A-1");
        //    result.Add("D=M-D");
        //    result.Add("@FALSE" + falseCount_Eq);
        //    result.Add("D;JLE");
        //    result.Add(stackPointer);
        //    result.Add("A=M-1");
        //    result.Add("M=-1");
        //    result.Add("@CONTINUE" + continueCount_Eq);
        //    result.Add("0;JMP");
        //    result.Add($"(FALSE{falseCount_Eq})");
        //    result.Add(stackPointer);
        //    result.Add("A=M-1");
        //    result.Add("M=0");

        //    needLable = true;
        //    lable = "CONTINUE" + continueCount_Eq;
        //    falseCount_Eq++;
        //    continueCount_Eq++;

        //    return result;
        //    #region Asm not example
        //    /* 
        //     * // gt
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * A=A-1
        //     * D=M-D
        //     * @FALSE6
        //     * D;JLE
        //     * @SP
        //     * A=M-1
        //     * M=-1
        //     * @CONTINUE6
        //     * 0;JMP
        //     * (FALSE6)
        //     * @SP
        //     * A=M-1
        //     * M=0
        //     */
        //    #endregion
        //}

        //private List<string> Sub()
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("A=A-1");
        //    result.Add("M=M-D");
        //    result.Add("D=0");

        //    return result;
        //    #region Asm sub example
        //    /* 
        //     * // sub
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * A=A-1
        //     * M=M-D
        //     * D=0
        //     */
        //    #endregion
        //}

        //private List<string> Neg()
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("A=M-1");
        //    result.Add("M=D-M");

        //    return result;
        //    #region Asm neg example
        //    /* 
        //     * //neg
        //     * @SP
        //     * A=M-1
        //     * M=D-M
        //     */
        //    #endregion
        //}

        //private List<string> And()
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("A=A-1");
        //    result.Add("M=M&D");

        //    return result;
        //    #region Asm and example
        //    /* 
        //     * //and
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * A=A-1
        //     * M=M&D
        //     */
        //    #endregion
        //}
        
        //private List<string> Or()
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("A=A-1");
        //    result.Add("M=M|D");

        //    return result;
        //    #region Asm or example
        //    /*
        //     * // or
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * A=A-1
        //     * M=M|D
        //     */
        //    #endregion
        //}

        //private List<string> Not()
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("A=M-1");
        //    result.Add("M=!M");

        //    return result;
        //    #region Asm not example
        //    /* 
        //     * // not
        //     * @SP
        //     * A = M - 1
        //     * M = !M
        //     */
        //    #endregion
        //}


        //private const int staticBaseLocation = 16;

        //private List<string> PopStatic(string command)
        //{
        //    List<string> result = new List<string>();

        //    const string stackPointer = "@SP";
        //    int staticMemLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    var memLoccation = staticBaseLocation + staticMemLocation;

        //    result.Add("@" + memLoccation);
        //    result.Add("D=A");
        //    result.Add("@R13");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("@R13");
        //    result.Add("A=M");
        //    result.Add("M=D");

        //    return result;
        //    #region asm pop static example
        //    /*
        //     * // Pop static 8
        //     * @24
        //     * D=A
        //     * @R13
        //     * M=D
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * @R13
        //     * A=M
        //     * M=D
        //     */
        //    #endregion

        //}

        //// TODO: PushStatic og PushConstant er næsten ens
        //private List<string> PushStatic(string command)
        //{
        //    List<string> result = new List<string>();

        //    const string stackPointer = "@SP";
        //    //var staticMemLocation = Regex.Replace(command, @"[^\d]", String.Empty);
        //    int staticMemLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    var memLoccation = staticBaseLocation + staticMemLocation;

        //    result.Add("@" + memLoccation);
        //    result.Add("D=M");
        //    result.Add(stackPointer);
        //    result.Add("A=M");
        //    result.Add("M=D"); 
        //    result.Add(stackPointer);
        //    result.Add("M=M+1");


        //    return result;
        //    #region asm pop static example
        //    /*
        //     * // Push static 3
        //     * @19
        //     * D=M
        //     * @SP
        //     * A=M
        //     * M=D
        //     * @SP
        //     * M=M+1
        //     */
        //    #endregion

        //}

        //// TODO: sub ask mikkle hvorfor D=0
        //private List<string> subWOutD0()
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("A=A-1");
        //    result.Add("M=M-D");

        //    return result;
        //    #region Asm Sub form staticTest.asm example
        //    /*
        //     * // sub
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * A=A-1
        //     * M=M-D
        //     */
        //    #endregion
        //}

        //private List<string> PopPointer(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

        //    if (memLocation.Equals(0))
        //    {
        //        result.Add("@THIS");
        //    }
        //    else
        //    {
        //        result.Add("@THAT");
        //    }
            
        //    result.Add("D=A");
        //    result.Add("@R13");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("@R13");
        //    result.Add("A=M");
        //    result.Add("M=D");

        //    return result;
        //    #region Asm pop pointer
        //    /*
        //     * // Pop pointer 0
        //     * @THIS
        //     * D=A
        //     * @R13
        //     * M=D
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * @R13
        //     * A=M
        //     * M=D
        //     */
        //    #endregion
        //}

        //private List<string> PopThis(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

        //    result.Add("@THIS");
        //    result.Add("D=M");
        //    result.Add("@" + memLocation);
        //    result.Add("D=D+A");
        //    result.Add("@R13");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("@R13");
        //    result.Add("A=M");
        //    result.Add("M=D");

        //    return result;
        //    #region Asm pop this 2
        //    /*
        //     * // Pop this 2
        //     * @THIS
        //     * D=M
        //     * @2
        //     * D=D+A
        //     * @R13
        //     * M=D
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * @R13
        //     * A=M
        //     * M=D
        //     */
        //    #endregion
        //}

        //private List<string> PopThat(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));

        //    result.Add("@THAT");
        //    result.Add("D=M");
        //    result.Add("@" + memLocation);
        //    result.Add("D=D+A");
        //    result.Add("@R13");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("@R13");
        //    result.Add("A=M");
        //    result.Add("M=D");

        //    return result;
        //    #region Asm pop that
        //    /*
        //     * // Pop that 6
        //     * @THAT
        //     * D=M
        //     * @6
        //     * D=D+A
        //     * @R13
        //     * M=D
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * @R13
        //     * A=M
        //     * M=D
        //     */
        //    #endregion
        //}

        //private List<string> PushPointer(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    //var memLoccation = staticBaseLocation + staticMemLocation;

        //    if (memLocation.Equals(0))
        //    {
        //        result.Add("@THIS");
        //    }
        //    else
        //    {
        //        result.Add("@THAT");
        //    }

        //    result.Add("D=M");
        //    result.Add(stackPointer);
        //    result.Add("A=M");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("M=M+1");

        //    return result;
        //    #region Asm push pointer
        //    /*
        //     * // Push pointer 0
        //     * @THIS
        //     * D=M
        //     * @SP
        //     * A=M
        //     * M=D
        //     * @SP
        //     * M=M+1
        //     */
        //    #endregion
        //}

        //private List<string> PushThis(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    //var memLoccation = staticBaseLocation + staticMemLocation;

        //    result.Add("@THIS");
        //    result.Add("D=M");
        //    result.Add("@" + memLocation);
        //    result.Add("A=D+A");
        //    result.Add("D=M");
        //    result.Add(stackPointer);
        //    result.Add("A=M");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("M=M+1");

        //    return result;
        //    #region Asm push this
        //    /*
        //     * // Push this 2
        //     * @THIS
        //     * D=M
        //     * @2
        //     * A=D+A
        //     * D=M
        //     * @SP
        //     * A=M
        //     * M=D
        //     * @SP
        //     * M=M+1
        //     */
        //    #endregion
        //}

        //private List<string> PushThat(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    //var memLoccation = staticBaseLocation + staticMemLocation;

        //    result.Add("@THAT");
        //    result.Add("D=M");
        //    result.Add("@" + memLocation);
        //    result.Add("A=D+A");
        //    result.Add("D=M");
        //    result.Add(stackPointer);
        //    result.Add("A=M");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("M=M+1");

        //    return result;
        //    #region Asm push that
        //    /*
        //     * // push that 6 
        //     * @THAT
        //     * D=M
        //     * @6
        //     * A=D+A
        //     * D=M
        //     * @SP
        //     * A=M
        //     * M=D
        //     * @SP
        //     * M=M+1
        //     */
        //    #endregion
        //}

        //private List<string> PopLocal(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    //var memLoccation = staticBaseLocation + staticMemLocation;

        //    result.Add("@LCL");
        //    result.Add("D=M");
        //    result.Add("@" + memLocation);
        //    result.Add("D=D+A");
        //    result.Add("@R13");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("@R13");
        //    result.Add("A=M");
        //    result.Add("M=D");


        //    return result;
        //    #region Asm pop local
        //    /*
        //     * // Pop local 0
        //     * @LCL
        //     * D=M
        //     * @0
        //     * D=D+A
        //     * @R13
        //     * M=D
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * @R13
        //     * A=M
        //     * M=D
        //     */
        //    #endregion
        //}

        //private List<string> PopArgument(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    //var memLoccation = staticBaseLocation + staticMemLocation;

        //    result.Add("@ARG");
        //    result.Add("D=M");
        //    result.Add("@" + memLocation);
        //    result.Add("D=D+A");
        //    result.Add("@R13");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("@R13");
        //    result.Add("A=M");
        //    result.Add("M=D");


        //    return result;
        //    #region Asm pop argument
        //    /*
        //     * // Pop argument 2
        //     * @ARG
        //     * D=M
        //     * @2
        //     * D=D+A
        //     * @R13
        //     * M=D
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * @R13
        //     * A=M
        //     * M=D
        //     */
        //    #endregion
        //}

        //private const int tempBaseLocation = 5;
        //private List<string> PopTemp(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int TempMemLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    var memLoccation = tempBaseLocation + TempMemLocation;

        //    result.Add("@R5");
        //    result.Add("D=M");
        //    result.Add("@" + memLoccation);
        //    result.Add("D=D+A");
        //    result.Add("@R13");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("AM=M-1");
        //    result.Add("D=M");
        //    result.Add("@R13");
        //    result.Add("A=M");
        //    result.Add("M=D");

        //    return result;
        //    #region Asm pop temp
        //    /*
        //     * // Pop temp 6
        //     * @R5
        //     * D=M
        //     * @11
        //     * D=D+A
        //     * @R13
        //     * M=D
        //     * @SP
        //     * AM=M-1
        //     * D=M
        //     * @R13
        //     * A=M
        //     * M=D
        //     */
        //    #endregion
        //}

        //private List<string> PushLocal(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    //var memLoccation = staticBaseLocation + staticMemLocation;

        //    result.Add("@LCL");
        //    result.Add("D=M");
        //    result.Add("@" + memLocation);
        //    result.Add("A=D+A");
        //    result.Add("D=M");
        //    result.Add(stackPointer);
        //    result.Add("A=M");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("M=M+1");

        //    return result;
        //    #region Asm push local
        //    /*
        //     * // Push local 0
        //     * @LCL
        //     * D=M
        //     * @0
        //     * A=D+A
        //     * D=M
        //     * @SP
        //     * A=M
        //     * M=D
        //     * @SP
        //     * M=M+1
        //     */
        //    #endregion
        //}

        //private List<string> PushArgument(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int memLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    //var memLoccation = staticBaseLocation + staticMemLocation;

        //    result.Add("@ARG");
        //    result.Add("D=M");
        //    result.Add("@" + memLocation);
        //    result.Add("A=D+A");
        //    result.Add("D=M");
        //    result.Add(stackPointer);
        //    result.Add("A=M");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("M=M+1");

        //    return result;
        //    #region Asm push argument
        //    /*
        //     * // Push argument 1
        //     * @ARG
        //     * D=M
        //     * @1
        //     * A=D+A
        //     * D=M
        //     * @SP
        //     * A=M
        //     * M=D
        //     * @SP
        //     * M=M+1
        //     */
        //    #endregion
        //}

        //private List<string> PushTemp(string command)
        //{
        //    List<string> result = new List<string>();
        //    const string stackPointer = "@SP";

        //    int tempMemLocation = Convert.ToInt32(Regex.Replace(command, @"[^\d]", String.Empty));
        //    var memLoccation = tempBaseLocation + tempMemLocation;

        //    result.Add("@R5");
        //    result.Add("D=M");
        //    result.Add("@" + memLoccation);
        //    result.Add("A=D+A");
        //    result.Add("D=M");
        //    result.Add(stackPointer);
        //    result.Add("A=M");
        //    result.Add("M=D");
        //    result.Add(stackPointer);
        //    result.Add("M=M+1");

        //    return result;
        //    #region Asm push temp
        //    /*
        //     * // push temp 6
        //     * @R5
        //     * D=M
        //     * @11
        //     * A=D+A
        //     * D=M
        //     * @SP
        //     * A=M
        //     * M=D
        //     * @SP
        //     * M=M+1
        //     */
        //    #endregion
        //}

    }
}
