using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities
{
    public class Skill:BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
