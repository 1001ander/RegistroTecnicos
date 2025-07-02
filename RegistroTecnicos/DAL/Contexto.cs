using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Tickets> Tickets { get; set; }
    public DbSet<Sistemas> Sistemas { get; set; }
    public DbSet<Ventas> Ventas { get; set; }

    public DbSet<VentasDetalle> VentasDetalle { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tickets>()
            .HasOne(t => t.Cliente)
            .WithMany()
            .HasForeignKey(t => t.ClienteId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<Tickets>()
            .HasOne(t => t.Tecnico)
            .WithMany()
            .HasForeignKey(t => t.TecnicoId)
            .OnDelete(DeleteBehavior.Restrict);
    }


}
