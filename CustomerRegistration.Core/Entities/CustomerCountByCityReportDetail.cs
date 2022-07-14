using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.Entities
{
    public class CustomerCountByCityReportDetail : BaseEntity
    {
        public string City { get; set; }
        public int Count { get; set; }
        public int ReportId { get; set; }
        public CustomerCountByCityReport Report { get; set; }
    }
}
