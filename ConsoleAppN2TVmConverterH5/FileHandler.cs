using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppN2TVmConverterH5
{
    public class FileHandler
    {
        public string[] ReadVmFile(string vmFilePath)
        {
            string[] contents = System.IO.File.ReadAllLines(vmFilePath);
            return contents;
        }


        /// <summary>
        /// this method craetes a new .hack file form a byte[] 
        /// </summary>
        /// <param name="vmFileContents"></param>
        /// <param name="pathToPlaceFile">the location to put the new file</param>
        public void WriteAsmFile(List<string> vmFileContents, string pathToPlaceFile)
        {
            using (StreamWriter streamWriter = new StreamWriter(pathToPlaceFile))
            {
                foreach (var line in vmFileContents)
                {
                    streamWriter.WriteLine(line);
                }

            }
        }
    }
}
