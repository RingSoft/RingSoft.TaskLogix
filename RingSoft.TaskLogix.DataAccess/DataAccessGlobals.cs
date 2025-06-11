﻿using Microsoft.EntityFrameworkCore;
using RingSoft.TaskLogix.DataAccess.Configurations;

namespace RingSoft.TaskLogix.DataAccess
{
    public static class DataAccessGlobals
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TlTaskConfiguration());
        }
    }
}
