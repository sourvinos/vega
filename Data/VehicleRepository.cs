using System.Collections.Generic;
using System.Linq;
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

		public async Task<QueryResult<Vehicle>> GetVehicles(Filter filter)
		{
			var result = new QueryResult<Vehicle>();

			var items = context.Vehicles
				.Include(v => v.Features)
					.ThenInclude(vf => vf.Feature)
				.Include(v => v.Model)
					.ThenInclude(m => m.Make)
				.OrderBy(o => o.Model.Make.Name)
					.ThenBy(o => o.Model.Name)
				.AsNoTracking();

			if (filter.MakeId.HasValue)
			{
				items = items.Where(x => x.Model.MakeId == filter.MakeId.Value);
			}

			result.TotalItems = items.Count();
			result.Items = await items.ToListAsync();

			return result;
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