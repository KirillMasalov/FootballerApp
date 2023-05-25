using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballerApp.Model.Footballers;
using Microsoft.EntityFrameworkCore;

namespace FootballerApp.Model
{
    public class SportsmanDbContext: DbContext
    {
        public DbSet<Footballer> Footballers { get; set; }

        public SportsmanDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=db.db");
        }
    }
}
