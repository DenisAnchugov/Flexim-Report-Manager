using FleximReportManager.ReportParser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Parser
{
    class Helper
    {
        public static DayOfTheWeek ParseDayOfTheWeek(string inputDayOfTheWeek)
        {
            if (inputDayOfTheWeek == "Ma")
            {
                return DayOfTheWeek.Monday;
            }
            else if (inputDayOfTheWeek == "Ti")
            {
                return DayOfTheWeek.Tuesday;
            }
            else if (inputDayOfTheWeek == "Ke")
            {
                return DayOfTheWeek.Wednesday;
            }
            else if (inputDayOfTheWeek == "To")
            {
                return DayOfTheWeek.Thursday;
            }
            else if (inputDayOfTheWeek == "Pe")
            {
                return DayOfTheWeek.Friday;
            }
            return 0;
        }

        public static CheckInType CheckInsParse(string inputMark)
        {
            if (inputMark == "SI")
            {
                return CheckInType.In;
            }
            else if (inputMark == "LS")
            {
                return CheckInType.Lunch;
            }
            else if (inputMark == "UL")
            {
                return CheckInType.Out;
            }
            else if (inputMark == "MU")
            {
                return CheckInType.Travel;
            }
            else if (inputMark == "SA")
            {
                return CheckInType.Sick;
            }
            else if (inputMark == "LO")
            {
                return CheckInType.Vacation;
            }
            return 0;
        }

        public static DayType DayTypesChecking(string dayMark)
        {
            if (dayMark == "A")
            {
                return DayType.Ordinary;
            }
            else if (dayMark == "b")
            {
                return DayType.Holiday;
            }
            else return 0;
        }

        public static SpecialNoteType ParseSpecialMark(string inputspecialMark)
        {
            if (inputspecialMark == "L")
            {
                return SpecialNoteType.Vacation;
            }
            else if (inputspecialMark == "!")
            {
                return SpecialNoteType.ShortWH;
            }
            else if (inputspecialMark == "M")
            {
                return SpecialNoteType.Travelling;
            }
            else if (inputspecialMark == "S")
            {
                return SpecialNoteType.Sick;
            }
            else if (inputspecialMark == "Y")
            {
                return SpecialNoteType.Overworked;
            }
            else if (inputspecialMark == "-")
            {
                return SpecialNoteType.Underworked;
            }
            return 0;
        }

        public static DateTime ParseDate(string date)
        {
            int dayOfMonth = int.Parse(date.Substring(0, 2));
            int monthNumber = int.Parse(date.Substring(3, 2));
            DateTime dateParsed = new DateTime(2013, monthNumber, dayOfMonth);

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
    }
}
