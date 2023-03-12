using SecPortal.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecPortal.Entities.Helpers
{
    public interface IRepositoryFilter
    {
        public Role CurrentRole { get; }
    }
}
