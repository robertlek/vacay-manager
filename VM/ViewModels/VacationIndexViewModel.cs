#nullable disable warnings

using VM.Models;

namespace VM.ViewModels
{
    public class VacationIndexViewModel
    {
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Vacation> Vacations { get; set; }
    }
}
