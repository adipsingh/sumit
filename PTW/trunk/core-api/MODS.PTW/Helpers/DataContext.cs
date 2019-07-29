using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using MODS.PTW.Models;
using MODS.PTW.Dtos;

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
            base.OnModelCreating(builder);

            builder.Entity<Company>()
                .HasKey(c => c.ID);
            builder.Entity<Company>()
                .HasOne(c => c.companyCatagories)
               .WithMany(cc => cc.companies)
               .HasForeignKey(c => c.CategoryID);

            builder.Entity<PrecautionQA>()
                .HasKey(p => p.ID);
            builder.Entity<PrecautionQA>()
                .HasOne(p => p.precautionType)
                .WithMany(pt => pt.precautions)
                .HasForeignKey(p => p.PrecautionTypeID);

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

            //certificates realted 

            builder.Entity<Certificate>()
                .HasKey(c => c.ID);

            builder.Entity<Question>()
                .HasKey(c => c.ID);

            builder.Entity<CertificateQuestion>()
                .HasKey(bc => new { bc.QuestionID, bc.CertificateID });

            builder.Entity<CertificateQuestion>()
                 .HasOne(bc => bc.Certificate)
                 .WithMany(b => b.CertificateQuestions)
                 .HasForeignKey(bc => bc.CertificateID);

            builder.Entity<CertificateQuestion>()
                .HasOne(bc => bc.Question)
                .WithMany(c => c.CertificateQuestions)
                .HasForeignKey(bc => bc.QuestionID);

        }

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
        public DbSet<PrecautionQA> PrecautionQA { get; set; }
        public DbSet<PrecautionType> PrecautionType { get; set; }
        public DbSet<Fwbs> FWBS { get; set; }

        //certificates 
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CertificateAuthority> CertificateAuthority { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<CertificateQuestion> CertificateQuestions { get; set; }
    }
}