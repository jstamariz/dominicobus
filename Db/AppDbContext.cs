using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DominicoBus.Models;
using Microsoft.EntityFrameworkCore;

namespace DominicoBus.Db
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Bus> Busses => Set<Bus>();
        public DbSet<DominicoBus.Models.Route> Routes => Set<DominicoBus.Models.Route>();
        public DbSet<Stop> Stops => Set<Stop>();
    }
}