using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleximReportManager.ReportParser.Entities
{
    public struct Month
    {
        public List<Day> days;
        public string monthPeriod;

        public Month(List<Day> days, string monthPeriod)
        {
            this.monthPeriod = monthPeriod;
            this.days = days;           
        }
    }
}
