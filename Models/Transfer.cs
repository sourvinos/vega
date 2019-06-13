using System;

namespace Vega.Models
{
	public class Transfer
	{
		public int Id { get; set; }
		public DateTime DateIn { get; set; }
		public int DestinationId { get; set; }
		public int CustomerId { get; set; }
		public int Persons { get; set; }

		public Customer Customer { get; set; }
		public Destination Destination { get; set; }
	}
}