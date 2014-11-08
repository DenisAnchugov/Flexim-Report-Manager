using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using FleximReportManager.ReportParser.Enums;
using FleximReportManager.ReportParser.Entities;
using System.IO;

namespace Parser
{
    public class ParseLogic
    {
        List<Month> months = new List<Month>();      
        string monthPeriod = string.Empty;
        string ratamahata;
        public List<Month> ReadReport(List<string> filePaths)
        {
            months = new List<Month>();

            //string folderPath = @"C:\Users\Denis\Source\Repos\FleximReportManager\fileToParse";
            //foreach (string file in Directory.EnumerateFiles(folderPath, "*.txt"))
            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                FinalDayList(lines);
            }
            Year year = new Year(months);
            MemoryStream stream = new MemoryStream();
            System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(Year));
            ser.WriteObject(stream, year);
            var sr = new StreamReader(stream);
            stream.Position = 0;
            ratamahata = sr.ReadToEnd();

            return months;
        }
        public string ReturnJson(List<string> filePaths)
        {
            ReadReport(filePaths);
            return ratamahata;
        }

        public void FinalDayList(string[] lines)
        {
            List<Day> days = new List<Day>();
            string dayLaneRegExp = @"\s+\w{2}\s+\d{2}[.]\d{2}\w";
            string checkInsRegExp = @"\d{2}[:]\d{2}\w{2}";
            string monthPeriodRegExp = @"\d{2}[.]\d{2}[.]\d{4}\s[-]\s\d{2}[.]\d{2}[.]\d{4}";
            string monthPeriod = SingleDayGenerate(days, dayLaneRegExp, checkInsRegExp, lines, monthPeriodRegExp);

            months.Add(new Month(days, monthPeriod));                                 
        }

        public string SingleDayGenerate(List<Day> days, string dayLaneRegExp, string checkInsRegExp, string[] lines, string monthPeriodRegExp)
        {
            foreach (string line in lines)
            {
                if (Regex.IsMatch(line, dayLaneRegExp))
                {
                    DayOfTheWeek dayOfTheWeek = Helper.ParseDayOfTheWeek(line.Substring(1, 2));
                    DayType dayType = Helper.DayTypesChecking(line.Substring(9, 1));
                    DateTime date = Helper.ParseDate(line.Substring(4, 6));

                    string AllCheckIns = line.Substring(11, 35);
                    List<CheckIn> checkIns = new List<CheckIn>();

                    DateTime referenceCheckIn = DateTime.ParseExact("07:00", "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    CheckInType checkInMark = new CheckInType();
                    int i = 0;
                    foreach (Match match in Regex.Matches(AllCheckIns, checkInsRegExp))
                    {
                        
                        string matchString = match.Value.ToString();
                        DateTime checkInTime = DateTime.ParseExact(matchString.Substring(0, 5), "HH:mm", CultureInfo.InvariantCulture);                                         
                        TimeSpan checkInRange = checkInTime.Subtract(referenceCheckIn);
                        RangeType rangeType = new RangeType();
                        referenceCheckIn = checkInTime;
                        if(i == 0)
                        {
                            rangeType = RangeType.OffTime;
                        }
                        else
                        {
                            rangeType = Helper.MapCheckinMarkToRangeType(checkInMark);
                        }
                        CheckIn checkIn = new CheckIn(checkInMark, checkInTime, checkInRange.TotalMinutes, rangeType);
                        checkInMark = Helper.CheckInsParse(matchString.Substring(5, 2));   
                        checkIns.Add(checkIn);
                        i++;
                    }

                    double saldo = Double.Parse(line.Substring(57, 5).Trim(), CultureInfo.InvariantCulture);
                    double workingHoursDay = Double.Parse(line.Substring(50, 4).Trim(), CultureInfo.InvariantCulture);

                    SpecialNoteType specialMark = SpecialNoteType.None;
                    if (line.Length > 63)
                    {
                        specialMark = Helper.ParseSpecialMark(line.Substring(70, 1));
                    }

                    Day day = new Day(dayOfTheWeek, date, dayType, checkIns, workingHoursDay, saldo, specialMark);
                    days.Add(day);
                }
                if (Regex.IsMatch(line, monthPeriodRegExp))
                {
                    monthPeriod = Regex.Match(line, monthPeriodRegExp).ToString();
                }
            }
            return monthPeriod;
        }
    }
}
