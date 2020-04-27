using LMS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Corporation>().ToTable("Corporation");//.Property(s=>s.ID).HasValueGenerator();
            modelBuilder.Entity<Corporation>().Property(c => c.Name).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<Corporation>().Property(c => c.CorporationAddress).HasMaxLength(1000);
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Area>().ToTable("Area");
            //modelBuilder.Entity<Corporation>()
            //    // .HasOne(s=>s.ID)
            //    .HasMany(c => c.Departments);
            //modelBuilder.Entity<Department>()
            //    .HasKey(d => new { d.ID, d.DepartmentName })
            //    .HasName("IDAndName");
            //modelBuilder.Entity<Department>()
            //    .HasOne(p => p.Corporation)
            //    .WithMany(p => p.Departments)
            //    .HasForeignKey(p => p.Corporation.ID);
            //.HasPrincipalKey(b => b.Url);


            //modelBuilder.Entity<PostTag>()
            //  .HasKey(t => new { t.PostId, t.TagId });

            //modelBuilder.Entity<PostTag>()
            //    .HasOne(pt => pt.Post)
            //    .WithMany(p => p.PostTags)
            //    .HasForeignKey(pt => pt.PostId);

            //modelBuilder.Entity<PostTag>()
            //    .HasOne(pt => pt.Tag)
            //    .WithMany(t => t.PostTags)
            //    .HasForeignKey(pt => pt.TagId);

            // modelBuilder.Entity<City>()
            //    .HasOne(x => x.Province)   //指向外键表的导航属性
            //    .WithMany(x => x.Cities);  //外键表的导航属性指向自己
            //   // .HasForeignKey(x => x.ProvinceId);             //外键表Id

            //// modelBuilder.Entity<CityCompany>()
            //    // .HasKey(x => new { x.CityId, x.CompanyId });         //创建联合主键

            // modelBuilder.Entity<CityCompany>()
            //     .HasOne(x => x.City)             //指向外键表的导航属性
            //     .WithMany(x => x.CityCompanies);  //外键表的导航属性指向自己
            //                                       // .HasForeignKey(x => x.CityId);                     //外键表Id

            // modelBuilder.Entity<CityCompany>()
            //     .HasOne(x => x.Company)         //指向外键表的导航属性
            //     .WithMany(x => x.CityCompanies); //外键表的导航属性指向自己
            //   //  .HasForeignKey(x => x.CompanyId);                 //外键表Id

            // modelBuilder.Entity<Mayor>()
            //     .HasOne(x => x.City)             //指向外键表的导航属性
            //     .WithOne(x => x.Mayor)           //外键表的导航属性指向自己
            //     .HasForeignKey<Mayor>("CityIDString");   //外键表Id
            // base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Corporation> Corporations { get; set; }
        public DbSet<Area> Areas { get; set; }
        //public DbSet<City> Cities { get; set; }
        //public DbSet<Province> provinces { get; set; }
        //public DbSet<Company> Companys { get; set; }
        //public DbSet<CityCompany> CityCompanys { get; set; }
        //public DbSet<Mayor> Mayor { get; set; }
    }
    //public class City
    //{
    //    /// <summary>
    //    /// 主键Id
    //    /// </summary>
    //    public int CityId { get; set; }

    //    /// <summary>
    //    /// 外键Id
    //    /// </summary>
    //    public int ProvinceId { get; set; }

    //    /// <summary>
    //    /// 导航属性
    //    /// </summary>
    //    public Province Province { get; set; }

    //    public IEnumerable<CityCompany> CityCompanies { get; set; }
    //    public Mayor Mayor { get; set; }
    //}
    //public class Province
    //{
    //    public int ProvinceId { get; set; }
    //    public IEnumerable<City> Cities { get; set; }
    //}
    //public class Company
    //{
    //    public int CompanyId { get; set; }
    //    public IEnumerable<CityCompany> CityCompanies { get; set; }
    //}
    //public class CityCompany
    //{
    //    public int CityCompanyId { get; set; }

    //    public int CityId { get; set; }
    //    public City City { get; set; }

    //    public int CompanyId { get; set; }
    //    public Company Company { get; set; }
    //}
    //public class Mayor
    //{
    //    public int MayOrId { get; set; }

    //   // public int CityId { get; set; }
    //    public City City { get; set; }
    //}
}
