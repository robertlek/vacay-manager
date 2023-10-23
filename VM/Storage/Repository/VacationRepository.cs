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
}
