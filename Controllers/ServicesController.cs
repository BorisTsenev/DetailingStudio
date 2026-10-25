using Microsoft.AspNetCore.Mvc;
using DetailingStudio.Data.Models;
using DetailingStudio.Data;
using Microsoft.EntityFrameworkCore;


namespace DetailingStudio.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DetailingStudioDbContext context;

        public ServicesController(DetailingStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Service> services = await context.Services
                .Include(s => s.ServicePrices)
                .ThenInclude(sp => sp.VehicleCategory)
                .ToListAsync();
            return View(services);
        }
    }
}
