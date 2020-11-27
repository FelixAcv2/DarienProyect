
using Acv2.SharedKernel.Infraestructure;
using DarienProyect.Solicitud.Data.Mappings;
using DarienProyect.Solicitud.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DarienProyect.Solicitud.Data
{
    public class SolicitudDbContext : SharedDbContext
    {

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<TypePermission> TypePermissions { get; set; }

        public SolicitudDbContext(IConfiguration config, DbContextOptions<SharedDbContext> options) : base(config, options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TypePermissionMapping());

            modelBuilder.ApplyConfiguration(new PermissionMapping());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("LocalConnection"), options =>
            {
               
                options.MigrationsHistoryTable("_SolicitudMigrationsHistory");
                options.MigrationsAssembly("DarienProyect.Solicitud.Data");

            });


        }


    }
}
