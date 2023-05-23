using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Core.Data.Repositories;
using Assignment.Infrastructure.Data.Repositories;
using Assignment.Migrations;
using Microsoft.EntityFrameworkCore;

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
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.User.Include(r => r.Role).Include(o=>o.Operation).AsEnumerable();
        }
    }
}