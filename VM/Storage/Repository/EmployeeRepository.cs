using VM.Models;
using VM.Storage.DataAccess;
using VM.Storage.Repository.IRepository;

namespace VM.Storage.Repository;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly Context _context;

    public EmployeeRepository(Context context) : base(context)
    {
        _context = context;
    }
}
