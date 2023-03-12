using SecPortal.Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading.Tasks;
using SecPortal.Entities.Repositories;
using SecPortal.Entities.Entities;

namespace SecPortal.Entities.Infrastructures
{
    public interface IDataContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Organization> Organizations { get; set; }
        DbSet<OrganizationAccessFilter> OrganizationAccessFilters { get; set; }
        DbSet<ApplicationLog> ApplicationLogs { get; set; }
        DbSet<AuditLog> AuditLogs { get; set; }

        DatabaseFacade Database { get; }
        EntityEntry Entry(object entity);
        int SaveChanges(int? userId);
        int SaveChangesWithoutTracking();
        Task SaveChangesAsync(int? userId);
    }

    public delegate int SaveChangesDelegate(string userId);
}
