using SecPortal.Commons.Enums;

namespace SecPortal.Entities.Infrastructures
{
    public interface IStatusEntity
    {
        public StatusEnum Status { get; set; }
    }
}
