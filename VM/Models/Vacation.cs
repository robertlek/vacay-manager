#nullable disable warnings

namespace VM.Models;

public class Vacation
{
    public int Id { get; set; }
    public string EmployeeId { get; set; }
    public int DepartmentId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DateTime CreatedOn { get; set; }
}
