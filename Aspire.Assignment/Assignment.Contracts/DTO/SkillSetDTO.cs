using Assignment.Contracts.Data.Entities;

namespace Assignment.Contracts.DTO
{
    public class SkillSetDTO
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid SkillId { get; set; }
        public string SkillName { get; set; }
        public string Proficiency { get; set; }
        public string YearsOfExperience { get; set; }
        public string? Achievement { get; set; }
        public DateTime LastWorkedDate { get; set; }
        public string ApprovalStatus { get; set; }
        public bool IsSendForApproval { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
