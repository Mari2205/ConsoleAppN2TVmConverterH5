using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppN2TVmConverterH5
{
    public class Utilities
    {
        private static string[] ReCodeComments(string[] asmFile)
        {

            for (int i = 0; i < asmFile.Length; i++)
            {
                if (asmFile[i].Contains("//"))
                {
                    if (asmFile[i].StartsWith("//"))
                    {
                        asmFile[i] = "";
                    }
                    else
                    {
                        var indexOfCumment = asmFile[i].IndexOf("//");
                        var f = asmFile[i].Remove(indexOfCumment);
                        asmFile[i] = f;
                    }

                }
            }

            var content = asmFile.Where(s => s != "").ToArray();
            return content;
        }

        private static List<string> RmWhiteSpaces(string[] strArr)
        {
            List<string> res = new List<string>();
            foreach (var item in strArr)
            {
                res.Add(item.Replace(" ", String.Empty));
            }

            return res;
        }

        public static List<string> CleanUpFile(string[] fileContent)
        {
            var rmCommit = ReCodeComments(fileContent);
            var rmWhiteSpace = RmWhiteSpaces(rmCommit);

            return rmWhiteSpace;
        }

        public static void WriteToConsole(List<string> vmFile)
        {
            foreach (var item in vmFile)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
