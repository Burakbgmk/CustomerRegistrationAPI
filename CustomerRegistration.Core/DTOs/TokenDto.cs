using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.DTOs
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        [JsonIgnore]
        public DateTime AccessTokenExpiration { get; set; }

        public string RefreshToken { get; set; }

        [JsonIgnore]
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
