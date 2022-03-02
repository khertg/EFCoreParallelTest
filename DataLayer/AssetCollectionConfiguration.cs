using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer
{
    public class AssetCollectionConfiguration : IEntityTypeConfiguration<AssetCollection>
    {
        public void Configure(EntityTypeBuilder<AssetCollection> builder)
        {
            builder.ToTable("AssetCollection");

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Desc)
                .HasColumnName("Desc")
                .IsUnicode(false);
        }
    }
}
