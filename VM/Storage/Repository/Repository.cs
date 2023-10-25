using VM.Storage.DataAccess;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VM.Storage.Repository.IRepository;

namespace VM.Storage.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly Context _context;
    private readonly DbSet<T> _db;

    public Repository(Context context)
    {
        _context = context;
        _db = _context.Set<T>();
    }

    public void Add(T entity)
    {
        _db.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter, string? properties = null)
    {
        IQueryable<T> query = _db.Where(filter);

        if (properties is not null)
        {
            foreach (var property in properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        return query.First();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? properties = null)
    {
        IQueryable<T> query = _db;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (properties is not null)
        {
            foreach (var property in properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        return query.ToList();
    }

    public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? properties = null)
    {
        IQueryable<T> query = _db.Where(filter);

        if (properties is not null)
        {
            foreach (var property in properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        return query.FirstOrDefault();
    }

    public void Remove(T entity)
    {
        _db.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _db.RemoveRange(entities);
    }

    public void Update(T entity)
    {
        _db.Update(entity);
    }
}
