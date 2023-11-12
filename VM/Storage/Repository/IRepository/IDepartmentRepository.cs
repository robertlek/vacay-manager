using VM.Models;

namespace VM.Storage.Repository.IRepository;

public interface IDepartmentRepository : IRepository<Department>
{
    int GetActiveVacationsCount(int departmentId);
}
