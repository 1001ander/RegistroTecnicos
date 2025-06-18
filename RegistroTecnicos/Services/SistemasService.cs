using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class SistemasService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Sistemas sistema)
    {
        if (!await Existe(sistema.SistemaId))
            return await Insertar(sistema);
        else
            return await Modificar(sistema);
    }

    private async Task<bool> Existe(int sistemaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .AnyAsync(s => s.SistemaId == sistemaId);
    }

    private async Task<bool> Insertar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Sistemas.Add(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Sistemas?> Buscar(int sistemaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .FirstOrDefaultAsync(s => s.SistemaId == sistemaId);
    }

    public async Task<bool> Eliminar(int sistemaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .Where(s => s.SistemaId == sistemaId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .Where(criterio)
            .OrderBy(s => s.Descripcion)
            .ToListAsync();
    }

    public async Task<List<Sistemas>> ListarTodos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .OrderBy(s => s.Descripcion)
            .ToListAsync();
    }

    public async Task<bool> ExisteSistema(int sistemaId, string descripcion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .AnyAsync(s => s.SistemaId != sistemaId &&
                s.Descripcion.ToLower().Equals(descripcion.ToLower()));
    }

    public async Task<List<Sistemas>> FiltrarPorComplejidad(string complejidad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var query = contexto.Sistemas.AsQueryable();

        if (!string.IsNullOrEmpty(complejidad))
        {
            query = query.Where(s => s.Complejidad.ToLower().Contains(complejidad.ToLower()));
        }

        return await query.OrderBy(s => s.Descripcion).ToListAsync();
    }

    public async Task<List<Sistemas>> Filtrar(string filtro, string valor)
    {
        Expression<Func<Sistemas, bool>> criterio = s => true;

        if (filtro == "Descripcion" && !string.IsNullOrEmpty(valor))
        {
            criterio = s => s.Descripcion.ToLower().Contains(valor.ToLower());
        }
        else if (filtro == "Complejidad" && !string.IsNullOrEmpty(valor))
        {
            criterio = s => s.Complejidad.ToLower().Contains(valor.ToLower());
        }

        return await Listar(criterio);
    }
}