namespace Domain.Commons
{
    public abstract class AudiTable : BaseEntity
    {
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}