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
		public GroupResult<Transfer> Get()
		{
			var details = context.Transfers.Include(x => x.Customer).Include(x => x.Destination);

			var groupResult = new GroupResult<Transfer>();

			var totalPersonsPerCustomer = context.Transfers
				.Include(x => x.Customer)
				.GroupBy(x => new { x.Customer.Name })
				.Select(x => new TotalPersonsPerCustomer
				{
					Name = x.Key.Name,
					Persons = x.Sum(s => s.Persons)
				});

			var TotalPersonsPerDestination = context.Transfers
				.Include(x => x.Destination)
				.GroupBy(x => new { x.Destination.Name })
				.Select(x => new TotalPersonsPerDestination
				{
					DestinationName = x.Key.Name,
					Persons = x.Sum(s => s.Persons)
				});

			groupResult.Items = details.ToList();
			groupResult.TotalPersonsPerCustomer = totalPersonsPerCustomer.ToList();
			groupResult.TotalPersonsPerDestination = TotalPersonsPerDestination.ToList();

			return groupResult;
		}

	}
}