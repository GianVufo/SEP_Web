using Microsoft.EntityFrameworkCore;
using SEP_Web.Models;

namespace SEP_Web.Database;
public class SEP_WebContext : DbContext
{
    public SEP_WebContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Environment.GetEnvironmentVariable("SEP_WEB_CONNECTION_STRING");

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        if (string.IsNullOrEmpty(connectionString)) throw new Exception("Não foi possível encontrar uma string de conexão devidamente configurada!");

        optionsBuilder.UseMySql(connectionString, serverVersion);
    }

    public DbSet<UserAdministrator> Administrator => Set<UserAdministrator>();
    public DbSet<Division> Division => Set<Division>();
    public DbSet<UserEvaluator> Evaluator => Set<UserEvaluator>();
    public DbSet<Instituition> Instituition => Set<Instituition>();
    public DbSet<Section> Section => Set<Section>();
    public DbSet<Sector> Sector => Set<Sector>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Adiciona um índice exclusivo para as propriedades que não podem ser duplicadas em um novo regtistro 'UserAdministrator'
        modelBuilder.Entity<UserAdministrator>().HasIndex(u => u.Masp).IsUnique();
        modelBuilder.Entity<UserAdministrator>().HasIndex(u => u.Name).IsUnique();
        modelBuilder.Entity<UserAdministrator>().HasIndex(u => u.Login).IsUnique();
        modelBuilder.Entity<UserAdministrator>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<UserAdministrator>().HasIndex(u => u.Phone).IsUnique();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SEP_WebContext).Assembly);
    }

}