using System;
using System.Globalization;
using FleximReportManager.ReportParser.Enums;

namespace FleximReportManager.ReportParser.Entities
{
   public struct CheckIn
   {
       string checkInMark;
       DateTime checkInTime;
       Double checkInRange;
       string rangeType;

        public CheckIn(CheckInType checkInMark, DateTime checkInTime, Double checkInRange, RangeType rangeType)
        {
            this.checkInMark = checkInMark.ToString();
            this.checkInTime = checkInTime;
            this.checkInRange = checkInRange;
            this.rangeType = rangeType.ToString();
        }

        public override string ToString()
        {
            string stringRepresentationOfCheckIn = String.Format("{0};{1};", this.checkInTime.ToString("HH:mm", CultureInfo.CreateSpecificCulture("en-EN")), this.checkInMark);
            return stringRepresentationOfCheckIn;
        }
    }    
}
