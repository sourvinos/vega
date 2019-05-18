using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Controllers
{
	[Route("api/[controller]")]
	public class InventoryController : Controller
	{
		private readonly VegaDbContext context;

		public InventoryController(VegaDbContext context)
		{
			this.context = context;
		}

		// GET: api/inventory
		[HttpGet]
		public async Task<IEnumerable<Inventory>> Get()
		{
			var items = await context.Inventory.ToListAsync();

			return (items);
		}

	}
}