using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Route.C41.MVC.DAL.Models.Employee;

namespace Route.C41.MVC.DAL.Data.Configrations
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e=>e.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(e=>e.Address).IsRequired();
            builder.Property(e => e.salary).HasColumnType("decimal(12,2)");
            builder.Property(e => e.gen)
                .HasConversion(
            (gen) => gen.ToString(),
                (genAsString) => (gender) Enum.Parse(typeof(gender),genAsString,true)

                );
        }
    }
}
