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

    public int GetActiveVacationsCount(int departmentId)
    {
        Department? department = _context.Departments.FirstOrDefault(department =>
            department.Id == departmentId) ?? throw new Exception("The target department is invalid!");

        var vacations = _context.Vacations.Where(vacation =>
            vacation.DepartmentId == department.Id &&
            vacation.ToDate >= DateTime.Now);

        return vacations.Count();
    }
}
