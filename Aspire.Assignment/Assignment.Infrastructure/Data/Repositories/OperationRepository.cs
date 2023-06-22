using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;
using Assignment.Infrastructure.Data.Repositories.Generic;
using Assignment.Migrations;

namespace Assignment.Infrastructure.Data.Repositories
{
    public class OperationRepository : Repository<Operation>, IOperationRepository
    {
        public OperationRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
