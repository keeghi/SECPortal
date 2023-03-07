using SecPortal.Entities.Data;
using SecPortal.Entities.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecPortal.Entities.Infrastructures
{
    public class BaseEntities : IBaseEntities<int>
    {
        public int Id { get; set; }
        public int? CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ModifiedById { get; set; }
        [ForeignKey("ModifiedById")]
        public virtual User ModifiedBy { get; set; }
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
