using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.Entities
{
    public class TopFiveCustomersByActivityReportDetail : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int ActivityCount { get; set; }
        public int ReportId { get; set; }
        public TopFiveCustomersByActivityReport Report { get; set; }
    }
}
