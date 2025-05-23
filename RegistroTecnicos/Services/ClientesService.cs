using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ClientesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Clientes clientes)
    {
        if (!await Existe(clientes.ClienteId))
            return await Insertar(clientes);
        else
            return await Modificar(clientes);
    }

    private async Task<bool> Existe(int clientesId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(c => c.ClienteId == clientesId);
    }

    private async Task<bool> Insertar(Clientes clientes)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(clientes);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes clientes)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(clientes);
        return await contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<Clientes?> Buscar(int clientesId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .FirstOrDefaultAsync(c => c.ClienteId == clientesId);
    }

    public async Task<bool> Eliminar(int clientesId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Where(c => c.ClienteId == clientesId)
            .ExecuteDeleteAsync() > 0;

    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.Include(t => t.Tecnicos)
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteCliente(int clientesId, string nombres)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(c => c.ClienteId != clientesId &&
                c.Nombres.ToLower().Equals(nombres.ToLower()));

    }
}