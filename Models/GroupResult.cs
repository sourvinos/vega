using System.Collections.Generic;

namespace Vega.Models
{
	public class GroupResult<T>
	{
		public IEnumerable<Transfer> Items { get; set; }

		public IEnumerable<TotalPersonsPerCustomer> TotalPersonsPerCustomer { get; set; }
		public IEnumerable<TotalPersonsPerDestination> TotalPersonsPerDestination { get; set; }
	}
}