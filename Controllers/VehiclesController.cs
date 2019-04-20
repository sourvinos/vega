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
		public async Task<ActionResult<IEnumerable<VehicleResource>>> Get()
		{
			var vehicles = await context.Vehicles.Include(x => x.Features).ToListAsync();

			return mapper.Map<List<Vehicle>, List<VehicleResource>>(vehicles);
		}

		// GET: api/vehicles/1
		[HttpGet("{id}")]
		public async Task<ActionResult<VehicleResource>> GetOne(int id)
		{
			var item = await context.Vehicles
				.Include(v => v.Features)
					.ThenInclude(vf => vf.Feature)
				.Include(v => v.Model)
					.ThenInclude(m => m.Make)
				.SingleOrDefaultAsync(v => v.Id == id);

			if (item == null) return NotFound();

			var vehicle = mapper.Map<Vehicle, VehicleResource>(item);

			return Ok(vehicle);
		}

		// POST: api/vehicles
		[HttpPost]
		public async Task<ActionResult> Create([FromBody] SaveVehicleResource vehicleResource)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var item = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);

			item.LastUpdate = DateTime.Now;

			context.Vehicles.Add(item);

			await context.SaveChangesAsync();

			var vehicle = mapper.Map<Vehicle, SaveVehicleResource>(item);

			return Ok(vehicle);
		}

		// PUT: api/vehicles/1
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] VehicleResource vehicleResource)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var item = await context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);

			if (item == null) return NotFound();

			mapper.Map<VehicleResource, Vehicle>(vehicleResource, item);

			item.LastUpdate = DateTime.Now;

			await context.SaveChangesAsync();

			var vehicle = mapper.Map<Vehicle, VehicleResource>(item);

			return Ok(vehicle);
		}

		// DELETE: api/vehicles/1
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var item = await context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);

			if (item == null) return NotFound();

			context.Vehicles.Remove(item);

			await context.SaveChangesAsync();

			return Ok(id);
		}

	}
}