using CustomerRegistration.Data.Entities.Abstractions;

namespace CustomerRegistration.Core.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
