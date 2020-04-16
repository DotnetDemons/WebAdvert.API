using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.API.Services
{
    public class SQLiteDBContext : DbContext
    {
        public DbSet<AdvertDBModel> AdvertDBModel { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=advertdb.db");
    }
}
