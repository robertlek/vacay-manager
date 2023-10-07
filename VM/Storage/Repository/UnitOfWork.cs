using VM.Storage.DataAccess;
using VM.Storage.Repository.IRepository;

namespace VM.Storage.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;

    public UnitOfWork(Context context)
    {
        _context = context;

        Department = new DepartmentRepository(_context);
        Employee = new EmployeeRepository(_context);
    }

    public IDepartmentRepository Department { get; private set; }
    public IEmployeeRepository Employee { get; private set; }

    public void Save()
    {
        _context.SaveChanges();
    }
}
