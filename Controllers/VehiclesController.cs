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
            await LoadVehicleFormData();


            return View();
        }

        private async Task LoadVehicleFormData()
        {
            List<Customer> customers = await context.Customers.ToListAsync();

            ViewBag.Customers = customers;

            ViewBag.CustomerOptions = customers
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FirstName} {c.LastName} - {c.PhoneNumber}"
                })
                .ToList();

            ViewBag.VehicleCategories = new SelectList(
                await context.VehicleCategories.ToListAsync(),
                "Id",
                "Name");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
        Vehicle vehicle,
        string ownerInput,
        int? selectedCustomerId)
        {
            Customer? newCustomer = null;

            if (selectedCustomerId.HasValue)
            {
                bool customerExists = await context.Customers
                    .AnyAsync(c => c.Id == selectedCustomerId.Value);

                if (!customerExists)
                {
                    ModelState.AddModelError(
                        "CustomerId",
                        "The selected customer does not exist.");
                }
                else
                {
                    vehicle.CustomerId = selectedCustomerId.Value;
                }
            }
            else
            {
                string[] customerData = ownerInput.Split(
                    " ",
                    StringSplitOptions.RemoveEmptyEntries);

                if (customerData.Length != 3)
                {
                    ModelState.AddModelError(
                        "CustomerId",
                        "Enter the customer as: FirstName LastName PhoneNumber");
                }
                else
                {
                    string firstName = customerData[0];
                    string lastName = customerData[1];
                    string phoneNumber = customerData[2];

                    Customer? existingCustomer = await context.Customers
                        .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);

                    if (existingCustomer != null)
                    {
                        vehicle.CustomerId = existingCustomer.Id;
                    }
                    else
                    {
                        newCustomer = new Customer
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            PhoneNumber = phoneNumber
                        };
                    }
                }
            }

            ModelState.Remove(nameof(Vehicle.Customer));
            ModelState.Remove(nameof(Vehicle.VehicleCategory));

            if (!ModelState.IsValid)
            {
                await LoadVehicleFormData();

                ViewBag.OwnerInput = ownerInput;

                return View(vehicle);
            }

            if (newCustomer != null)
            {
                await context.Customers.AddAsync(newCustomer);
                await context.SaveChangesAsync();

                vehicle.CustomerId = newCustomer.Id;
            }

            await context.Vehicles.AddAsync(vehicle);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            Vehicle? vehicle = await context.Vehicles
                .Include(v => v.Customer)
                .Include(v => v.VehicleCategory)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Vehicle? vehicle = await context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            await LoadVehicleFormData();

            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Vehicle vehicle)
        {
            ModelState.Remove(nameof(Vehicle.Customer));
            ModelState.Remove(nameof(Vehicle.VehicleCategory));

            if (!ModelState.IsValid)
            {
                await LoadVehicleFormData();
                return View(vehicle);
            }

            context.Vehicles.Update(vehicle);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Vehicle? vehicle = await context.Vehicles
                .Include(v => v.VehicleCategory)
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Vehicle? vehicle = await context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
