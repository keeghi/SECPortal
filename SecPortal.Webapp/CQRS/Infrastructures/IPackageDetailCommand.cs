using SecPortal.Commons.Infrastructures;
using System;
using System.Collections.Generic;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public interface IPackageDetailCommand<T>
        where T : INullableIdentifier
    {
        public Guid PackageId { get; set; }
        public List<Guid> RemovedIds { get; set; }
        public List<T> Items { get; set; }
    }
}
