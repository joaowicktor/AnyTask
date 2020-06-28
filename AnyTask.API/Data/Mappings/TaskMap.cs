using AnyTask.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyTask.API.Data.Mappings
{
    public class TaskMap : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("tasks", "dbo");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(200).IsRequired();
            builder.Property(p => p.Concluded).HasColumnName("concluded").HasDefaultValue(false).IsRequired();
            builder.Property(p => p.UserId).HasColumnName("id_user").IsRequired();

            builder.HasOne<User>().WithMany(u => u.Tasks).HasForeignKey(t => t.UserId);
        }
    }
}
