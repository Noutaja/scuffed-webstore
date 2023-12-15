using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Framework.src.Database;
public class TimestampInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
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
        return base.SavingChanges(eventData, result);
    }
}
