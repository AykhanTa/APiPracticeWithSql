using ApiPractice.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPractice.DAL.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a=>a.Name)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(a => a.Surname)
               .IsRequired()
               .HasMaxLength(50);
        }
    }
}
