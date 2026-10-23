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

        public async Task<IActionResult> Details(int id)
        {
            Customer? customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            await context.Customers.AddAsync(customer);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
