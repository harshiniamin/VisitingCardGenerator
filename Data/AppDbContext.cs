using Microsoft.EntityFrameworkCore;
using Visitingcardgenerator.Models;

namespace Visitingcardgenerator.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Banks> Banks { get; set; }
        public DbSet<CustomerInfo> CustomerInfo { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<UploadHistory> UploadHistory { get; set; }

    }
}