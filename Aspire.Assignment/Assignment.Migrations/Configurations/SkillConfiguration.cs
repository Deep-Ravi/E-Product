using Assignment.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Migrations.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("Skill");
            builder.HasData
            (
                new Skill
                {
                    Id = new Guid("1701DC57-CCB9-403E-BDC2-B3BC460C95AC"),
                    Name = "Angular",
                    CategoryId= new Guid("2fb2c391-0bc6-40a9-b85c-d5196864762e"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8873)
                },
                new Skill
                {
                    Id = new Guid("241FD9C6-50E0-4D8C-90EE-A295E58EC9BA"),
                    Name = "React JS",
                    CategoryId = new Guid("2fb2c391-0bc6-40a9-b85c-d5196864762e"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8877)
                },
                new Skill
                {
                    Id = new Guid("09B8F8E0-D431-4118-A2D4-A9343CF10033"),
                    Name = "JQuery",
                    CategoryId = new Guid("2fb2c391-0bc6-40a9-b85c-d5196864762e"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8879)
                },
                new Skill
                {
                    Id = new Guid("933E4168-7B24-4045-B144-5297A4DAF851"),
                    Name = "Java",
                    CategoryId = new Guid("5e7dcdbc-bdea-4805-b454-a8761823e1c9"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8882)
                },
                new Skill
                {
                    Id = new Guid("392012D0-C131-4377-AC84-B2362836FB27"),
                    Name = "CSharp",
                    CategoryId = new Guid("5e7dcdbc-bdea-4805-b454-a8761823e1c9"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8885)
                },
                new Skill
                {
                    Id = new Guid("6CB033F9-F405-44F4-B262-2CB393004309"),
                    Name = "VB.NET",
                    CategoryId = new Guid("5e7dcdbc-bdea-4805-b454-a8761823e1c9"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8887)
                },
                 new Skill
                 {
                     Id = new Guid("CD037D12-7FEB-46FD-BF24-2489080FB39B"),
                     Name = "AWS",
                     CategoryId = new Guid("c6f9b020-bebf-4f5a-b880-201e12ab6b26"),
                     AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8890)
                 },
                new Skill
                {
                    Id = new Guid("E398C01E-B30D-4AEA-9BF3-85A0D7F7FF9A"),
                    Name = "Azure",
                    CategoryId = new Guid("c6f9b020-bebf-4f5a-b880-201e12ab6b26"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8892)
                },
                new Skill
                {
                    Id = new Guid("3C27B4EE-3273-41A5-A8E8-33AFE6690D54"),
                    Name = "Terraform",
                    CategoryId = new Guid("c6f9b020-bebf-4f5a-b880-201e12ab6b26"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8895)
                },
                new Skill
                {
                    Id = new Guid("08E5A731-C969-488C-8526-1ACB2EE27254"),
                    Name = "Apex",
                    CategoryId = new Guid("5cce7a1e-34ae-4f2b-ab0b-415519a8b908"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8898)
                },
                new Skill
                {
                    Id = new Guid("FEA067F2-A2C8-49B3-8746-38C1B184ECED"),
                    Name = "MS Dynamics CRM",
                    CategoryId = new Guid("5cce7a1e-34ae-4f2b-ab0b-415519a8b908"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8900)
                },
                new Skill
                {
                    Id = new Guid("CDC6D211-4822-46B6-A31B-A859D4A77CEA"),
                    Name = "SAP CRM",
                    CategoryId = new Guid("5cce7a1e-34ae-4f2b-ab0b-415519a8b908"),
                    AddedOn = new DateTime(2023, 6, 20, 11, 20, 12, 18, DateTimeKind.Utc).AddTicks(8903)
                }
            );
        }
    }
}
