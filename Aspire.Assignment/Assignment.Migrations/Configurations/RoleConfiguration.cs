using Assignment.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Migrations.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasData
            (
                new Role
                {
                    Id = 1,
                    Name = "ADMIN"
                },
                new Role
                {
                    Id = 2,
                    Name = "GUEST"
                },
                new Role
                {
                    Id = 3,
                    Name = "DEVELOPER"
                },
                new Role
                {
                    Id = 4,
                    Name = "MANAGER"
                }
            );
        }
    }
}
