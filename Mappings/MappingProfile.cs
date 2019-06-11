using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Vega.Models;
using Vega.Resources;

namespace Vega.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// From Domain To API 
			CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
			CreateMap<Make, MakeResource>();
			CreateMap<Model, KeyValuePairResource>();
			CreateMap<Feature, KeyValuePairResource>();
			CreateMap<Vehicle, SaveVehicleResource>()
				.ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
				.ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
			CreateMap<Vehicle, VehicleResource>()
				.ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
				.ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
				.ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource { Id = vf.Feature.Id, Name = vf.Feature.Name })));

			// From API To Domain
			CreateMap<FilterResource, Filter>();
			CreateMap<SaveVehicleResource, Vehicle>()
				.ForMember(v => v.Id, opt => opt.Ignore())
				.ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
				.ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
				.ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
				.ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature { FeatureId = id })))
				.AfterMap((vr, v) =>
				{
					// Find and store the features in the Domain that are not in the API
					var removedFeatures = v.Features.Where(x => !vr.Features.Contains(x.FeatureId));
					// Remove the features from the Domain
					foreach (var x in removedFeatures)
						v.Features.Remove(x);

					// Find and store the features in the API that are not in the Domain
					var addedFeatures = vr.Features.Where(id => !v.Features.Any(x => x.FeatureId == id));
					// Add the new features in the Domain
					foreach (var x in addedFeatures)
						v.Features.Add(new VehicleFeature
						{
							FeatureId = x
						});
				});
		}
	}
}