using HRAssistant.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRAssistant.Web.DataAccess.Configurations
{
    public sealed class FormSagaConfiguration : IEntityTypeConfiguration<FormSagaEntity>
    {
        public void Configure(EntityTypeBuilder<FormSagaEntity> builder)
        {
            builder.ToTable("FormSaga");

            builder.HasMany(f => f.Questions).WithOne(q => q.FormSaga)
                .HasForeignKey(q => q.FormSagaId);
        }
    }
}
