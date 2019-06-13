using System.Collections.Generic;

namespace Vega.Models
{
	public class QueryResultResource<T>
	{
		public int TotalVehicles { get; set; }
		public IEnumerable<TotalVehiclesPerMake> TotalVehiclesPerMake { get; set; }
		public IEnumerable<T> Items { get; set; }
	}
}