using SecPortal.Entities.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecPortal.Entities.Infrastructures
{
    public class BaseEntities : IBaseEntities<Guid>
    {
        public Guid Id { get; set; }
        public Guid? CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ModifiedById { get; set; }
        [ForeignKey("ModifiedById")]
        public virtual ApplicationUser ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// CALL THIS WHEN ALL TRANSACTION ARE FINISHED
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool CheckRelation(IDataContext context = null)
        {
            return true;
        }

        public BaseEntities()
        {
            IsActive = true;
        }
    }
}
