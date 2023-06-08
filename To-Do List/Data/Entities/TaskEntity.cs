using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Data.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
