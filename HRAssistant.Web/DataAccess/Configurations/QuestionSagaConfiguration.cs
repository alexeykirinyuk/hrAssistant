using System;
using HRAssistant.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRAssistant.Web.DataAccess.Configurations
{
    public sealed class QuestionSagaConfiguration : IEntityTypeConfiguration<QuestionSagaEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionSagaEntity> builder)
        {
            builder.ToTable("QuestionSaga");

            builder.HasOne(q => q.Question).WithMany(q => q.Sagas)
                .HasForeignKey(q => q.QuestionId);
        }
    }
}
