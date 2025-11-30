using AutoMapper;
using Application.DTOs.Finance;
using Application.DTOs.Finance.Request;
using Domain.Entities;

namespace Application.Mappings;

public class FinanceMappingProfile : Profile
{
    public FinanceMappingProfile()
    {
        // --------------------
        // General Income
        // --------------------
        CreateMap<GeneralIncome, IncomeDto>();
        CreateMap<CreateIncomeDto, GeneralIncome>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        CreateMap<UpdateIncomeDto, GeneralIncome>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // --------------------
        // General Expense
        // --------------------
        CreateMap<GeneralExpense, ExpenseDto>();
        CreateMap<CreateExpenseDto, GeneralExpense>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        CreateMap<UpdateExpenseDto, GeneralExpense>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // --------------------
        // Overhead
        // --------------------
        CreateMap<Overhead, OverheadDto>();
        CreateMap<CreateOverheadDto, Overhead>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? true));
        CreateMap<UpdateOverheadDto, Overhead>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // --------------------
        // Monastery Expense
        // --------------------
        CreateMap<MonasteryExpense, MonasteryExpenseDto>();
        CreateMap<CreateMonasteryExpenseDto, MonasteryExpense>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        CreateMap<UpdateMonasteryExpenseDto, MonasteryExpense>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // --------------------
        // Financial Report
        // --------------------
        CreateMap<FinancialReport, FinancialReportDto>();
        CreateMap<CreateFinancialReportDto, FinancialReport>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        CreateMap<UpdateFinancialReportDto, FinancialReport>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
