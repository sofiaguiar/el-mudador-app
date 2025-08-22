using Microsoft.EntityFrameworkCore;
using ElMudador.Api.Domain.Entities;

namespace ElMudador.Api.Infrastructure;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) {}
    public DbSet<Cliente> Clientes => Set<Cliente>();
}

