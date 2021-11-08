using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Temperatur_API;
using Temperatur_API.Models;

namespace Temperatur_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RoomsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Temperatures
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Rooms.ToListAsync());
        }

        private bool TemperatureExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}
