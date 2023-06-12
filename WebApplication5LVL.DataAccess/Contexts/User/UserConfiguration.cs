using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication5LVL.DataAccess.Contexts.User
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Models.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SFL)
                .HasMaxLength(165);

            builder.Property(u => u.birthDay)
                .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));
        }
    }
}
