using System;

namespace SecPortal.Entities.Infrastructures
{
    public interface IDocumentNumber
    {
        public string DocumentNo { get; set; }
    }

    public interface IDocumentNumberWithIdentifier : IDocumentNumber, IBaseEntities<Guid>
    {

    }
}
