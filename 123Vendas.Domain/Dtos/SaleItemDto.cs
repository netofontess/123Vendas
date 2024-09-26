﻿namespace _123Vendas.Domain.Dtos
{
    public class SaleItemDto
    {
        public Guid? Id { get; set; }
        public string? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
