using _123Vendas.Domain.Entities;
using _123Vendas.Domain.Interfaces.Repositories;
using _123Vendas.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SalesDbContext _context;

        public SaleRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale)
        {
            _context.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Sale sale)
        {
            _context.Remove(sale);
            await _context.SaveChangesAsync();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Sale>()
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Sale sale)
        {
            var existingSale = _context.Sales
                .Where(p => p.Id == sale.Id)
                .Include(p => p.SaleItems)
                .SingleOrDefault();

            if (existingSale != null)
            {

                _context.Entry(existingSale).CurrentValues.SetValues(sale);

                foreach (var existingItem in existingSale.SaleItems.ToList())
                {
                    if (!sale.SaleItems.Any(c => c.Id == existingItem.Id))
                        _context.SaleItem.Remove(existingItem);
                }

                foreach (var saleItem in sale.SaleItems)
                {
                    saleItem.SaleId = sale.Id;
                    var existingChild = existingSale.SaleItems
                        .Where(c => c.Id == saleItem.Id && c.Id != default(Guid))
                        .SingleOrDefault();

                    if (existingChild != null)
                        _context.Entry(existingChild).CurrentValues.SetValues(saleItem);
                    else
                    {
                        var newItem = new SaleItem
                        {
                            Id = Guid.NewGuid(),
                            SaleId = sale.Id,
                            Product = saleItem.Product,
                            Quantity = saleItem.Quantity,
                            UnitPrice = saleItem.UnitPrice,
                            Discount = saleItem.Discount,
                            IsCancelled = saleItem.IsCancelled
                        };
                        existingSale.SaleItems.Add(newItem);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
