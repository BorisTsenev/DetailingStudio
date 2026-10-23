using Microsoft.AspNetCore.Mvc;
using DetailingStudio.Data;
using DetailingStudio.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DetailingStudio.Controllers
{
    public class CustomersController : Controller
    {
        private readonly DetailingStudioDbContext context;
        public CustomersController(DetailingStudioDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Customer> customers = await context.Customers.ToListAsync();
            return View(customers);
        }
    }
}
