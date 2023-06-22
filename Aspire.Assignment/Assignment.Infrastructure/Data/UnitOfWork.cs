using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Repositories;
using Assignment.Core.Data.Repositories;
using Assignment.Infrastructure.Data.Repositories;
using Assignment.Migrations;

namespace Assignment.Core.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public IAppRepository App => new AppRepository(_context);
        public IUserRepository User => new UserRepository(_context);
        public IProductRepository Product => new ProductRepository(_context);
        public IRoleRepository Role=>new RoleRepository(_context);
        public IOperationRepository Operation=> new OperationRepository(_context);
        public ICategoryRepository Category=> new CategoryRepository(_context);
        public ISkillRepository Skill => new SkillRepository(_context);
        public ISkillSetRepository SkillSet=> new SkillSetRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}