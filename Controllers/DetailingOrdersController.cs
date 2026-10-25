using DetailingStudio.Data;
using Microsoft.AspNetCore.Mvc;
using DetailingStudio.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DetailingStudio.Controllers
{
    public class DetailingOrdersController : Controller
    {

        private readonly DetailingStudioDbContext context;
        public DetailingOrdersController(DetailingStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<DetailingOrder> detailingOrders = await context.DetailingOrders
                .Include(o => o.Vehicle)
                .ThenInclude(v => v.Customer)
                .Include(o => o.DetailingOrderServices)
                .ThenInclude(dos => dos.Service)
                .ToListAsync();

            return View(detailingOrders);
        }
    }
}
