using Assignment.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Migrations.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasData
            (
                CreateAdmin("Admin", "Admin@ayi.com", "AQAAAAEAACcQAAAAEK7fK9rklmNwB8J395U3LgJhevQEwGd/RazdOjguuNDbCnNIFoP9V8fq5hxckoPQew==") //password=Admin@123
            );
        }
        public static User CreateAdmin(string name ,string email, string password)
        {
            return new User
            {
                Id=5,
                Email = email,
                Username = name,
                RoleId = 1,
                OperationId = 1,
                Password= password
            };
        }
    }
}
