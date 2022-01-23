using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Projects.ToListAsync());
        }
        public IActionResult Create() //creating new project
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Project project) //creating new project
        {
            db.Projects.Add(project);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id) //project details
        {
            if (id != null)
            {
                Project project = await db.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project != null)
                    return View(project);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id) //edit projects
        {
            if (id != null)
            {
                Project project= await db.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project != null)
                    return View(project);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Project project) //edit projects
        {
            db.Projects.Update(project);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id) //deleting project
        {
            if (id != null)
            {
                Project project = await db.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project != null)
                    return View(project);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id) //deleting project
        {
            if (id != null)
            {
                Project project = new Project { Id = id.Value };
                db.Entry(project).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
