using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using StudentAPIw6.Model;

namespace StudentAPIw6.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<SinhVien> SinhViens => Set<SinhVien>();
        public DbSet<LopHoc> LopHocs => Set<LopHoc>();
        public DbSet<BoMon> BoMons => Set<BoMon>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ giữa LopHoc và BoMon
            modelBuilder.Entity<LopHoc>()
                .HasOne(lh => lh.BoMon)          // LopHoc có một BoMon
                .WithMany(bm => bm.lopHocs)      // BoMon có nhiều LopHocs
                .HasForeignKey(lh => lh.BoMonId);   // Khóa ngoại ở bảng LopHoc là cột MaBM (string)

            modelBuilder.Entity<SinhVien>()
            .HasOne(sv => sv.lopHoc)
            .WithMany(lh => lh.SinhViens)
            .HasForeignKey(sv => sv.LopHocId);



        }

    }
}