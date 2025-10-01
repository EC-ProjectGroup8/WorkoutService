using Data.Contexts;
using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract partial class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _table = context.Set<TEntity>();

    public virtual async Task<RepositoryResult> AddAsync(TEntity entity)
    {
        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.ToListAsync();
            return new RepositoryResult<IEnumerable<TEntity>> { Success = true, Result = entities };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<TEntity>>
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public virtual async Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _table.FirstOrDefaultAsync(expression);
            if (entity == null)
                throw new Exception("Not Found");

            return new RepositoryResult<TEntity?> { Success = true, Result = entity };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<TEntity?>
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetManyAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entities = await _table.Where(expression).ToListAsync();

            return new RepositoryResult<IEnumerable<TEntity>> 
            { 
                Success = true, 
                Result = entities 
            };

        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<TEntity>>
            { 
                Success = false,
                Error = ex.Message
            };
        }
    }

    public virtual async Task<RepositoryResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public virtual async Task<RepositoryResult> DeleteAsync(TEntity entity)
    {
        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }
}
