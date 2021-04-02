using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockWatchRestApi.Models;

namespace StockWatchRestApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Price> Prices { get; set; }
    }
}
