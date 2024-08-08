using APiPracticeSql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPractice.DAL.Configurations
{ 
    internal class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.Name).IsRequired().HasMaxLength(10);
            builder.Property(g=>g.Limit).IsRequired().HasMaxLength(10);
            builder.Property(g => g.CreateDate).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(g => g.Image).IsRequired();
        }
    }
}
