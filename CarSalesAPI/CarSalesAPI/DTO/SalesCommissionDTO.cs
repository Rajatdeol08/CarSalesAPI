namespace CarSalesAPI.DTOs
{
    public class SalesCommissionDTO
    {
        public string Salesman { get; set; }
        public string Class { get; set; }
        public decimal TotalSales { get; set; }
        public decimal FixedCommission { get; set; }
        public decimal ClassCommission { get; set; }
        public decimal ExtraCommission { get; set; }
        public decimal TotalCommission => FixedCommission + ClassCommission + ExtraCommission;
    }
}
