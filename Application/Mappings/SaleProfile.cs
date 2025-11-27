using AutoMapper;
using Application.DTOs.Sales;
using Domain.Entities;

namespace Application.Mappings
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.SaleDetails));

            CreateMap<SaleDetail, SaleDetailDto>();
        }
    }
}
