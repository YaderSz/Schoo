using SharedModels;
using Microsoft.EntityFrameworkCore;

namespace School_API.Data
 
{
    public class SchoolContext : DbContext

    {
        public DbSet<Student> Students { get; set; }    
        public DbSet<Attendance> Attendances { get; set; }
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }
    }
}
