using APiPracticeSql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPractice.DAL.Configurations
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.Name).IsRequired().HasMaxLength(20);
            builder
                .HasOne(s=>s.Group)
                .WithMany(g=>g.Students)
                .HasForeignKey(s=>s.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(s => s.CreateDate).IsRequired().HasDefaultValueSql("getdate()");
        }
    }
}
