using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace EventTask.Events.Entities;

public class TestEvent : AuditedAggregateRoot<Guid>
{
    public Dictionary<string, string> Name { get; private set; }
    public DateTime StartDate { get; private set; }

    public TestEvent(Guid id, Dictionary<string, string> name, DateTime startDate) : base(id)
    {
        Name = name ?? new Dictionary<string, string>();
        StartDate = startDate;
    }

    private TestEvent() { Name = new Dictionary<string, string>(); }

    public void UpdateName(Dictionary<string, string> newName)
    {
        Name = newName ?? throw new ArgumentNullException(nameof(newName));
    }

    public void UpdateStartDate(DateTime newStartDate)
    {
        StartDate = newStartDate;
    }

    public void SetNameTranslation(string cultureCode, string value)
    {
        if (Name.ContainsKey(cultureCode))
        {
            Name[cultureCode] = value;
        }
        else
        {
            Name.Add(cultureCode, value);
        }
    }

    public string GetName(string cultureCode, string defaultCulture = "en")
    {
        if (Name.TryGetValue(cultureCode, out var translation))
        {
            return translation;
        }
        if (Name.TryGetValue(defaultCulture, out var defaultTranslation))
        {
            return defaultTranslation;
        }
        return "[Translation Missing]";
    }
}

//var dictionaryConverter = new ValueConverter<Dictionary<string, string>, string>(
//            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
//            v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null) ?? new Dictionary<string, string>()
//        );

//        modelBuilder.Entity<Event>(entity =>
//        {
//            entity.HasKey(e => e.Id);
//            entity.Property(e => e.Name)
//                  .HasConversion(dictionaryConverter)
//                  .HasColumnType("nvarchar(max)");
//        });

//builder.Property(e => e.Name)
//               .HasColumnType("jsonb") // أو "json" أو "nvarchar(max)" حسب قاعدة البيانات
//               .HasConversion(

//                   new ValueConverter<Dictionary<string, string>, string>(
//                       v => System.Text.Json.JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
//                       v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null)
//                   )
//               );