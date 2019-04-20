using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Resources
{
	// Will be used for reading
	public class VehicleResource
	{
		public int Id { get; set; }
		public KeyValuePairResource Make { get; set; }
		public KeyValuePairResource Model { get; set; }
		public bool IsRegistered { get; set; }
		public ContactResource Contact { get; set; }
		public DateTime LastUpdate { get; set; }

		public ICollection<KeyValuePairResource> Features { get; set; }

		public VehicleResource()
		{
			Features = new Collection<KeyValuePairResource>();
		}
	}
}