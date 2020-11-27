using DarienProyect.Solicitud.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace DarienProyect.Solicitud.Data.Mappings
{
    public class TypePermissionMapping : IEntityTypeConfiguration<TypePermission>
    {
        public void Configure(EntityTypeBuilder<TypePermission> builder)
        {
            builder.ToTable("TypePermission");

            builder.HasKey(x => x.ID);
            //builder.Property(x => x.ID).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
            builder.Property(x => x.Description).HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.Permissions)
                     .WithOne(x => x.TypePermission)
                     .HasForeignKey(x => x.TypePermissionId).OnDelete(DeleteBehavior.Restrict);




           /// builder.HasData(new TypePermission { Description = "Enfermedad" },new TypePermission {Description= "Diligencias" },new TypePermission {Description="Capacitacion"});

        }
    }
}
