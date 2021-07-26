using System.Threading.Tasks;
using Shop.DAL.Core.Entities;
using Shop.DAL.Core.Repositories.Interfaces;

namespace Shop.DAL.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }
        public Task SaveChanges();
    }
}