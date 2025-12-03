using EventTask.Events;
using EventTask.Events.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTask.EntityFrameworkCore.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable(EventTaskConsts.DbTablePrefix + "Events",
                EventTaskConsts.DbSchema);

        builder.HasKey(x => x.Id);

        builder.HasOne(o => o.Organizer)
               .WithMany(o => o.Events)
               .HasForeignKey(o => o.OrganizerId);

        builder.Property(o => o.NameAr)
               .HasMaxLength(EventConsts.MaxNameLength)
               .IsRequired();

        builder.Property(o => o.NameEn)
               .HasMaxLength(EventConsts.MaxNameLength)
               .IsRequired();

        builder.Property(o => o.Location)
               .HasMaxLength(EventConsts.MaxLocationLength);

        builder.Property(o => o.Link)
               .HasMaxLength(EventConsts.MaxLinkLength);
    }
}
