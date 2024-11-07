using System.Linq.Expressions;
using NanoStarCTF.Domain.Domain.Commons;
#pragma warning disable
namespace NanoStarCTF.DataAccess.Repositories;

public interface IGenericRepository<T>
{
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null);
    ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null);
    ValueTask<T> CreateAsync(T entity);
    ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression);
    T Update(T entity);
    public ValueTask SaveChangesAsync();
}