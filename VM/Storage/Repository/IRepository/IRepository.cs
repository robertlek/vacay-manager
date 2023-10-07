using System.Linq.Expressions;

namespace VM.Storage.Repository.IRepository;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    T? Get(Expression<Func<T, bool>> filter, string? properties = null);
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? properties = null);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}
