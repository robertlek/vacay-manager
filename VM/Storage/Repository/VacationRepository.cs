using VM.Models;
using VM.Storage.DataAccess;
using VM.Storage.Repository.IRepository;

namespace VM.Storage.Repository;

public class VacationRepository : Repository<Vacation>, IVacationRepository
{
    private readonly Context _context;

    public VacationRepository(Context context) : base(context)
    {
        _context = context;
    }

    public Vacation GetActiveVacation(Employee employee)
    {
        Vacation vacation = _context.Vacations.First(vacation =>
            vacation.EmployeeId == employee.Id &&
            vacation.ToDate >= DateTime.Now);

        return vacation;
    }

    public IEnumerable<Vacation> GetAll()
    {
        return _context.Vacations;
    }

    public int GetVacationsCountByMonth(int month)
    {
        return GetAll().Where(vacation => vacation.FromDate.Month == month).Count();
    }
}
