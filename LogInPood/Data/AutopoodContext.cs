﻿using Autopood.Domain;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Autopood.Data
{
    public class AutopoodContext : DbContext
    {
        public AutopoodContext(DbContextOptions<AutopoodContext> options) : base(options) { }

        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<FileToApi> FilesToApi { get; set; }
        public DbSet<Plane> Planes { get; set; }
    }
}
