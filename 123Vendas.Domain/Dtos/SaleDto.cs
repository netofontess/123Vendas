namespace _123Vendas.Domain.Dtos
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string? Customer { get; set; }
        public decimal TotalValue { get; set; }
        public string? Branch { get; set; }
        public List<SaleItemDto>? SaleItems { get; set; }
        public bool IsCancelled { get; set; }
    }
}
