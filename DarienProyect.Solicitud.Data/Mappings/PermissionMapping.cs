using DarienProyect.Solicitud.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DarienProyect.Solicitud.Data.Mappings
{
    public class PermissionMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {


            builder.ToTable("Permission");

            builder.HasKey(x => x.ID);
            //builder.Property(x => x.ID).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();

            builder.Property(x => x.DatePermission).IsRequired().HasDefaultValue(DateTime.Now);

            builder.HasOne(x => x.TypePermission)
                  .WithMany(x => x.Permissions)
                  .HasForeignKey(x => x.TypePermissionId)
                  .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
