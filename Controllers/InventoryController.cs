using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

		// GET: api/inventory/getAll
		[HttpGet]
		[Route("getAll")]
		public async Task<IEnumerable<Inventory>> Get()
		{
			var items = await context.Inventory.ToListAsync();

			return (items);
		}


		// GET: api/inventory/getByName/orange
		[HttpGet]
		[Route("getByName/{fruit}")]
		public async Task<IEnumerable<Inventory>> GetByName(string fruit)
		{
			var items = await context.Inventory.Where(s => s.Name == fruit).ToListAsync();

			return (items);
		}

		// GET: api/inventory/getByDate/2019-05-12
		[HttpGet]
		[Route("getByDate/{date}")]
		public async Task<IEnumerable<Inventory>> GetByDate(DateTime date)
		{
			var items = await context.Inventory.Where(s => s.Date >= date).ToListAsync();

			return (items);
		}




	}
}