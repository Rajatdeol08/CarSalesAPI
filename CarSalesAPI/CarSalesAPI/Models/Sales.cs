namespace CarSalesAPI.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public string Salesman { get; set; }
        public string Class { get; set; }
        public string Brand { get; set; }
        public int CarsSold { get; set; }
        public decimal TotalSaleAmount { get; set; }
    }
}
