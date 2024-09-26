namespace _123Vendas.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string? Customer { get; set; }
        public decimal TotalValue { get; set; }
        public string? Branch { get; set; }
        public List<SaleItem>? SaleItems { get; set; }
        public bool IsCancelled { get; set; }
    }
}
