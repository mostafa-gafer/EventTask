using EventTask.EventRegistrations;
using EventTask.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTask.EntityFrameworkCore.Configurations;

public class EventRegistrationConfiguration : IEntityTypeConfiguration<EventRegistration>
{
    public void Configure(EntityTypeBuilder<EventRegistration> builder)
    {
        builder.ToTable(EventTaskConsts.DbTablePrefix + "EventRegistrations",
                EventTaskConsts.DbSchema);

        builder.HasKey(x => x.Id);

        builder.HasOne(o => o.User)
               .WithMany(o => o.Registrations)
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(o => o.Event)
               .WithMany(o => o.Registrations)
               .HasForeignKey(o => o.EventId)
               .OnDelete(DeleteBehavior.NoAction);

        // Make UserId + EventId unique
        builder.HasIndex(x => new { x.UserId, x.EventId })
               .IsUnique();
    }
}
