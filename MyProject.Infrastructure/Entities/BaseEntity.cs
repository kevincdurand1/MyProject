using System;
using System.ComponentModel.DataAnnotations;
using MyProject.Infrastructure.Interfaces;

namespace MyProject.Infrastructure.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {

        public virtual int Id { get; set; }

        [MaxLength(200)]
        public virtual string CreatedBy { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        [MaxLength(200)]
        public virtual string UpdatedBy { get; set; }

        public virtual DateTime UpdatedDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        [MaxLength(200)]
        public virtual string DeletedBy { get; set; }

        public virtual DateTime DeletedDate { get; set; }
    }
}
