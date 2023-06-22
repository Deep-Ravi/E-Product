using Microsoft.EntityFrameworkCore;
using Assignment.Contracts.Data.Entities;
using Assignment.Migrations.Configurations;

namespace Assignment.Migrations
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>().AsEnumerable())
            {
                item.Entity.AddedOn = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new OperationConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
        }
        public DbSet<App> App { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Operation> Operation { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<SkillSet> SkillSet { get; set; }
    }
}