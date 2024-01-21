using VM.Models;
using VM.Storage.Repository.IRepository;

namespace VM.Services;

public class YearlyVacationCleanup : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public YearlyVacationCleanup(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            PerformVacationCleanup();

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }

    private void PerformVacationCleanup()
    {
        using var scope = _serviceProvider.CreateScope();

        IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        DateTime currentDate = DateTime.Now;

        IEnumerable<Vacation> vacations = unitOfWork.Vacation.GetAll().Where(vacation =>
            vacation.CreatedOn.Year < currentDate.Year &&
            vacation.ToDate < currentDate);

        unitOfWork.Vacation.RemoveRange(vacations);
        unitOfWork.Save();
    }
}
