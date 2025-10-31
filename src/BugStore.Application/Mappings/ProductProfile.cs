using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Domain.Entities;

namespace BugStore.Application.Mappings
{
    public class ProductProfle : Profile
    {
        public ProductProfle()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
