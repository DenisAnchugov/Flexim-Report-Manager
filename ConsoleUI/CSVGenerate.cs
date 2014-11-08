using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser;
using System.IO;

namespace ConsoleUI
{
    class CSVGenerate
    {
        static void Main()
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\Denis\Documents\FleximReportManager\parsed2.csv");
            ParseLogic pl = new ParseLogic();
            foreach (var month in pl.ReadReport(new List<string>()))
            {
                Console.WriteLine(month.ToString());
                sw.Write(month.ToString() + "\n");
            }
            sw.Close();
        }
    }
}
