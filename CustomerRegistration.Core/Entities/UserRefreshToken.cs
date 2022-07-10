using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.Entities
{
    public class UserRefreshToken : BaseEntity
    {
        //[JsonIgnore]
        //public int Id;
        [Key]
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
