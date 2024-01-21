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

    private IEnumerable<Employee> GetAll()
    {
        return _context.Employees;
    }

    public int GetEmploymentRateForYear(int year)
    {
        return GetAll().Where(employee => employee.EmployedOn.Year == year).Count();
    }

    public bool HasActiveVacation(Employee employee)
    {
        var vacations = _context.Vacations.Where(vacation => vacation.EmployeeId == employee.Id);

        if (vacations.Any())
        {
            foreach (var vacation in vacations)
            {
                if (vacation.ToDate >= DateTime.Now)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
