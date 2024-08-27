using ApiPractice.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiPractice.DAL.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> builder)
        {
            builder.Property(b=>b.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(b => b.PageCount)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
