using Application.DTOs.Sales;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class SalesProfile : Profile
{
    public SalesProfile()
    {
        CreateMap<Sale, SaleDto>()
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

        CreateMap<SaleDetail, SaleDetailDto>();

        CreateMap<Store, StoreDto>();
    }
}
