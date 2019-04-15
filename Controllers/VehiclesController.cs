using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Models;
using Vega.Resources;

namespace Vega.Controllers
{
	[Route("api/[controller]")]
	public class VehiclesController : Controller
	{
		private readonly VegaDbContext context;
		private readonly IMapper mapper;

		public VehiclesController(VegaDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		// POST: api/vehicles
		[HttpPost]
		public IActionResult Create([FromBody] VehicleResource vehicleResource)
		{
			var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);

			return Ok(vehicle);
		}
	}
}