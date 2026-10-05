

using System.Linq.Expressions;

namespace DulcesPro.Repository
{
    public interface IDulcesRepository<TI>
    {
        Task<IEnumerable<TI>> Get();
        Task<TI?> GetByID(int id);
        Task Insert(TI tdulces);
        void Delete(TI tdulces);
        void Update(TI tdulces);
        Task<IEnumerable<TI>> Search(Expression<Func<TI, bool>> filter);
        Task Save();
    }
}
