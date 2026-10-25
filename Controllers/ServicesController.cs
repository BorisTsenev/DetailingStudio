using DetailingStudio.Data;
using DetailingStudio.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DetailingStudio.Models.ViewModels;


namespace DetailingStudio.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DetailingStudioDbContext context;

        public ServicesController(DetailingStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            List<VehicleCategory> categories = await context.VehicleCategories
                .ToListAsync();

            ViewBag.Categories = new SelectList(
                categories,
                "Id",
                "Name",
                categoryId);

            ViewBag.SelectedCategoryId = categoryId;

            IQueryable<Service> servicesQuery = context.Services
                .Include(s => s.ServicePrices)
                .ThenInclude(sp => sp.VehicleCategory);

            if (categoryId.HasValue)
            {
                servicesQuery = servicesQuery
                    .Where(s => s.ServicePrices
                        .Any(sp => sp.VehicleCategoryId == categoryId.Value));
            }

            List<Service> services = await servicesQuery.ToListAsync();

            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<VehicleCategory> categories = await context.VehicleCategories
                .ToListAsync();

            ServiceCreateViewModel viewModel = new ServiceCreateViewModel();

            foreach (VehicleCategory category in categories)
            {
                viewModel.ServicePrices.Add(new ServicePriceInputModel
                {
                    VehicleCategoryId = category.Id,
                    VehicleCategoryName = category.Name
                });
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateViewModel viewModel)
        {
            List<VehicleCategory> categories = await context.VehicleCategories
                .ToListAsync();

            foreach (VehicleCategory category in categories)
            {
                bool hasPrice = viewModel.ServicePrices
                    .Any(sp => sp.VehicleCategoryId == category.Id);

                if (!hasPrice)
                {
                    ModelState.AddModelError(
                        "",
                        $"A price for category {category.Name} is required.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            Service service = new Service
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive
            };

            foreach (ServicePriceInputModel priceInput in viewModel.ServicePrices)
            {
                ServicePrice servicePrice = new ServicePrice
                {
                    VehicleCategoryId = priceInput.VehicleCategoryId,
                    Price = priceInput.Price
                };

                service.ServicePrices.Add(servicePrice);
            }

            await context.Services.AddAsync(service);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Service? service = await context.Services
                .Include(s => s.ServicePrices)
                    .ThenInclude(sp => sp.VehicleCategory)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Service? service = await context.Services
                .Include(s => s.ServicePrices)
                .ThenInclude(sp => sp.VehicleCategory)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            ServiceCreateViewModel viewModel = new ServiceCreateViewModel
            {
                Name = service.Name,
                Description = service.Description,
                IsActive = service.IsActive
            };

            foreach (ServicePrice servicePrice in service.ServicePrices)
            {
                viewModel.ServicePrices.Add(new ServicePriceInputModel
                {
                    VehicleCategoryId = servicePrice.VehicleCategoryId,
                    VehicleCategoryName = servicePrice.VehicleCategory.Name,
                    Price = servicePrice.Price
                });
            }


            ViewBag.ServiceId = id;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    ServiceCreateViewModel viewModel)
        {
            Service? service = await context.Services
                .Include(s => s.ServicePrices)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            service.Name = viewModel.Name;
            service.Description = viewModel.Description;
            service.IsActive = viewModel.IsActive;

            foreach (ServicePriceInputModel priceInput in viewModel.ServicePrices)
            {
                ServicePrice? servicePrice = service.ServicePrices
                    .FirstOrDefault(sp =>
                        sp.VehicleCategoryId == priceInput.VehicleCategoryId);

                if (servicePrice != null)
                {
                    servicePrice.Price = priceInput.Price;
                }
            }

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Service? service = await context.Services
                .Include(s => s.ServicePrices)
                .ThenInclude(sp => sp.VehicleCategory)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Service? service = await context.Services
                .Include(s => s.ServicePrices)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            bool isUsedInOrder = await context.DetailingOrderServices
                .AnyAsync(os => os.ServiceId == id);

            if (isUsedInOrder)
            {
                service.IsActive = false;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            context.ServicePrices.RemoveRange(service.ServicePrices);
            context.Services.Remove(service);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
