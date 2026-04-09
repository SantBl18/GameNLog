using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class InvolvedCompany
    {
        public int GameId { get; set; }
        public int CompanyId { get; set; }
        public Game? Game { get; set; }
        public Company? Company { get; set; }
    }

    public class InvolvedCompanyConfiguration : IEntityTypeConfiguration<InvolvedCompany>
    {
        public void Configure(EntityTypeBuilder<InvolvedCompany> builder)
        {
            builder.ToTable("InvolvedCompany");
            builder.HasKey(ic => new { ic.GameId, ic.CompanyId });

            builder.HasOne(ic => ic.Game)
                .WithMany(g => g.InvolvedCompanies)
                .HasForeignKey(ic => ic.GameId);

            builder.HasOne(ic => ic.Company)
                .WithMany(c => c.InvolvedCompanies)
                .HasForeignKey(ic => ic.CompanyId);
        }
    }
}
