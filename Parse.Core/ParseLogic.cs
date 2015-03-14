using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using FleximReportManager.ReportParser.Enums;
using FleximReportManager.ReportParser.Entities;
using System.IO;

namespace Parser
{
    public class ParseLogic
    {
        public Year ReadReports(List<string> filePaths)
        {
            var months = filePaths.Select(File.ReadAllLines).Select(ParseReport).ToList();
            return new Year(months);
        }
        public Month ParseReport(string[] lines)
        {
            const string dayLaneRegExp = @"\s+\w{2}\s+\d{2}[.]\d{2}\w";
            const string checkInsRegExp = @"\d{2}[:]\d{2}\w{2}";
            const string startAndEndDatesRegExp = @"\d{2}[.]\d{2}[.]\d{4}\s[-]\s\d{2}[.]\d{2}[.]\d{4}";

            return ParseMonthCheckIns(dayLaneRegExp, checkInsRegExp, lines, startAndEndDatesRegExp);
        }
        public Month ParseMonthCheckIns(string dayLaneRegExp, string checkInsRegExp, string[] lines, string monthPeriodRegExp)
        {
            var days = new List<Day>();
            var startAndEndDate = String.Empty;

            foreach (var line in lines)
            {
                if (Regex.IsMatch(line, dayLaneRegExp))
                {
                    days.Add(ParseDay(checkInsRegExp, line));
                }

                if (Regex.IsMatch(line, monthPeriodRegExp))
                {
                    startAndEndDate = Regex.Match(line, monthPeriodRegExp).ToString();
                }
            }

            return new Month(days, startAndEndDate);
        }
        private static Day ParseDay(string checkInsRegExp, string line)
        {
            var dayOfTheWeek = ParseHelper.ParseDayOfTheWeek(line.Substring(1, 2));
            var dayType = ParseHelper.DayTypesChecking(line.Substring(9, 1));
            var date = ParseHelper.ParseDate(line.Substring(4, 6));

            var allCheckInsString = line.Substring(11, 35);
            var listOfCheckIns = ParseDayCheckIns(checkInsRegExp, allCheckInsString);

            var saldo = Double.Parse(line.Substring(57, 5).Trim(), CultureInfo.InvariantCulture);
            var workingHoursDay = Double.Parse(line.Substring(50, 4).Trim(), CultureInfo.InvariantCulture);

            var specialMark = SpecialNoteType.None;
            if (line.Length > 63)
            {
                specialMark = ParseHelper.ParseSpecialMark(line.Substring(70, 1));
            }

            return new Day(dayOfTheWeek, date, dayType, listOfCheckIns, workingHoursDay, saldo, specialMark);
        }
        private static List<CheckIn> ParseDayCheckIns(string checkInsRegExp, string allCheckIns)
        {
            var checkInMark = new CheckInType();
            var checkIns = new List<CheckIn>();
            var referenceCheckIn = DateTime.ParseExact("07:00", "HH:mm", CultureInfo.InvariantCulture);

            bool isFirstCheckIn = true;
            foreach (Match match in Regex.Matches(allCheckIns, checkInsRegExp))
            {
                var matchString = match.Value;
                var checkInTime = DateTime.ParseExact(matchString.Substring(0, 5), "HH:mm", CultureInfo.InvariantCulture);
                var checkInRange = checkInTime.Subtract(referenceCheckIn);

                referenceCheckIn = checkInTime;
                var rangeType = isFirstCheckIn ? RangeType.OffTime : ParseHelper.MapCheckinMarkToRangeType(checkInMark);
                var checkIn = new CheckIn(checkInMark, checkInTime, checkInRange.TotalMinutes, rangeType);

                checkInMark = ParseHelper.CheckInsParse(matchString.Substring(5, 2));
                checkIns.Add(checkIn);
                isFirstCheckIn = false;
            }
            return checkIns;
        }
    }
}
