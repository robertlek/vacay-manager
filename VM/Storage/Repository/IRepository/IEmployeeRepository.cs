using VM.Models;

namespace VM.Storage.Repository.IRepository;

public interface IEmployeeRepository : IRepository<Employee>
{
    int GetEmploymentRateForYear(int year);
    bool HasActiveVacation(Employee employee);
}
