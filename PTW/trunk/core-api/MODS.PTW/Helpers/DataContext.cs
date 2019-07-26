using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using MODS.PTW.Models;


namespace MODS.PTW.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var con = GetConnectionString();
                optionsBuilder.UseSqlServer(con);
            }

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>()
                .HasKey(c => c.ID);
            builder.Entity<Company>()
                .HasOne(c => c.companyCatagories)
               .WithMany(cc => cc.companies)
               .HasForeignKey(c => c.CategoryID);

            builder.Entity<Equipment>()
                .HasKey(eq => eq.ID);
            builder.Entity<Equipment>()
                .HasOne(eq => eq.equipmentCatagories)
                .WithMany(eqc => eqc.Equipment)
                .HasForeignKey(eq => eq.CatagoryId);

            builder.Entity<PTWQuestionTypeAns>()
               .HasKey(p => p.ID);
            builder.Entity<PTWQuestionTypeAns>()
                .HasOne(pt => pt.Question)
                .WithMany(b => b.Types)
                .HasForeignKey(bc => bc.QAID);
            builder.Entity<PTWQuestionTypeAns>()
                .HasOne(bc => bc.Type)
                .WithMany(c => c.Questions)
                .HasForeignKey(bc => bc.TypeID);
        }

        /* builder.Entity<UserRole>()
            .HasIndex(u => new { u.RoleId, u.UserId })
            .IsUnique();
         builder.Entity<Role>()
           .HasIndex(u => u.Name)
           .IsUnique();
         builder.Entity<User>()
             .HasIndex(u => u.Email)
             .IsUnique();
         builder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

         //seed data
         builder.Entity<Role>().HasData(
             new Role() { Id = 1, Name = "Administrator" },
             new Role() { Id = 2, Name = "User" }
         );*/



        //public DbSet<User> Users { get; set; }
        // public DbSet<Role> Roles { get; set; }
        // public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Questionnarie> questionnaries { get; set; }
        public DbSet<Media> Media { get; set; }       
        public DbSet<WorkPermitStatus> WorkPermitStatus { get; set; }
        public DbSet<WorkPermitTypes> WorkPermitTypes { get; set; }
        public DbSet<UserAuthority> UserAuthorities { get; set; }
        public DbSet<CompanyCatagory> CompanyCategories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentCatagory> EquipmentCatagory { get; set; }
        public DbSet<PTWQuestion> PTWSelectionQA { get; set; }
        public DbSet<PTWType> PTWType { get; set; }
        public DbSet<PTWQuestionTypeAns> PTWQuestionTypeAns { get; set; }
        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<CertificateAuthority> CertificateAuthority { get; set; }
        public DbSet<CertificateQA> CertificateQA { get; set; }

    }
}