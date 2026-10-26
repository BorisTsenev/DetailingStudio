using DetailingStudio.Data.Models;

namespace DetailingStudio.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int OrdersThisMonth { get; set; }

        public int ServicesThisMonth { get; set; }

        public decimal RevenueThisMonth { get; set; }

        public int TotalCustomers { get; set; }

        public List<string> ServiceNames { get; set; }
            = new List<string>();

        public List<int> ServiceCounts { get; set; }
            = new List<int>();

        public List<DetailingOrder> RecentOrders { get; set; }
            = new List<DetailingOrder>();
    }
}