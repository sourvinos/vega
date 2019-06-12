using System;

namespace Vega.Models
{
	public class TransferDetail
	{
		public int Id { get; set; }
		public DateTime DateIn { get; set; }
		public Destination Destination { get; set; }
		public Customer Customer { get; set; }
		public int Persons { get; set; }
	}
}