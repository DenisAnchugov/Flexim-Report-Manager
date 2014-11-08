using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using FleximReportManager.ReportParser.Enums;

namespace FleximReportManager.ReportParser.Entities
{
   public struct CheckIn
    {
       public string checkInMark;
       public DateTime checkInTime;
       public Double checkInRange;
       public string rangeType;

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
