using AutoMapper;
using Vega.Models;
using Vega.Resources;

namespace Vega.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Make, MakeResource>();
			CreateMap<Model, ModelResource>();
		}
	}
}