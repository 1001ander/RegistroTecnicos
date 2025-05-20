using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicosService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;

    public TecnicosService(IDbContextFactory<Contexto> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (tecnico.TecnicoId == 0)
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    public async Task<bool> Existe(int tecnicosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId == tecnicosId);
    }

    private async Task<bool> Insertar(Tecnicos tecnicos)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Add(tecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnicos)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Update(tecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Tecnicos?> Buscar(int tecnicosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == tecnicosId);
    }

    public async Task<bool> Eliminar(int tecnicosId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .Where(t => t.TecnicoId == tecnicosId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExisteTecnico(int tecnicosId, string nombres)
    {
        if (string.IsNullOrWhiteSpace(nombres))
            return false;

        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId != tecnicosId &&
                          t.Nombres.ToLower() == nombres.ToLower());
    }
}