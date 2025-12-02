using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EventTask.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class EventTaskDbContextFactory : IDesignTimeDbContextFactory<EventTaskDbContext>
{
    public EventTaskDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        EventTaskEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<EventTaskDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new EventTaskDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EventTask.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
