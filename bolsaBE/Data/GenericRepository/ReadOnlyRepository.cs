using bolsaBE.DBContexts;
using bolsaBE.Entities.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static bolsaBE.Data.GenericRepository.IGenericRepository;

namespace bolsaBE.Data.GenericRepository
{
    namespace bolsaBE.Data.GenericRepository
    {
        public class ReadOnlyRepository<T> : IReadRepository<T> where T : class, IEntity
        {
            private readonly BolsaDeTrabajoContext _dbContext;
            private readonly DbSet<T> _dbSet;

            public ReadOnlyRepository(BolsaDeTrabajoContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
                _dbSet = _dbContext.Set<T>();
            }
            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _dbSet.AsNoTracking().OrderBy(item => item.Id).ToListAsync();
            }

            public async Task<T?> GetByIdAsync(Guid id)
            {
                return await _dbSet.AsNoTracking().SingleOrDefaultAsync(item => item.Id == id);
            }

            public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            }

            public async Task<IEnumerable<T>> FindAllByConditionAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
            }

            public async Task<bool> ItemExists(Guid id)
            {
                return await _dbSet.AsNoTracking().SingleOrDefaultAsync(item => item.Id == id) is null;
            }
        }
    }
}
