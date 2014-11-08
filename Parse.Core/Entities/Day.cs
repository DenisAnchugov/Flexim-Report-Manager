using FleximReportManager.ReportParser.Enums;
using Parser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleximReportManager.ReportParser.Entities
{
    public struct Day
    {
        public DayOfTheWeek dayOfTheWeek;
        public DayType dayMark;
        public DateTime dateParsed;
        public string stringDate;
        public List<CheckIn> checkIns;
        public double workingHoursDay; 
        public double saldo;
        public SpecialNoteType specialMark;

        public Day(DayOfTheWeek dayOfTheWeek, DateTime dateParsed, DayType dayMark, List<CheckIn> checkIns, double workingHoursDay, double saldo, SpecialNoteType specialMark)
        {
            this.dayOfTheWeek = dayOfTheWeek;
            this.dateParsed = dateParsed;
            this.dayMark = dayMark;
            this.workingHoursDay = workingHoursDay;
            this.saldo = saldo;
            this.specialMark = specialMark;
            this.checkIns = checkIns;
            this.stringDate = DateBuilder(dateParsed, dayOfTheWeek);
        }

        public override string ToString()
        {
            string stringRepresentationOfDateAndDayType = String.Format("{0};{1}", this.dateParsed.ToString("m", CultureInfo.CreateSpecificCulture("en-EN")), this.dayMark);
            string stringRepresentationOfDay = String.Format("{0};{1};{2};{3};{4};", this.dayOfTheWeek, stringRepresentationOfDateAndDayType, this.workingHoursDay, this.saldo, this.specialMark);

            foreach (CheckIn checkIn in checkIns)
            {
                stringRepresentationOfDay = stringRepresentationOfDay + checkIn.ToString();
            }
            return stringRepresentationOfDay;
        }

        public static string DateBuilder(DateTime date, DayOfTheWeek dayOfTheWeek)
        {
            string output = string.Empty;
            output += dayOfTheWeek + ", " + date.Date.ToString("dd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            return output;
        }
    }
}

