using System.Linq;
using AutoMapper;
using Vega.Models;
using Vega.Resources;

namespace Vega.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// Domain to API
			CreateMap<Make, MakeResource>();
			CreateMap<Model, ModelResource>();
			CreateMap<Feature, FeatureResource>();

			// API to Domain
			CreateMap<VehicleResource, Vehicle>()
				.ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
				.ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
				.ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
				.ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature { FeatureId = id })));

		}
	}
}