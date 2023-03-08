using System;

namespace SecPortal.Commons.Infrastructures
{
    public interface IIdentifier
    {
        public int Id { get; set; }
    }

    public interface INullableIdentifier
    {
        public int? Id { get; set; }
    }
}
