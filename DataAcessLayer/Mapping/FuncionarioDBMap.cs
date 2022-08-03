﻿using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcessLayer.Mapping
{
    internal class FuncionarioDBMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("FUNCIONARIOS");
            builder.Property(f => f.Nome).HasMaxLength(100).IsUnicode(false);
        }
    }
}