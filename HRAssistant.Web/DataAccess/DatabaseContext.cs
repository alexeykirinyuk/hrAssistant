using HRAssistant.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRAssistant.Web.DataAccess
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<JobPositionEntity> JobPositions { get; set; }

        public DbSet<CityEntity> Cities { get; set; }

        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<VacancyEntity> Vacancies { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().ToTable("User");

            builder.Entity<JobPositionEntity>().ToTable("JobPosition")
                .HasOne(p => p.Template).WithOne(t => t.JobPosition)
                .HasForeignKey<JobPositionEntity>(e => e.TemplateId);

            var template = builder.Entity<TemplateEntity>().ToTable("Template");
            template.HasMany(t => t.Questions).WithOne(q => q.Template)
                .HasForeignKey(e => e.TemplateId);

            builder.Entity<QuestionEntity>().ToTable("Question");
            builder.Entity<InputQuestionEntity>().HasBaseType<QuestionEntity>();

            var selectQuestion = builder.Entity<SelectQuestionEntity>().HasBaseType<QuestionEntity>()
                .HasMany(q => q.Options).WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId);

            var option = builder.Entity<OptionEntity>().ToTable("Option");

            builder.Entity<GeneralQuestionEntity>().ToTable("GeneralQuestion");

            builder.Entity<CityEntity>().ToTable("City");

            var team = builder.Entity<TeamEntity>().ToTable("Team");
            team.HasOne(t => t.City).WithMany(c => c.Teams)
                .HasForeignKey(t => t.CityId);
            team.HasOne(t => t.TeamLead).WithMany(u => u.TeamLeadTeams)
                .HasForeignKey(t => t.TeamLeadId);

            var vacancy = builder.Entity<VacancyEntity>().ToTable("Vacancy");
            vacancy.HasOne(v => v.Form).WithOne(f => f.Vacancy)
                .HasForeignKey<VacancyEntity>(v => v.FormId);
            vacancy.HasOne(v => v.Team).WithMany(t => t.Vacancies)
                .HasForeignKey(v => v.TeamId);
            vacancy.HasOne(v => v.JobPosition).WithMany(t => t.Vacancies)
                .HasForeignKey(v => v.JobPositionId);

            builder.Entity<FormEntity>().ToTable("Form")
                .HasMany(f => f.Questions).WithOne(q => q.Form)
                .HasForeignKey(q => q.FormId);
        }
    }
}
