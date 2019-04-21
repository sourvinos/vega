using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Interfaces;
using Vega.Models;

namespace Vega.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly VegaDbContext context;

		public UnitOfWork(VegaDbContext context)
		{
			this.context = context;
		}

		public async Task Complete()
		{
			await context.SaveChangesAsync();
		}
	}
}