using VM.Models;
using VM.Storage.DataAccess;
using VM.Storage.Repository.IRepository;

namespace VM.Storage.Repository;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    private readonly Context _context;

    public DepartmentRepository(Context context) : base(context)
    {
        _context = context;
    }
}
