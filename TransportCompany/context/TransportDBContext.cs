
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TransportCompany.Dto_s.Branches;
using TransportCompany.Models;

namespace TransportCompany.context
{
    public class TransportDBContext : IdentityDbContext<User>
    {
        public TransportDBContext(DbContextOptions<TransportDBContext> options) : base(options)
        {

        }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Message> Message { get; set; } = default!;

        public DbSet<Truck> Trucks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //BRANCHES

            builder.Entity<Branch>().HasMany(b => b.Users).WithOne(u => u.Branch).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Branch>().HasMany(b => b.RoutesFrom).WithOne(u => u.RouteFrom).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Branch>().HasMany(b => b.RoutesTo).WithOne(u => u.RouteTo).OnDelete(DeleteBehavior.NoAction);
            var Branch = new Branch
            {
                Id = 1,
                City = "Medellín",
                Code = "MED001",
                Name = "Centralita",
                Telephone = "3103942961",
                Users = new List<User>(),
                RoutesFrom = new List<Truck>(),
                RoutesTo = new List<Truck>()
            };
            builder.Entity<Branch>().HasData(Branch) ;


            //ROLES
            var roles = new List<IdentityRole> {
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Name = "gerente",
                    NormalizedName = "GERENTE",
                },
                new IdentityRole
                {
                    Name = "visitor",
                    NormalizedName = "VISITOR",
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            //USER 

            var passwordHasher = new PasswordHasher<User>();
            var admin = new User
            {
                Email = "admin0910@gmail.com",
                UserName = "administrador",
                NormalizedUserName = "ADMINISTRADOR",
                NormalizedEmail = "ADMIN0910@GMAIL.COM",
                JoiningDate = new DateTime(2020, 12, 25),
                BirthDate = new DateTime(2006, 06, 20),
                Salary = 1300000,
                BranchId = Branch.Id,

            };


            admin.PasswordHash = passwordHasher.HashPassword(admin, "admin123");
            builder.Entity<User>().HasData(admin);

            IdentityRole adminRole = roles.FirstOrDefault(t => t.Name == "admin")!;
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = admin.Id,
                    RoleId = adminRole.Id

                });
            builder.Entity<User>().HasMany(u => u.Messages).WithOne(m => m.User).OnDelete(DeleteBehavior.Cascade);

            //STATUS
            builder.Entity<Status>().HasMany(s => s.Trucks).WithOne(t => t.Status).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Status>().HasData(new List<Status>
            {
                new Status
                {
                    Id = 1,
                    Name = "parchado",
                },
                new Status
                {
                    Id = 2,
                    Name = "Running",
                },
                new Status
                {
                    Id = 3,
                    Name = "Accidental",
                },
            });

        }
    }
}
