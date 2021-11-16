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

        // POST: Temperatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
            }
            return Ok(room);
        }

        private bool TemperatureExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}
