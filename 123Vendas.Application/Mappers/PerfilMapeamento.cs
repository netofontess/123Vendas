using _123Vendas.Domain.Dtos;
using _123Vendas.Domain.Entities;
using AutoMapper;

namespace _123Vendas.Application.Mappers
{
    public class PerfilMapeamento : Profile
    {
        public PerfilMapeamento()
        {
            CreateMap<SaleDto, Sale>().ReverseMap();
            CreateMap<SaleItemDto, SaleItem>().ReverseMap();
        }
    }
}
