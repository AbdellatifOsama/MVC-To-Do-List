using Microsoft.AspNetCore.Identity;

namespace To_Do_List.Data.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public List<TaskEntity> Tasks { get; set; }
    }
}
