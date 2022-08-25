﻿using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcessLayer.Mapping
{
    internal class EnderecoDBMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.Property(e => e.CEP).IsUnicode(false).IsRequired();
            builder.Property(e => e.Complemento).IsUnicode(false).IsRequired(false);
            builder.Property(e => e.Rua).IsUnicode(false).IsRequired();
            builder.Property(e => e.NumeroCasa).IsUnicode(false).IsRequired();
            builder.ToTable("ENDERECOS");
        }
    }
}