using DetailingStudio.Data;
using DetailingStudio.Data.Models;
using DetailingStudio.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DetailingStudio.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DetailingStudioDbContext context;

        public DashboardController(DetailingStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            DateTime now = DateTime.Now;

            DateTime startOfMonth =
                new DateTime(now.Year, now.Month, 1);

            DateTime startOfNextMonth =
                startOfMonth.AddMonths(1);


            List<DetailingOrder> completedOrders =
                await context.DetailingOrders
                    .Where(o =>
                        o.Status == "Completed" &&
                        o.AppointmentDate >= startOfMonth &&
                        o.AppointmentDate < startOfNextMonth)
                    .Include(o => o.DetailingOrderServices)
                        .ThenInclude(os => os.Service)
                    .ToListAsync();


            DashboardViewModel viewModel =
                new DashboardViewModel();


            viewModel.OrdersThisMonth =
                completedOrders.Count;


            viewModel.ServicesThisMonth =
                completedOrders
                    .Sum(o => o.DetailingOrderServices.Count);


            viewModel.RevenueThisMonth =
                completedOrders
                    .Sum(o => o.DetailingOrderServices
                        .Sum(os => os.Price));


            viewModel.TotalCustomers =
                await context.Customers.CountAsync();


            var serviceStatistics =
                completedOrders
                    .SelectMany(o => o.DetailingOrderServices)
                    .GroupBy(os => os.Service.Name)
                    .Select(g => new
                    {
                        Name = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .ToList();


            viewModel.ServiceNames =
                serviceStatistics
                    .Select(s => s.Name)
                    .ToList();


            viewModel.ServiceCounts =
                serviceStatistics
                    .Select(s => s.Count)
                    .ToList();


            viewModel.RecentOrders =
                await context.DetailingOrders
                    .Include(o => o.Vehicle)
                        .ThenInclude(v => v.Customer)
                    .Include(o => o.DetailingOrderServices)
                    .OrderByDescending(o => o.CreatedOn)
                    .Take(5)
                    .ToListAsync();


            return View(viewModel);
        }
    }
}
