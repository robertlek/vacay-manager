#nullable disable warnings

using VM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VM.ViewModels;

public class VacationInsertViewModel
{
    [ValidateNever]
    public Department Department { get; set; }

    [ValidateNever]
    public Employee Employee { get; set; }

    public Vacation Vacation { get; set; }
}
