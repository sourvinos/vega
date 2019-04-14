using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Models;
using Vega.Resources;

namespace Vega.Controllers
{
	[Route("api/[controller]")]
	public class MakesController : Controller
	{
		private readonly VegaDbContext context;
		private readonly IMapper mapper;

		public MakesController(VegaDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		// GET: api/makes
		[HttpGet]
		public async Task<IEnumerable<MakeResource>> Get()
		{
			var makes = await context.Makes.Include(m => m.Models).ToListAsync();

			return mapper.Map<List<Make>, List<MakeResource>>(makes);
		}
	}
}