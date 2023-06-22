using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Infrastructure.Data.Repositories.Generic;
using Assignment.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Core.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<User> _dbSet;
        public UserRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _dbSet.Include(r => r.Role).Include(o => o.Operation).AsEnumerable();
        }
    }
}