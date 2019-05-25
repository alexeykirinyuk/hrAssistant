using HRAssistant.Web.DataAccess.Configurations;
using HRAssistant.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HRAssistant.Web.DataAccess
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<JobPositionEntity> JobPositions { get; set; }

        public DbSet<CityEntity> Cities { get; set; }

        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<VacancyEntity> Vacancies { get; set; }

        public DbSet<InterviewEntity> Interviews { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));
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
            builder.Entity<GeneralQuestionEntity>().HasBaseType<QuestionEntity>();

            var selectQuestion = builder.Entity<SelectQuestionEntity>().HasBaseType<QuestionEntity>()
                .HasMany(q => q.Options).WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId);

            var option = builder.Entity<OptionEntity>().ToTable("Option");

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

            builder.ApplyConfiguration(new InterviewConfiguration());
            builder.Entity<CandidateEntity>().ToTable("Candidate");

            builder.ApplyConfiguration(new FormSagaConfiguration());
            builder.ApplyConfiguration(new QuestionSagaConfiguration());
            builder.Entity<SelectQuestionSagaEntity>().HasBaseType<QuestionSagaEntity>();
            builder.Entity<InputQuestionSagaEntity>().HasBaseType<QuestionSagaEntity>();
            builder.Entity<GeneralQuestionSagaEntity>().HasBaseType<QuestionSagaEntity>();

            base.OnModelCreating(builder);
        }
    }
}
