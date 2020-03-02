using Microsoft.EntityFrameworkCore;
using Personaas.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personaas.DAL
{
    public class Context : DbContext
    {
        public DbSet<Personas> Personas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Personaas.db");
        }



    }
}
