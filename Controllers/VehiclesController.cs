using Microsoft.AspNetCore.Mvc;
using DetailingStudio.Data;
using Microsoft.EntityFrameworkCore;
using DetailingStudio.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DetailingStudio.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly DetailingStudioDbContext context;

        public VehiclesController(DetailingStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Vehicle> vehicles = await context.Vehicles
                .Include(v => v.Customer)
                .Include(v => v.VehicleCategory)
                .ToListAsync();

            return View(vehicles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = await context.Customers.ToListAsync();

            ViewBag.VehicleCategories = new SelectList(
                await context.VehicleCategories.ToListAsync(),
                "Id",
                "Name");

            return View();
        }
    }
}
