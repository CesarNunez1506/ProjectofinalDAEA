using AutoMapper;
using Application.DTOs.Stores;
using Domain.Entities;

namespace Application.Mappings
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<Store, StoreDto>();
        }
    }
}
