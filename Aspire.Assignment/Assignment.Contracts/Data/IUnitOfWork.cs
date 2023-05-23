using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data.Repositories;

namespace Assignment.Contracts.Data
{
    public interface IUnitOfWork
    {
        IAppRepository App { get; }
        IUserRepository User { get; }
        IProductRepository Product { get; }
        IRoleRepository Role { get; }
        IOperationRepository Operation { get; }
        Task CommitAsync();
        IEnumerable<User> GetAllUsers();
    }
}