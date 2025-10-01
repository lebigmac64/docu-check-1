using DocuCheck.Domain.Entities.ChecksHistory;
using Microsoft.EntityFrameworkCore;

namespace DocuCheck.Infrastructure.Persistence
{
    public class DocuCheckDbContext : DbContext
    {
        public DocuCheckDbContext()
        {
            // for EF Core
        }
        
        public DocuCheckDbContext(DbContextOptions<DocuCheckDbContext> options) 
            : base(options)
        {
        }
        
        public const string ConnectionStringKey = "DocuCheckDb";
        
        public DbSet<CheckHistory> CheckHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocuCheckDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}