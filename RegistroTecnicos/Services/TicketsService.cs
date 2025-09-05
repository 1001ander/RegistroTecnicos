using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TicketsService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Tickets ticket)
    {
        if (!await Existe(ticket.TicketId))
            return await Insertar(ticket);
        else
            return await Modificar(ticket);
    }
    private async Task<bool> Existe(int ticketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .AnyAsync(t => t.TicketId == ticketId);
    }
    private async Task<bool> Insertar(Tickets ticket)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tickets.Add(ticket);
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(Tickets ticket)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(ticket);
        return await contexto.SaveChangesAsync() > 0;

    }
    public async Task<Tickets?> Buscar(int ticketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .FirstOrDefaultAsync(t => t.TicketId == ticketId);
    }
    public async Task<bool> Eliminar(int ticketId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .Where(t => t.TicketId == ticketId)
            .ExecuteDeleteAsync() > 0;
    }
    public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .Where(criterio)
            .OrderByDescending(t => t.Fecha)
            .ToListAsync();
    }
    public async Task<List<Tickets>> ListarTodos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .OrderByDescending(t => t.Fecha)
            .ToListAsync();
    }
    public async Task<bool> ExisteTicket(int ticketId, string asunto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tickets
            .AnyAsync(t => t.TicketId != ticketId &&
                t.Asunto.ToLower().Equals(asunto.ToLower()));
    }
    public async Task<List<Tickets>> FiltrarPorFecha(DateTime? desde, DateTime? hasta)
    {
 
       await using var contexto = await DbFactory.CreateDbContextAsync();

        var query = contexto.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .AsQueryable();

        if (desde.HasValue && hasta.HasValue)
        {
            query = query.Where(t => t.Fecha.Date >= desde.Value.Date &&
                                   t.Fecha.Date <= hasta.Value.Date);
        }
        else if (desde.HasValue)
        {
            query = query.Where(t => t.Fecha.Date >= desde.Value.Date);
        }
        else if (hasta.HasValue)
        {
            query = query.Where(t => t.Fecha.Date <= hasta.Value.Date);
        }

        return await query.OrderByDescending(t => t.Fecha).ToListAsync();
    }
   
    public async Task<List<Tickets>> Filtrar(string filtro, DateTime? desde, DateTime? hasta)
    {
        Expression<Func<Tickets, bool>> criterio = t => true;

        if (filtro == "Fecha" && desde.HasValue && hasta.HasValue)
        {
            criterio = t => t.Fecha.Date >= desde.Value.Date &&
                           t.Fecha.Date <= hasta.Value.Date;
        }
        

        return await Listar(criterio);
    }
}