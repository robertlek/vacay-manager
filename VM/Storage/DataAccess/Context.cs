using VM.Models;
using Microsoft.EntityFrameworkCore;

namespace VM.Storage.DataAccess;

public class Context : DbContext
{
	public Context(DbContextOptions<Context> options) : base(options)
	{
	}

	public DbSet<Department> Departments { get; set; }
}
