using System.Linq.Expressions;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using NanoStarCTF.DataAccess.DbContexts;
using NanoStarCTF.Domain.Domain.Commons;
using NanoStarCTF.Domain.Enums;

#pragma warning disable
namespace NanoStarCTF.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : Auditble
{
    private readonly AppDbContext dbContext;
    private readonly DbSet<T> dbSet;

    public GenericRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<T>();
    }

    public async ValueTask<T> CreateAsync(T entity) =>
        (await dbContext.AddAsync(entity)).Entity;

    public async ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await GetAsync(expression);

        if (entity == null)
            return false;

        entity.State = ItemState.Deleted;
        Update(entity);
        await SaveChangesAsync();
        return true;
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null)
    {

        IQueryable<T> query = expression is null ? dbSet.Where(p => p.State != ItemState.Deleted) : dbSet.Where(p => p.State != ItemState.Deleted).Where(expression);

        if (includes != null)
            foreach (var include in includes)
                if (!string.IsNullOrEmpty(include))
                    query = query.Include(include);
        return query;
    }

    public async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null) 
        =>  await dbSet.Where(p => p.State != ItemState.Deleted).Where(expression).FirstOrDefaultAsync();

    public T Update(T entity) =>
        dbSet.Update(entity).Entity;

    public async ValueTask SaveChangesAsync() =>
       await dbContext.SaveChangesAsync();
}
