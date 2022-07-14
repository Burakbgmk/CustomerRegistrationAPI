using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.Entities
{
    public class CustomerCountByCityReport : BaseEntity
    {
        public List<CustomerCountByCityReportDetail>? Details { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
