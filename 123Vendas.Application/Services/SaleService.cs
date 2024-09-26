using _123Vendas.Domain.Dtos;
using _123Vendas.Domain.Entities;
using _123Vendas.Domain.Interfaces.Repositories;
using _123Vendas.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace _123Vendas.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ILogger<SaleService> _logger;
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public SaleService(ILogger<SaleService> logger, IMapper mapper, ISaleRepository saleRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public async Task<Sale?> GetSaleByIdAsync(Guid id)
        {
            return await _saleRepository.GetByIdAsync(id);
        }

        public async Task CreateSaleAsync(SaleDto saleUpdateDto)
        {
            var sale = _mapper.Map<Sale>(saleUpdateDto);
            await _saleRepository.AddAsync(sale);
            _logger.LogInformation("Compra criada: {@Sale}", sale);
        }

        public async Task UpdateSaleAsync(Guid saleId, SaleDto saleUpdateDto)
        {
            saleUpdateDto.Id = saleId;
            var sale = _mapper.Map<Sale>(saleUpdateDto);
            await _saleRepository.UpdateAsync(sale);
        }

        public async Task CancelSaleAsync(Guid saleId)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);

            if (sale == null)
            {
                _logger.LogWarning("Tentativa de cancelar uma venda não encontrada. ID: {SaleId}", saleId);
                throw new Exception($"A venda com ID {saleId} não foi encontrada.");
            }

            sale.IsCancelled = true;
            await _saleRepository.UpdateAsync(sale);

            _logger.LogInformation("Compra cancelada: {@Sale}", sale);
        }
    }
}
