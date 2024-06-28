using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfig;

public class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees", schema: SchemaNames.HR)
            .HasIndex(i => i.FirstName)
            .HasDatabaseName("IX_Employees_FirstName");
        builder.HasIndex(i => i.LastName)
            .HasDatabaseName("IX_Employees_LastName");
    }
}