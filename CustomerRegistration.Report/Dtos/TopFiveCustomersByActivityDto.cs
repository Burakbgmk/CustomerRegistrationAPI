using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Report.Dtos
{
    public class TopFiveCustomersByActivityDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int ActivityCount { get; set; }
    }
}
