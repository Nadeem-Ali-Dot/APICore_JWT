using Microsoft.EntityFrameworkCore;

namespace API_WithAuthorize.Models
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }    
    }
}
