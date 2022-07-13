using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.DTOs
{
    public class CustomerDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        [JsonIgnore]
        public string? Photograph { get; set; }

        public string? Phone { get; set; }
        public string? City { get; set; }
    }
}
