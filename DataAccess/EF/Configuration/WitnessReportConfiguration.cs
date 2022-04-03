using Common.Models.WitnessReport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EF.Configuration
{
    public class WitnessReportConfiguration : IEntityTypeConfiguration<WitnessReport>
    {
        public void Configure(EntityTypeBuilder<WitnessReport> builder)
        {
            builder.ToTable("WitnessReport");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PhoneNumber).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CaseName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Country).HasMaxLength(100);
            builder.Property(x => x.Region).HasMaxLength(100);
            builder.Property(x => x.IPAddress).HasMaxLength(100);
        }
    }
}
