using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities
{
    public class SkillSet:BaseEntity
    {
        public Guid Id { get; set; }      
        [Column(TypeName = "nvarchar(15)")]
        public string Proficiency { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string YearsOfExperience { get; set; }
        public string? Achievement { get; set; }
        public DateTime LastWorkedDate { get; set; }
        [Column(TypeName = "nvarchar(25)")]
        public string ApprovalStatus { get; set; }
        public bool IsSendForApproval { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("Skill")]
        public Guid SkillId { get; set; }
        public virtual Skill Skill { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
