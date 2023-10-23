namespace VM.Storage.Repository.IRepository;

public interface IUnitOfWork
{
    IDepartmentRepository Department { get; }
    IEmployeeRepository Employee { get; }
    IVacationRepository Vacation { get; }

    void Save();
}
