using Autopood.Domain;
using Microsoft.EntityFrameworkCore;
using System.Xml;

public class AutopoodContext : DbContext
{
    public AutopoodContext(DbContextOptions<AutopoodContext> options) : base(options) { }

    public DbSet<FileToDatabase> FilesToDatabase { get; set; }
    public DbSet<Plane> Planes { get; set; }
    public DbSet<FileToApi> FilesToApi { get; set; }
}