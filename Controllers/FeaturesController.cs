using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Controllers
{
	[Route("api/[controller]")]
	public class FeaturesController : Controller
	{
		private readonly VegaDbContext context;

		public FeaturesController(VegaDbContext context)
		{
			this.context = context;
		}

		// GET: api/features
		[HttpGet]
		public async Task<IEnumerable<Feature>> Get()
		{
			var features = await context.Features.ToListAsync();

			return features;
		}
	}
}