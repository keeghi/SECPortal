using SecPortal.Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SecPortal.Entities.Infrastructures
{
    public interface IDataContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DatabaseFacade Database { get; }
        EntityEntry Entry(object entity);
        int SaveChanges(Guid? userId);
        int SaveChangesWithoutTracking();
        Task SaveChangesAsync(Guid? userId);
    }

    public delegate int SaveChangesDelegate(string userId);
}
