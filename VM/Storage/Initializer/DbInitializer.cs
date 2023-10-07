using VM.Models;
using VM.Environment;
using VM.Storage.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace VM.Storage.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly Context _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(
        Context context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        try
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }
        }
        catch
        {
            throw new Exception("Error at migrating the pending mirations!");
        }

        string email = "lecarobertandrei@gmail.com";

        try
        {
            var department = _context.Departments.FirstOrDefault(department =>
                department.Name.Equals("Administration"));

            if (department is null)
            {
                _context.Departments.Add(new Department { Name = "Administration", Overlaps = 1 });
                _context.SaveChanges();

                department = _context.Departments.First(department =>
                    department.Name.Equals("Administration"));
            }

            if (!_roleManager.RoleExistsAsync(Roles.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Employee)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new Employee
                {
                    UserName = email,
                    Email = email,
                    PhoneNumber = "0747577171",
                    DepartmentId = department.Id,
                    EmployedOn = DateTime.Now,
                    FirstName = "Robert",
                    LastName = "Leca"
                }, "Parola1.").GetAwaiter().GetResult();

                var user = _context.Employees.First(user => user.UserName.Equals(email));
                _userManager.AddToRoleAsync(user, Roles.Admin).GetAwaiter().GetResult();
            }
        }
        catch
        {
            throw new Exception("Error at creating the initial entities!");
        }
    }
}
