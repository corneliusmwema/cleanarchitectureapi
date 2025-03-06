namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public BaseEntity()
        {
            IsDeleted = false;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }


    }
}