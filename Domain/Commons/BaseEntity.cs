using System.ComponentModel.DataAnnotations;

namespace Domain.Commons
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}
