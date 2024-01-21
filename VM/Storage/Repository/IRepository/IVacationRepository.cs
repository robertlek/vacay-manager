using VM.Models;

namespace VM.Storage.Repository.IRepository;

public interface IVacationRepository : IRepository<Vacation>
{
    Vacation GetActiveVacation(Employee employee);
    IEnumerable<Vacation> GetAll();
    int GetVacationsCountByMonth(int month);
}
