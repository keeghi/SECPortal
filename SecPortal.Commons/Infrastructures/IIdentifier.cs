using System;

namespace SecPortal.Commons.Infrastructures
{
    public interface IIdentifier
    {
        public Guid Id { get; set; }
    }

    public interface INullableIdentifier
    {
        public Guid? Id { get; set; }
    }
}
