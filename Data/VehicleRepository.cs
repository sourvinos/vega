using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vega.Interfaces;
using Vega.Models;
using Vega.Resources;

namespace Vega.Data
{
	public class VehicleRepository : IVehicleRepository
	{
		private readonly VegaDbContext context;

		public VehicleRepository(VegaDbContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<Vehicle>> GetVehicles(bool includeRelated = true)
		{
			return await context.Vehicles.Include(v => v.Features).ThenInclude(vf => vf.Feature).Include(v => v.Model).ThenInclude(m => m.Make).AsNoTracking().ToListAsync();
		}

		public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
		{
			if (!includeRelated) return await context.Vehicles.SingleAsync(v => v.Id == id);

			return await context.Vehicles.Include(v => v.Features).ThenInclude(vf => vf.Feature).Include(v => v.Model).ThenInclude(m => m.Make).SingleOrDefaultAsync(v => v.Id == id);
		}

		public void AddVehicle(Vehicle vehicle)
		{
			context.Vehicles.Add(vehicle);
		}

		public async void UpdateVehicle(Vehicle vehicle)
		{
			await context.SaveChangesAsync();
		}

		public async void DeleteVehicle(Vehicle vehicle)
		{
			context.Vehicles.Remove(vehicle);

			await context.SaveChangesAsync();
		}

	}
}