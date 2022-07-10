using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Data.Entities.Abstractions
{
    public interface IEntity
    {
        public int Id { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}
