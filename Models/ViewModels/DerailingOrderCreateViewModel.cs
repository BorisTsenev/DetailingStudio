namespace DetailingStudio.Models.ViewModels
{
    public class DerailingOrderCreateViewModel
    {

        public int? ExistingVehicleId { get; set; }


        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }


        public string? Make { get; set; }

        public string? Model { get; set; }

        public string? RegistrationNumber { get; set; }

        public string? VinNumber { get; set; }

        public int? Year { get; set; }

        public string? Color { get; set; }

        public int? VehicleCategoryId { get; set; }


        public DateTime AppointmentDate { get; set; }

        public string? Notes { get; set; }


        public List<int> SelectedServiceIds { get; set; }
            = new List<int>();

    }
}
