using VM.Library;
using VM.Storage.DataAccess;
using VM.Storage.Repository;
using VM.Storage.Initializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VM.Storage.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    option => { option.MigrationsHistoryTable("Migrations", "ef"); }));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<Context>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

void SeedDatabase()
{
    using (var scoped = app.Services.CreateScope())
    {
        var dbInitializer = scoped.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Public/Home/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

SeedDatabase();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Public}/{controller=Home}/{action=Index}/{id?}");

app.Run();
