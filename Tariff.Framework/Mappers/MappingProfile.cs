using AutoMapper;
using Tariff.Framework.Models;
using Tariff.Framework.Types;

namespace Tariff.Framework.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, TariffResult>()
                .ForMember(dest => dest.AnnualCost, opt => opt.MapFrom(o => o.AnnualCost))
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(o => true))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(o => StringResources.TariffComparisonSuccessfull)); 
        }
    }
}
