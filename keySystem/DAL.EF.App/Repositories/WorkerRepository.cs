﻿using DAL.Contracts.App;
using DAL.EF.Base;
using Domain;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class WorkerRepository :
    EFBaseRepository<Worker, ApplicationDbContext>, IWorkerRepository
{
    public WorkerRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Worker>> AllAsync()
    {
        return await RepositoryDbSet
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<Worker>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public override async Task<Worker?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .OrderBy(t => t.Id)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public virtual async Task<Worker?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Worker?> RemoveAsync(Guid id, Guid userId)
    {
        var part = await FindAsync(id, userId);
        return part == null ? null : Remove(part);
    }
}