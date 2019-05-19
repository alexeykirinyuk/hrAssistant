using System;
using HRAssistant.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRAssistant.Web.DataAccess.Configurations
{
    public sealed class InterviewConfiguration : IEntityTypeConfiguration<InterviewEntity>
    {
        public void Configure(EntityTypeBuilder<InterviewEntity> builder)
        {
            builder.ToTable("Interview");

            builder.HasOne(i => i.Candidate).WithOne(i => i.Interview)
                .HasForeignKey<InterviewEntity>(i => i.CandidateId);

            builder.HasOne(i => i.Vacancy).WithMany(v => v.Interviews)
                .HasForeignKey(i => i.VacancyId);

            builder.HasOne(i => i.FormSagaEntity).WithOne(f => f.Interview)
                .HasForeignKey<InterviewEntity>(i => i.FormSagaId);
        }
    }
}