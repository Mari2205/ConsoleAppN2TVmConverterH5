using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppN2TVmConverterH5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();

            parser.ParseVmFile();
        }
    }
}
