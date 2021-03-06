using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Interfaces
{
	public interface IVehicleRepository
	{
		Task<QueryResult<Vehicle>> GetVehicles(Filter filter);
		Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
		void AddVehicle(Vehicle vehicle);
		void UpdateVehicle(Vehicle vehicle);
		void DeleteVehicle(Vehicle vehicle);
	}
}