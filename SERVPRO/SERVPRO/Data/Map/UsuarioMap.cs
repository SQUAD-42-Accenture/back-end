﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SERVPRO.Models;
using System.Reflection.Emit;

namespace SERVPRO.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.CPF);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Senha).IsRequired().HasMaxLength(255);
            builder.Property(x => x.TipoUsuario).IsRequired();

            builder.HasDiscriminator<string>("TipoUsuario")
              .HasValue<Cliente>("Cliente")
              .HasValue<Tecnico>("Tecnico")
              .HasValue<Administrador>("Administrador");

            //builder.Property(x => x.TipoUsuario).IsRequired();





        }
    }
}
