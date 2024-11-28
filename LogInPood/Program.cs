using Autopood.Data;
using Autopood.ServiceInterface;
using Autopood.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFilesServices, FilesServices>();
builder.Services.AddDbContext<AutopoodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AutopoodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IFilesServices, FilesServices>();
builder.Services.AddScoped<ICarsServices, CarsServices>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AutopoodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IFilesServices, FilesServices>();
builder.Services.AddScoped<IPlanesServices, PlanesServices>();

// Register the UserService with dependency injection
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
