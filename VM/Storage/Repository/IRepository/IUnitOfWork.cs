namespace VM.Storage.Repository.IRepository;

public interface IUnitOfWork
{
    IDepartmentRepository Department { get; }

    void Save();
}
