using _123Vendas.Application.Services;
using _123Vendas.Domain.Dtos;
using _123Vendas.Domain.Entities;
using _123Vendas.Domain.Interfaces.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace _123Vendas.Test.Application.Services
{
    public class SaleServiceTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleService> _logger;
        private readonly IMapper _mapper;
        private readonly SaleService _saleService;

        public SaleServiceTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _logger = Substitute.For<ILogger<SaleService>>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SaleDto, Sale>();
                cfg.CreateMap<Sale, SaleDto>();
            });
            _mapper = new Mapper(config);

            _saleService = new SaleService(_logger, _mapper, _saleRepository);
        }

        [Fact]
        public async Task CreateSaleAsync_ShouldCallAddAsync()
        {
            // Arrange
            var saleDto = new SaleDto { Id = Guid.NewGuid(), SaleNumber = 123, TotalValue = 1000 };

            // Act
            await _saleService.CreateSaleAsync(saleDto);

            // Assert
            await _saleRepository.Received(1).AddAsync(Arg.Any<Sale>());
        }

        [Fact]
        public async Task UpdateSaleAsync_ShouldCallUpdateAsync()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var saleDto = new SaleDto { SaleNumber = 123, TotalValue = 1000 };

            // Act
            await _saleService.UpdateSaleAsync(saleId, saleDto);

            // Assert
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>());
        }

        [Fact]
        public async Task GetSaleByIdAsync_ShouldReturnSale()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var expectedSale = new Sale { Id = saleId, SaleNumber = 123, TotalValue = 1000 };
            _saleRepository.GetByIdAsync(saleId).Returns(expectedSale);

            // Act
            var result = await _saleService.GetSaleByIdAsync(saleId);

            // Assert
            result.Should().BeEquivalentTo(expectedSale);
        }

        [Fact]
        public async Task CancelSaleAsync_ShouldSetIsCancelledToTrue()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var sale = new Sale { Id = saleId, SaleNumber = 123, TotalValue = 1000, IsCancelled = false };
            _saleRepository.GetByIdAsync(saleId).Returns(sale);

            // Act
            await _saleService.CancelSaleAsync(saleId);

            // Assert
            sale.IsCancelled.Should().BeTrue();
            await _saleRepository.Received(1).UpdateAsync(sale);
        }

        [Fact]
        public async Task CancelSaleAsync_ShouldThrowException_WhenSaleNotFound()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            _saleRepository.GetByIdAsync(saleId).Returns((Sale)null);

            // Act
            Func<Task> act = async () => await _saleService.CancelSaleAsync(saleId);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage($"A venda com ID {saleId} não foi encontrada.");
        }
    }
}
