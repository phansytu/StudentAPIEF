using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentAPIw6.Model;

namespace StudentAPIw6.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<SinhVien> SinhViens => Set<SinhVien>();
        public DbSet<LopHoc> LopHocs => Set<LopHoc>();
        public DbSet<BoMon> BoMons => Set<BoMon>();

    }
}