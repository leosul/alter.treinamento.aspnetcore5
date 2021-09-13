using alter.treinamento.api.ViewModel;
using alter.treinamento.business.Models;
using AutoMapper;

namespace alter.treinamento.api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<Product, ProductViewModel>()
                .ForMember(s => s.Width, z => z.MapFrom(s => s.Dimension.Width))
                .ForMember(s => s.Height, z => z.MapFrom(s => s.Dimension.Height))
                .ForMember(s => s.Depth, z => z.MapFrom(s => s.Dimension.Depth)).ReverseMap();
        }
    }
}
