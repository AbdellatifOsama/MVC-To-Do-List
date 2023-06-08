using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Data;
using To_Do_List.Data.Entities;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    public class TaskController : Controller
    {
        private readonly ListContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public TaskController(ListContext context)
        {
            this.context = context;
            this.userManager = userManager;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTask(AddTaskViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Task) && model.Task.Length > 2)
            {
                var IsNameFoundBefore = await context.Set<TaskEntity>().ToListAsync();
                var Task = new TaskEntity
                {
                    ApplicationUserID = model.UserId,
                    Name = model.Task,
                    IsDone = false,
                };
                context.Set<TaskEntity>().Add(Task);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("index", "home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeTaskDone([FromRoute] int id)
        {
            var task = await context.Set<TaskEntity>().FindAsync(id);
            if (task is not null)
            {
                task.IsDone = true;
                context.Set<TaskEntity>().Update(task);
                await context.SaveChangesAsync();
                return RedirectToAction("index", "home");
            }
            return NotFound();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> MakeTaskUnDone([FromRoute] int id)
        {
            var task = await context.Set<TaskEntity>().FindAsync(id);
            if (task is not null)
            {
                task.IsDone = false;
                context.Set<TaskEntity>().Update(task);
                await context.SaveChangesAsync();
                return RedirectToAction("index", "home");
            }
            return NotFound();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            var task = await context.Set<TaskEntity>().FindAsync(id);
            if (task is not null)
            {
                context.Set<TaskEntity>().Remove(task);
                await context.SaveChangesAsync();
                return RedirectToAction("index", "home");
            }
            return NotFound();
        }
    }
}
