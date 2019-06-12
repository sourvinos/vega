using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Models;
using Vega.Resources;

namespace Vega.Controllers
{
	[Route("api/[controller]")]
	public class TransfersController : Controller
	{
		private readonly VegaDbContext context;
		private readonly IMapper mapper;

		public TransfersController(VegaDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		// GET: api/transfers
		[HttpGet]
		public IEnumerable<TotalPersonsPerCustomer> Get()
		{
			var details = context.Transfers.Include(x => x.Customer);

			var totalPersonsPerCustomer = context.Transfers
				.Include(x => x.Customer)
				.GroupBy(x => new { x.Customer.Name })
				.Select(x => new TotalPersonsPerCustomer
				{
					Name = x.Key.Name,
					Persons = x.Sum(s => s.Persons)
				});



			return totalPersonsPerCustomer;
		}

		public static List<Transfer> PopulateTransfers()
		{
			List<Transfer> TransferCollection = new List<Transfer>
			{
				new Transfer { Id = 1, DateIn = DateTime.Parse("2018-05-01"), DestinationId = 1, CustomerId = 1, Persons = 10 },
				new Transfer { Id = 2, DateIn = DateTime.Parse("2018-05-01"), DestinationId = 1, CustomerId = 1, Persons = 2 },
				new Transfer { Id = 3, DateIn = DateTime.Parse("2018-05-02"), DestinationId = 2, CustomerId = 2, Persons = 3 },
				new Transfer { Id = 4, DateIn = DateTime.Parse("2018-05-02"), DestinationId = 2, CustomerId = 2, Persons = 4 },
				new Transfer { Id = 5, DateIn = DateTime.Parse("2018-05-02"), DestinationId = 3, CustomerId = 3, Persons = 7 },
			};

			return TransferCollection;
		}

		public static List<Destination> PopulateDestinations()
		{
			List<Destination> DestinationCollection = new List<Destination>
			{
				new Destination { Id = 1, Name = "Paxos" },
				new Destination { Id = 2, Name = "Blue lagoon" },
				new Destination { Id = 3, Name = "Corfu Town Shopping" }
			};

			return DestinationCollection;
		}

		public static List<Customer> PopulateCustomers()
		{
			List<Customer> CustomerCollection = new List<Customer>
			{
				new Customer { Id = 1, Name = "TUI" },
				new Customer { Id = 2, Name = "Hellenic" },
				new Customer { Id = 3, Name = "TravelCo" }
			};

			return CustomerCollection;
		}

	}
}