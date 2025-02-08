using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PacificBattle.Data.ContextModels;

namespace PacificBattle.Data
{
    [Route("[controller]")]
    [ApiController]
    public class DbController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DbController(AppDbContext db) 
        {
            _db = db;
        }

        [HttpGet("/GetAllShips")]
        public async Task<ActionResult<IEnumerable<Ship>>> GetAllShips()
        {
            return await _db.Ships.ToListAsync();
        }

        [HttpGet("/GetShip/{shipId}")]
        public Task<ActionResult<Ship>> GetShip(int shipId)
        {
            return Task.FromResult<ActionResult<Ship>>(_db.Ships.Find(shipId) ?? new());
        }

        [HttpGet("/GetShipsByNavy/{navyId}")]
        public async Task<ActionResult<List<Ship>>> GetShipsByNavy(int navyId)
        {
            return await _db.Ships.Where(x => x.NavyId == navyId).ToListAsync() ?? [];
        }

        [HttpGet("/GetTestShipsByNavy/{navyId}")]
        public async Task<ActionResult<List<Ship>>> GetTestShipsByNavy(int navyId)
        {
            return await _db.Ships.Where(x => x.NavyId == navyId && x.Attack != 0).ToListAsync() ?? [];
        }
    }
}
