using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDeploy.Api.Data;
using TestDeploy.Api.Entities;

namespace TestDeploy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThingsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ThingsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Thing>>> Things()
        {
            var things = await _db.Things.ToListAsync();

            return Ok(things);
        }
    }
}