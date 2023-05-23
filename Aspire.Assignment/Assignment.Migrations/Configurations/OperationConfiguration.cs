using Assignment.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Migrations.Configurations
{
    public class OperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.ToTable("Operation");
            builder.HasData
            (
                new Operation
                {
                    Id = 1,
                    OperationAccess = "ADD,EDIT,VIEW,DELETE"
                },
                new Operation
                {
                    Id = 2,
                    OperationAccess = "ADD,VIEW"
                }
            );
        }
    }
}
