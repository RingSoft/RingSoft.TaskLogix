using Microsoft.EntityFrameworkCore;
using RingSoft.TaskLogix.DataAccess.Configurations;

namespace RingSoft.TaskLogix.DataAccess
{
    public static class DataAccessGlobals
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TlTaskConfiguration());
            modelBuilder.ApplyConfiguration(new TlTaskRecurDailyConfiguration());
            modelBuilder.ApplyConfiguration(new TlTaskRecurWeeklyConfiguration());
            modelBuilder.ApplyConfiguration(new TlTaskRecurMonthlyConfiguration());
        }
    }
}
