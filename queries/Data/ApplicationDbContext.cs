using Microsoft.EntityFrameworkCore;
using queries.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace queries.Data
{
    internal class ApplicationDbContext : DbContext
    {
        private static readonly ApplicationDbContext _db;
        private readonly string _dbPath;

        public static ApplicationDbContext Instance { get => _db; }

        static ApplicationDbContext()
        {
            _db = new ApplicationDbContext();

            if (_db.Database.GetPendingMigrations().Count() > 0)
                _db.Database.Migrate();
        }

        //public na czas migracji
        public ApplicationDbContext()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"siema","apka","app_data.db");

            _dbPath = Path.Combine(dir, "db.db3");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //[RELACJA] jeden uczen - wiele ocen
            builder.Entity<Student>().HasMany(s => s.Grades)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId);

            //[RELACJA] jeden przedmiot - wiele ocen
            builder.Entity<Subject>().HasMany(s => s.Grades)
                .WithOne(s => s.Subject)
                .HasForeignKey(s => s.SubjectId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data source={_dbPath}")
                .UseLazyLoadingProxies();
        }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
