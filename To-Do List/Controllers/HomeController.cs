using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using To_Do_List.Data;
using To_Do_List.Data.Entities;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ListContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ListContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var SignedInUser = await userManager.GetUserAsync(User);
            var Tasks = await context.Set<TaskEntity>().Where(T => T.ApplicationUserID == SignedInUser.Id).ToListAsync();
            ViewData["Active"] = Tasks.Where(T => T.IsDone == false).ToList() ;
            ViewData["Completed"] = Tasks.Where(T => T.IsDone == true).ToList();
            return View(Tasks);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}