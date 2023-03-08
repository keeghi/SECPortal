using SecPortal.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecPortal.Entities.Helpers
{
    public class RepositoryFilter : IRepositoryFilter
    {
        public Role CurrentRole
        {
            get
            {
                return new Role()
                {
                    CanAccessAllOrganizations = true,
                };
            }
        }
    }
}
