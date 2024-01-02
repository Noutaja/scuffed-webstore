using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Framework.src.Database;
public class TimestampInterceptor : SaveChangesInterceptor
{
    private static readonly Lazy<TimestampInterceptor> lazyInstance = new Lazy<TimestampInterceptor>(() => new TimestampInterceptor());

    public static TimestampInterceptor Instance => lazyInstance.Value;

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken token)
    {
        IEnumerable<EntityEntry>? changedData = eventData.Context.ChangeTracker.Entries();
        IEnumerable<EntityEntry>? updatedEntries = changedData.Where(entity => entity.State == EntityState.Modified);
        IEnumerable<EntityEntry>? addedEntries = changedData.Where(entity => entity.State == EntityState.Added);

        foreach (var e in updatedEntries)
        {
            if (e.Entity is BaseEntity entity)
            {
                entity.UpdatedAt = DateTime.Now;
            }
        }

        foreach (var e in addedEntries)
        {
            if (e.Entity is BaseEntity entity)
            {
                entity.UpdatedAt = DateTime.Now;
                entity.CreatedAt = DateTime.Now;
            }
        }
        return base.SavingChangesAsync(eventData, result);
    }
}
