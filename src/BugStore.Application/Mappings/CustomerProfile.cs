using AutoMapper;
using BugStore.Application.Dtos;
using BugStore.Domain.Entities;

namespace BugStore.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
