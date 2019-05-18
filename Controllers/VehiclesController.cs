using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Vega.Models;
using Vega.Resources;
using Vega.Interfaces;

namespace Vega.Controllers
{
	[Route("api/[controller]")]
	public class VehiclesController : Controller
	{
		private readonly IMapper mapper;
		private readonly IVehicleRepository vehicleRepository;
		private readonly IUnitOfWork unitOfWork;

		public VehiclesController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
		{
			this.mapper = mapper;
			this.vehicleRepository = vehicleRepository;
			this.unitOfWork = unitOfWork;
		}

		// GET: api/vehicles
		[HttpGet]
		public async Task<IEnumerable<VehicleResource>> Get()
		{
			var items = await vehicleRepository.GetVehicles();

			return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(items);
		}

		// GET: api/vehicles/1
		[HttpGet("{id}")]
		public async Task<ActionResult<VehicleResource>> GetOne(int id)
		{
			var item = await vehicleRepository.GetVehicle(id);

			if (item == null) return NotFound();

			var vehicle = mapper.Map<Vehicle, VehicleResource>(item);

			return Ok(vehicle);
		}

		// POST: api/vehicles
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] SaveVehicleResource vehicleResource)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);

			vehicle.LastUpdate = DateTime.Now;

			vehicleRepository.AddVehicle(vehicle);

			await unitOfWork.Complete();

			var item = await vehicleRepository.GetVehicle(vehicle.Id);

			var result = mapper.Map<Vehicle, VehicleResource>(item);

			return Ok(result);
		}

		// PUT: api/vehicles/1
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] SaveVehicleResource vehicleResource)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var vehicle = await vehicleRepository.GetVehicle(vehicleResource.Id);

			if (vehicle == null) return NotFound();

			mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);

			vehicle.LastUpdate = DateTime.Now;

			vehicleRepository.UpdateVehicle(vehicle);

			await unitOfWork.Complete();

			var item = await vehicleRepository.GetVehicle(vehicle.Id);

			var result = mapper.Map<Vehicle, VehicleResource>(item);

			return Ok(result);
		}

		// DELETE: api/vehicles/1
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var vehicle = await vehicleRepository.GetVehicle(id, includeRelated: false);

			if (vehicle == null) return NotFound();

			vehicleRepository.DeleteVehicle(vehicle);

			await unitOfWork.Complete();

			return Ok(id);
		}
	}
}