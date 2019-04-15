using System.ComponentModel.DataAnnotations;
using System;
using Vega.Models;

namespace Vega
{
	public class Vehicle
	{
		public int Id { get; set; }
		public int ModelId { get; set; }
		public Model Model { get; set; }
		public bool IsRegistered { get; set; }
		[Required]
		[MaxLength(255)]
		public string ContactName { get; set; }
		public string ContactNumber { get; set; }
		public string ContactPhone { get; set; }
		public DateTime LastUpdate { get; set; }
	}
}