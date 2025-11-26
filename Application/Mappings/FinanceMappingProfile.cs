using AutoMapper;
using Application.DTOs.Finance.Request;
using Application.DTOs.Finance;
using Domain.Entities;

namespace Application.Mappings;

public class FinanceMappingProfile : Profile
{
    public FinanceMappingProfile()
    {
        // Income (GeneralIncome)
        CreateMap<GeneralIncome, IncomeDto>();
        CreateMap<CreateIncomeDto, GeneralIncome>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        CreateMap<UpdateIncomeDto, GeneralIncome>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Expense (GeneralExpense)
        CreateMap<GeneralExpense, ExpenseDto>();
        CreateMap<CreateExpenseDto, GeneralExpense>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        CreateMap<UpdateExpenseDto, GeneralExpense>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Overhead
        CreateMap<Overhead, OverheadDto>();
        CreateMap<CreateOverheadDto, Overhead>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? true));
        CreateMap<UpdateOverheadDto, Overhead>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
