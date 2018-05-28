using Indexing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Indexing.Infra.SqLite.Context
{
    public class SampleContext : TnfDbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public SampleContext(DbContextOptions<SampleContext> options, ITnfSession session)
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(e =>
            {
                e.HasKey(k => k.EmployeeId);

                e.Property(p => p.Name).HasMaxLength(350).IsRequired();

                e.OwnsOne(
                    p => p.FullAddress,
                    sa =>
                    {
                        sa.Property(p => p.Street).HasMaxLength(250).HasColumnName("Street");
                        sa.Property(p => p.ZipCode).HasMaxLength(10).HasColumnName("ZipCode");
                    });

                e.ToTable("Employees");
            });
        }
    }
}
