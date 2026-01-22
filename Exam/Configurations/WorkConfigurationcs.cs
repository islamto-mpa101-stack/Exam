using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Exam.Models;

namespace Exam.Configurations;

public class WorkConfiguration : IEntityTypeConfiguration<Exam.Models.Work>
{
  

    public void Configure(EntityTypeBuilder<Exam.Models.Work> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(180);
        builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(256);
               
    }

   
}
