using _123Vendas.Domain.Dtos;
using _123Vendas.Domain.Entities;

namespace _123Vendas.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        Task<Sale?> GetSaleByIdAsync(Guid id);
        Task CreateSaleAsync(SaleDto saleUpdateDto);
        Task UpdateSaleAsync(Guid saleId, SaleDto saleUpdateDto);
        Task CancelSaleAsync(Guid saleId);
    }
}
