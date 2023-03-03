using SecPortal.Commons.Enums;
using SecPortal.Entities.Infrastructures;

namespace SecPortal.Entities.Entities
{
    public class AuditLog : BaseEntities
    {
        public string Entity { get; set; }
        public AuditType Type { get; set; }
    }
}
