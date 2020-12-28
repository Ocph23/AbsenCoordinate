using AbsenCoordinatWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbsenCoordinatWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser,IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Karyawan> Karyawans { get; set; }
        public DbSet<Absen> Absens { get; set; }
        public DbSet<Cuti> Cutis { get; set; }
        public DbSet<PersetujuanCuti> Persetujuan{ get; set; }
        public DbSet<Tempat> Tempats { get; set; }
        public DbSet<TempatDetail> TempatDetails { get; set; }
    }
}
