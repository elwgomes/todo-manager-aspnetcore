using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Todo> Todos { get; }
    
    // unit of work
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}