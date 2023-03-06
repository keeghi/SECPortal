using SecPortal.Commons.Constant;
using SecPortal.Commons.Enums;
using SecPortal.Entities.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecPortal.Entities.Repositories;

namespace SecPortal.Entities.Data
{
    public class ApplicationDbContext : DbContext, IDataContext
    {
        private IOrganizationRepository _organizations;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IOrganizationRepository organizations)
            : base(options)
        {
            _organizations = organizations;
        }

        public IOrganizationRepository Organizations { 
            get {  return _organizations; }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        public async Task SaveChangesAsync(Guid? userId)
        {
            TrackChange(userId);
            await base.SaveChangesAsync();
        }

        public int SaveChanges(Guid? userId)
        {
            TrackChange(userId);
            return base.SaveChanges();
        }

        public int SaveChangesWithoutTracking()
        {
            return base.SaveChanges();
        }

        private void TrackChange(Guid? username)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntities && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntities)entity.Entity).Id = Guid.NewGuid();
                    ((BaseEntities)entity.Entity).CreatedAt = DateTime.UtcNow;
                    if (username != null)
                        ((BaseEntities)entity.Entity).CreatedById = username.Value;
                    ((BaseEntities)entity.Entity).IsActive = true;
                }
               ((BaseEntities)entity.Entity).ModifiedAt = DateTime.UtcNow;
                if (username != null)
                    ((BaseEntities)entity.Entity).ModifiedById = username;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<ApplicationUser>(b =>
            //{
            //    // Each User can have many entries in the UserRole join table
            //    b.HasMany(e => e.UserRoles)
            //        .WithOne(e => e.User)
            //        .HasForeignKey(ur => ur.UserId)
            //        .IsRequired();
            //});

            //builder.Entity<ApplicationRole>(b =>
            //{
            //    // Each Role can have many entries in the UserRole join table
            //    b.HasMany(e => e.UserRoles)
            //        .WithOne(e => e.Role)
            //        .HasForeignKey(ur => ur.RoleId)
            //        .IsRequired();
            //});


            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            var ROLE_SUPERADMIN_ID = Guid.Parse(RolesConstant.SuperAdminId);
            var ROLE_ADMIN_ID = Guid.Parse(RolesConstant.AdminId);
            var ROLE_SENIOR_ID = Guid.Parse(RolesConstant.SeniorId);
            var ROLE_SUPERVISOR_ID = Guid.Parse(RolesConstant.SupervisorId);
            var ROLE_READONLY_ID = Guid.Parse(RolesConstant.ReadOnlyId);
            //builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            //{
            //    Id = ROLE_SUPERADMIN_ID,
            //    Name = RolesEnum.SuperAdmin.ToString(),
            //    NormalizedName = RolesEnum.SuperAdmin.ToString().ToUpper()
            //});
            //builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            //{
            //    Id = ROLE_ADMIN_ID,
            //    Name = RolesEnum.Admin.ToString(),
            //    NormalizedName = RolesEnum.Admin.ToString().ToUpper()
            //});
            //builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            //{
            //    Id = ROLE_SENIOR_ID,
            //    Name = RolesEnum.Senior.ToString(),
            //    NormalizedName = RolesEnum.Senior.ToString().ToUpper(),
            //});
            //builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            //{
            //    Id = ROLE_SUPERVISOR_ID,
            //    Name = RolesEnum.Supervisor.ToString(),
            //    NormalizedName = RolesEnum.Supervisor.ToString().ToUpper(),
            //});
            //builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            //{
            //    Id = ROLE_READONLY_ID,
            //    Name = RolesEnum.ReadOnly.ToString(),
            //    NormalizedName = RolesEnum.ReadOnly.ToString().ToUpper(),
            //});

            //var USER_SUPERADMIN_ID = Guid.Parse("A97A27F8-BEC8-4435-9858-3E6F550E60C2");
            //builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            //{
            //    Id = USER_SUPERADMIN_ID,
            //    Name = "SuperAdmin",
            //    IsActive = true,
            //    Email = "admin@superadmin.com",
            //    NormalizedEmail = "admin@superadmin.com".ToUpper(),
            //    UserName = "admin@superadmin.com",
            //    NormalizedUserName = "admin@superadmin.com".ToUpper(),
            //    PhoneNumber = "+111111111111",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    PasswordHash = "AQAAAAEAACcQAAAAEP4Gd9w5NHiXv6/sAUhyIw1PqrYZNlt57/tbJP89Lp1hERI+5FrhTUKN7Zx/5EM14w==",
            //    SecurityStamp = "G36OIDMYMFPTMUEKVHFTKXAT57GLRGWO",
            //    ConcurrencyStamp = "c03eaa2b-f6c2-4b92-bd66-76175e11ea63"
            //});
            //var USER_ZANDEN_ID = Guid.Parse("3AB2332C-9CEF-42E3-BD23-D6AA0ACFEC84");
            //builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            //{
            //    Id = USER_ZANDEN_ID,
            //    Name = "Zanden",
            //    IsActive = true,
            //    Email = "zanden@zanden.com",
            //    NormalizedEmail = "zanden@zanden.com".ToUpper(),
            //    UserName = "zanden@zanden.com",
            //    NormalizedUserName = "zanden@zanden.com".ToUpper(),
            //    PhoneNumber = "+111111111111",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    PasswordHash = "AQAAAAEAACcQAAAAEP4Gd9w5NHiXv6/sAUhyIw1PqrYZNlt57/tbJP89Lp1hERI+5FrhTUKN7Zx/5EM14w==",
            //    SecurityStamp = "G36OIDMYMFPTMUEKVHFTKXAT57GLRGWO",
            //    ConcurrencyStamp = "1531F2A7-360F-40D8-A3D3-2BADB56413DA"
            //});
            //builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
            //{
            //    RoleId = ROLE_SUPERADMIN_ID,
            //    UserId = USER_SUPERADMIN_ID
            //});
            //builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
            //{
            //    RoleId = ROLE_SUPERADMIN_ID,
            //    UserId = USER_ZANDEN_ID
            //});
        }
    }

    //public class ApplicationUser : IdentityUser<Guid>, IBaseEntities<Guid>
    //{
    //    public string Name { get; set; }
    //    public bool IsActive { get; set; }
    //    public virtual ICollection<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();
    //    public string RoleName
    //    {
    //        get
    //        {
    //            if (UserRoles == null)
    //            {
    //                return "";
    //            }

    //            switch (UserRoles.Single().RoleId.ToString())
    //            {
    //                case RolesConstant.SuperAdminId:
    //                case RolesConstant.AdminId:
    //                    return "Admin";
    //                case RolesConstant.ReadOnlyId:
    //                    return "Read Only";
    //                case RolesConstant.SeniorId:
    //                    return "Senior/Staff";
    //                case RolesConstant.SupervisorId:
    //                    return "Supervisor";
    //                default:
    //                    throw new NotImplementedException();
    //            };
    //        }
    //    }

    //    public DateTime CreatedAt { get; set; }
    //    public Guid? CreatedById { get; set; }
    //    public DateTime? ModifiedAt { get; set; }
    //    public Guid? ModifiedById { get; set; }

    //    public bool CheckRelation(IDataContext context = null)
    //    {
    //        return false;
    //    }

    //    public override string ToString()
    //    {
    //        return $"{Name}".Trim();
    //    }

    //    public ApplicationUser()
    //    {
    //        IsActive = true;
    //    }
    //}
}
