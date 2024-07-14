using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using BTLDB.Models;
using System.Configuration;

namespace BTLDB.Data;

/*
//leave it for other purposes
public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {

    }
}
*/

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public DbSet<SiPMArray> SiPMArrays { get; set; }
    public DbSet<Channel> Channels { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SiPMArray>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<SiPMArray>()
            .HasIndex(a => a.SN)
            .IsUnique();

        modelBuilder.Entity<Channel>()
            .HasOne(c => c.SiPMArray)
            .WithMany(a => a.Channels)
            .HasForeignKey(c => c.SiPMArrayId);

        base.OnModelCreating(modelBuilder);
    }
}