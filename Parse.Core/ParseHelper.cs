using System.IO;
using System.Runtime.Serialization.Json;
using FleximReportManager.ReportParser.Entities;
using FleximReportManager.ReportParser.Enums;
using System;


namespace Parser
{
    public static class ParseHelper
    {
        public static DayOfTheWeek ParseDayOfTheWeek(string inputDayOfTheWeek)
        {
            switch (inputDayOfTheWeek)
            {
                case "Ma":
                    return DayOfTheWeek.Monday;
                case "Ti":
                    return DayOfTheWeek.Tuesday;
                case "Ke":
                    return DayOfTheWeek.Wednesday;
                case "To":
                    return DayOfTheWeek.Thursday;
                case "Pe":
                    return DayOfTheWeek.Friday;
            }
            return 0;
        }

        public static CheckInType CheckInsParse(string inputMark)
        {
            switch (inputMark)
            {
                case "SI":
                    return CheckInType.In;
                case "LS":
                    return CheckInType.Lunch;
                case "UL":
                    return CheckInType.Out;
                case "MU":
                    return CheckInType.Travel;
                case "SA":
                    return CheckInType.Sick;
                case "LO":
                    return CheckInType.Vacation;
            }
            return 0;
        }

        public static DayType DayTypesChecking(string dayMark)
        {
            switch (dayMark)
            {
                case "A":
                    return DayType.Ordinary;
                case "b":
                    return DayType.Holiday;
                default:
                    return 0;
            }
        }

        public static SpecialNoteType ParseSpecialMark(string inputspecialMark)
        {
            switch (inputspecialMark)
            {
                case "L":
                    return SpecialNoteType.Vacation;
                case "!":
                    return SpecialNoteType.ShortWH;
                case "M":
                    return SpecialNoteType.Travelling;
                case "S":
                    return SpecialNoteType.Sick;
                case "Y":
                    return SpecialNoteType.Overworked;
                case "-":
                    return SpecialNoteType.Underworked;
            }
            return 0;
        }

        public static DateTime ParseDate(string date)
        {
            var dayOfMonth = int.Parse(date.Substring(0, 2));
            var monthNumber = int.Parse(date.Substring(3, 2));
            var dateParsed = new DateTime(2013, monthNumber, dayOfMonth);

            return dateParsed;
        }

        public static RangeType MapCheckinMarkToRangeType(CheckInType checkInType)
        {
            switch (checkInType)
            {
                case CheckInType.In:
                    return RangeType.WorkTime;
                case CheckInType.Out:
                    return RangeType.OffTime;
                case CheckInType.Lunch:
                    return RangeType.LunchTime;
                case CheckInType.Travel:
                    return RangeType.OffTime;
                case CheckInType.Sick:
                    return RangeType.OffTime;
                case CheckInType.Vacation:
                    return RangeType.OffTime;
                default:
                    return RangeType.OffTime;
            }
        }

        public static string SerializeToJson(Year yearCheckIns)
        {
            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof (Year));
            ser.WriteObject(stream, yearCheckIns);
            var sr = new StreamReader(stream);
            stream.Position = 0;
            return sr.ReadToEnd();
        }
    }
}
