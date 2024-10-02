namespace TaskResolution.Repositories.Core.Entities.Common
{
    public abstract class RelationEntity
    {
        public DateTime CreatedDateTime { get; set; }

        public Guid CreatedBy { get; set; }
    }

    public abstract class TransactionalEntity<T> : BaseEntity<T>
    {
        public DateTime UpdatedAt { get; set; }

        public Guid UpdatedBy { get; set; }
    }

    public abstract class TransactionalEntity : BaseEntity<Guid>
    {
        public DateTime UpdatedAt { get; set; }

        public Guid UpdatedBy { get; set; }
    }

    public abstract class BaseEntity<T>
    {
        public T ID { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
    }

    public abstract class TransactionalAndRelationEntity : BaseEntity<Guid>
    {
        public DateTime UpdatedAt { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }
    }
}