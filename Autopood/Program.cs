using Microsoft.EntityFrameworkCore;
using Autopood.Data;
using Autopood.ServiceInterface;
using Autopood.Services;

using Autopood.ServiceInterface;
using Autopood.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPlanesServices, PlanesServices>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFilesServices, FilesServices>();
builder.Services.AddDbContext<AutopoodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AutopoodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IFilesServices, FilesServices>();
builder.Services.AddScoped<ICarsServices, CarsServices>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
