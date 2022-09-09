using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace proyectoApi.Models
{
    public class db_libreriaContext : DbContext
    {
        public db_libreriaContext(DbContextOptions<db_libreriaContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localhost);Database=db_libreria;User Id=usr_prueba;Password=123456;");
            }
        }

    }
}
