using System;
using System.ComponentModel.DataAnnotations;

namespace SecPortal.Entities.Infrastructures
{
    public interface IBaseEntities<T>
    {
        [Key]
        T Id { get; set; }
        Guid? CreatedById { get; set; }
        DateTime CreatedAt { get; set; }
        Guid? ModifiedById { get; set; }
        DateTime? ModifiedAt { get; set; }
        public bool IsActive { get; set; }

        bool CheckRelation(IDataContext context = null);
    }
}
