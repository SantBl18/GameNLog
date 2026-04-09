using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameNLog.Models
{
    public class Company
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
        public ICollection<InvolvedCompany> InvolvedCompanies{ get; } = [];

    }

    public class CompanyConfiguration : IEntityTypeConfiguration<Company> 
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.HasMany(c => c.InvolvedCompanies)
                .WithOne(ic => ic.Company);
        }
    }
}
