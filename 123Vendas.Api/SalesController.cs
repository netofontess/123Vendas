using _123Vendas.Domain.Dtos;
using _123Vendas.Domain.Entities;
using _123Vendas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace _123Vendas.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(SaleDto sale)
        {
            await _saleService.CreateSaleAsync(sale);
            return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
        }

        [HttpPut("{saleId}")]
        public async Task<IActionResult> UpdateSale(Guid saleId, SaleDto saleUpdateDto)
        {
            await _saleService.UpdateSaleAsync(saleId, saleUpdateDto);
            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            await _saleService.CancelSaleAsync(id);
            return NoContent();
        }
    }
}
