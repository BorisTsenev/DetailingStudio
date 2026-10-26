using DetailingStudio.Data;
using Microsoft.AspNetCore.Mvc;
using DetailingStudio.Data.Models;
using Microsoft.EntityFrameworkCore;
using DetailingStudio.Models.ViewModels;

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


        private async Task LoadCreateFormData()
        {
            ViewBag.Vehicles = await context.Vehicles
                .Include(v => v.Customer)
                .Include(v => v.VehicleCategory)
                .ToListAsync();

            ViewBag.VehicleCategories = await context.VehicleCategories
                .ToListAsync();

            ViewBag.Services = await context.Services
                .Where(s => s.IsActive)
                .Include(s => s.ServicePrices)
                    .ThenInclude(sp => sp.VehicleCategory)
                .ToListAsync();


            ViewBag.Customers = await context.Customers
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToListAsync();
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadCreateFormData();

            DetailingOrderCreateViewModel viewModel =
                new DetailingOrderCreateViewModel();

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    DetailingOrderCreateViewModel viewModel)
        {
            Vehicle? vehicle = null;
            Customer? customer = null;

            int vehicleCategoryId = 0;

            if (viewModel.SelectedServiceIds.Count == 0)
            {
                ModelState.AddModelError(
                    nameof(viewModel.SelectedServiceIds),
                    "Select at least one service.");
            }

            if (viewModel.AppointmentDate == default)
            {
                ModelState.AddModelError(
                    nameof(viewModel.AppointmentDate),
                    "Appointment date is required.");
            }


            if (viewModel.ExistingVehicleId.HasValue)
            {
                vehicle = await context.Vehicles
                    .FirstOrDefaultAsync(v =>
                        v.Id == viewModel.ExistingVehicleId.Value);

                if (vehicle == null)
                {
                    ModelState.AddModelError(
                        nameof(viewModel.ExistingVehicleId),
                        "The selected vehicle does not exist.");
                }
                else
                {
                    vehicleCategoryId = vehicle.VehicleCategoryId;
                }
            }

            else
            {
                if (string.IsNullOrWhiteSpace(viewModel.Make))
                {
                    ModelState.AddModelError(
                        nameof(viewModel.Make),
                        "Vehicle make is required.");
                }

                if (string.IsNullOrWhiteSpace(viewModel.Model))
                {
                    ModelState.AddModelError(
                        nameof(viewModel.Model),
                        "Vehicle model is required.");
                }

                if (string.IsNullOrWhiteSpace(viewModel.RegistrationNumber))
                {
                    ModelState.AddModelError(
                        nameof(viewModel.RegistrationNumber),
                        "Registration number is required.");
                }

                if (!viewModel.VehicleCategoryId.HasValue)
                {
                    ModelState.AddModelError(
                        nameof(viewModel.VehicleCategoryId),
                        "Vehicle category is required.");
                }


                if (viewModel.ExistingCustomerId.HasValue)
                {
                    customer = await context.Customers
                        .FirstOrDefaultAsync(c =>
                            c.Id == viewModel.ExistingCustomerId.Value);

                    if (customer == null)
                    {
                        ModelState.AddModelError(
                            nameof(viewModel.ExistingCustomerId),
                            "The selected customer does not exist.");
                    }
                }

                else
                {
                    if (string.IsNullOrWhiteSpace(viewModel.FirstName))
                    {
                        ModelState.AddModelError(
                            nameof(viewModel.FirstName),
                            "First name is required.");
                    }

                    if (string.IsNullOrWhiteSpace(viewModel.LastName))
                    {
                        ModelState.AddModelError(
                            nameof(viewModel.LastName),
                            "Last name is required.");
                    }

                    if (string.IsNullOrWhiteSpace(viewModel.PhoneNumber))
                    {
                        ModelState.AddModelError(
                            nameof(viewModel.PhoneNumber),
                            "Phone number is required.");
                    }

                    if (!string.IsNullOrWhiteSpace(viewModel.PhoneNumber))
                    {
                        customer = await context.Customers
                            .FirstOrDefaultAsync(c =>
                                c.PhoneNumber == viewModel.PhoneNumber);

                        if (customer == null)
                        {
                            customer = new Customer
                            {
                                FirstName = viewModel.FirstName!,
                                LastName = viewModel.LastName!,
                                PhoneNumber = viewModel.PhoneNumber!,
                                Email = viewModel.Email
                            };
                        }
                    }
                }
            }


            if (!ModelState.IsValid)
            {
                await LoadCreateFormData();
                return View(viewModel);
            }


            if (vehicle == null)
            {
                vehicle = new Vehicle
                {
                    Make = viewModel.Make!,
                    Model = viewModel.Model!,
                    RegistrationNumber = viewModel.RegistrationNumber!,
                    VinNumber = viewModel.VinNumber,
                    Year = viewModel.Year,
                    Color = viewModel.Color,
                    VehicleCategoryId = viewModel.VehicleCategoryId!.Value,
                    Customer = customer!
                };

                vehicleCategoryId =
                    viewModel.VehicleCategoryId.Value;
            }



            List<int> selectedServiceIds =
                viewModel.SelectedServiceIds
                    .Distinct()
                    .ToList();

            List<ServicePrice> servicePrices =
                await context.ServicePrices
                    .Include(sp => sp.Service)
                    .Where(sp =>
                        selectedServiceIds.Contains(sp.ServiceId) &&
                        sp.VehicleCategoryId == vehicleCategoryId &&
                        sp.Service.IsActive)
                    .ToListAsync();


            if (servicePrices.Count != selectedServiceIds.Count)
            {
                ModelState.AddModelError(
                    nameof(viewModel.SelectedServiceIds),
                    "One or more selected services do not have a price for this vehicle category.");

                await LoadCreateFormData();

                return View(viewModel);
            }



            DetailingOrder order = new DetailingOrder
            {
                Vehicle = vehicle,
                CreatedOn = DateTime.Now,
                AppointmentDate = viewModel.AppointmentDate,
                Status = "Pending",
                Notes = viewModel.Notes
            };


            foreach (ServicePrice servicePrice in servicePrices)
            {
                DetailingOrderService orderService =
                    new DetailingOrderService
                    {
                        ServiceId = servicePrice.ServiceId,
                        Price = servicePrice.Price
                    };

                order.DetailingOrderServices.Add(orderService);
            }


            await context.DetailingOrders.AddAsync(order);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            DetailingOrder? order = await context.DetailingOrders
                .Include(o => o.Vehicle)
                .ThenInclude(v => v.Customer)
                .Include(o => o.Vehicle)
                .ThenInclude(v => v.VehicleCategory)
                .Include(o => o.DetailingOrderServices)
                .ThenInclude(dos => dos.Service)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            DetailingOrder? order = await context.DetailingOrders
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
                int id,
                DateTime appointmentDate,
                string status,
                string? notes)
        {
            DetailingOrder? order = await context.DetailingOrders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            if (appointmentDate == default)
            {
                ModelState.AddModelError(
                    nameof(appointmentDate),
                    "Appointment date is required.");
            }

            if (string.IsNullOrWhiteSpace(status))
            {
                ModelState.AddModelError(
                    nameof(status),
                    "Status is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            order.AppointmentDate = appointmentDate;
            order.Status = status;
            order.Notes = notes;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = order.Id });
        }
    }

}