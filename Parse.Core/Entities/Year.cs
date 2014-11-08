﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace FleximReportManager.ReportParser.Entities
{
    [DataContract]
    struct Year
    {
        [DataMember]
        List<Month> months;

        public Year(List<Month> months)
        {
            this.months = months;
        }
    }


}