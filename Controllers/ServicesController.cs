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
    }
}
