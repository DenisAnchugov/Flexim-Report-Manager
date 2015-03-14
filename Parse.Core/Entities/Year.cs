using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FleximReportManager.ReportParser.Entities
{
    [DataContract]
    public struct Year
    {
        [DataMember]
        public List<Month> Months { get; set; }

        public Year(List<Month> months) : this()
        {
            Months = months;
        }
    }
}
