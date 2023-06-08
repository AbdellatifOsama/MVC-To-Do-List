using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Data.Entities;

namespace To_Do_List.Data
{
    public class ListContext:IdentityDbContext<ApplicationUser>
    {
        public ListContext(DbContextOptions<ListContext> options):base(options)
        {
            
        }
        DbSet<TaskEntity> Tasks { get; set; }
    }
}
