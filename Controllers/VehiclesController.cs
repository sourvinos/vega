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
	public class VehiclesController : Controller
	{
		private readonly VegaDbContext context;
		private readonly IMapper mapper;

		public VehiclesController(VegaDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		// GET: api/vehicles
		[HttpGet]
		public async Task<IEnumerable<VehicleResource>> Get()
		{
			var vehicles = await context.Vehicles.Include(x => x.Features).ToListAsync();

			return mapper.Map<List<Vehicle>, List<VehicleResource>>(vehicles);
		}

		// POST: api/vehicles
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] VehicleResource vehicleResource)
		{
			var item = mapper.Map<VehicleResource, Vehicle>(vehicleResource);

			item.LastUpdate = DateTime.Now;

			context.Vehicles.Add(item);
			await context.SaveChangesAsync();

			var vehicle = mapper.Map<Vehicle, VehicleResource>(item);

			return Ok(vehicle);
		}
	}
}