using bolsaBE.DBContexts;
using bolsaBE.Entities.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static bolsaBE.Data.GenericRepository.IGenericRepository;

namespace bolsaBE.Data.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        protected readonly BolsaDeTrabajoContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(BolsaDeTrabajoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.OrderBy(item => item.Order).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Add(T item)
        {
            int maxOrder = _dbSet.Max(i => i.Order);
            item.Order = ++maxOrder;
            _dbSet.Add(item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void Update(T item)
        {
            _dbSet.Update(item).State = EntityState.Modified;
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindAllByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<bool> ItemExists(Guid id)
        {
            return await _dbSet.FindAsync(id) is not null;
        }
    }
}
