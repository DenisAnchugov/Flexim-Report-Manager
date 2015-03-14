using System;
using System.Collections.Generic;
using Parser;
using System.IO;

namespace ConsoleUI
{
    class CsvGenerate
    {
        static void Main()
        {
            var sw = new StreamWriter(@"C:\Users\Denis\Documents\FleximReportManager\parsed2.csv");
            var pl = new ParseLogic();
            var yearCheckIns = pl.ReadReports(new List<string>());

            foreach (var month in yearCheckIns.Months)
            {
                Console.WriteLine(month.ToString());
                sw.Write(month + "\n");
            }
            sw.Close();
        }
    }
}
