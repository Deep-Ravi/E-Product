using Assignment.Contracts.Data.Entities;

namespace Assignment.Contracts.Data.Repositories
{
    public interface ISkillSetRepository : IRepository<SkillSet>
    {
        IEnumerable<SkillSet> GetAllDeveloperSkillSet(int userId);
        IEnumerable<SkillSet> GetAllApprovalSkillSet();
        IEnumerable<SkillSet> GetNotifyNewSkillSet();
    }
}
