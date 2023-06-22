using Assignment.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Migrations.Configurations
{
    public class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasData
            (
                new Category
                {
                    Id= new Guid("5cce7a1e-34ae-4f2b-ab0b-415519a8b908"),
                    Type="CRM",
                    AddedOn= new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8728)
                },
                new Category
                {
                    Id = new Guid("c6f9b020-bebf-4f5a-b880-201e12ab6b26"),
                    Type = "Cloud",
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8736)
                },
                new Category
                {
                    Id = new Guid("2fb2c391-0bc6-40a9-b85c-d5196864762e"),
                    Type = "Client Side Scripting",
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8739)
                },
                new Category
                {
                    Id = new Guid("5e7dcdbc-bdea-4805-b454-a8761823e1c9"),
                    Type = "Programming Language",
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8742)
                }
            );
        }
    }
}
