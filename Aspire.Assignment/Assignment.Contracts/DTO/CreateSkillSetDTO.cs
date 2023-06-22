namespace Assignment.Contracts.DTO
{
    public class CreateSkillSetDTO
    {
        public Guid CategoryId { get; set; }
        public Guid SkillId { get; set; }
        public string Proficiency { get; set; }
        public string YearsOfExperience { get; set; } //eg: Enter 0.5-1 or 1-1.5
        public string? Achievement { get; set; }
        public DateTime LastWorkedDate { get; set; }
        public string UserName { get; set; }
    }
}
