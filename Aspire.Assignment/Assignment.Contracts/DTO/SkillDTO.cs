using Assignment.Contracts.Data.Entities;

namespace Assignment.Contracts.DTO
{
    public class SkillDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
