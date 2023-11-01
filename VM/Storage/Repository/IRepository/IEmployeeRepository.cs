using VM.Models;

namespace VM.Storage.Repository.IRepository;

public interface IEmployeeRepository : IRepository<Employee>
{
    bool HasActiveVacation(Employee employee);
}
