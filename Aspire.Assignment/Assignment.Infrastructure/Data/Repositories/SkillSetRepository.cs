using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Infrastructure.Data.Repositories.Generic;
using Assignment.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Data.Repositories
{
    public class SkillSetRepository : Repository<SkillSet>, ISkillSetRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<SkillSet> _dbSet;
        public SkillSetRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<SkillSet>();
        }
        public IEnumerable<SkillSet> GetAllDeveloperSkillSet(int userId)
        {
            return _dbSet.AsQueryable().Where(s=>s.UserId==userId).Include(s => s.User).Include(s => s.Category).Include(s => s.Skill).ToList();
        }
        public IEnumerable<SkillSet> GetAllApprovalSkillSet()
        {
            return _dbSet.AsQueryable().Where(s => s.IsSendForApproval == true && s.ApprovalStatus == "APPROVAL PENDING")
                .Include(s=>s.User).Include(s=>s.Category).Include(s=>s.Skill).ToList();
        }
        public IEnumerable<SkillSet> GetNotifyNewSkillSet()
        {
            return _dbSet.AsQueryable().Where(s => s.IsNotified == false && s.ApprovalStatus == "APPROVAL PENDING" && s.IsSendForApproval == true).ToList();
        }
    }
}
