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
    public class MoisturesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MoisturesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Moistures
        [HttpGet("latest/{RoomName}")]
        public async Task<IActionResult> latestIndex(string RoomName)
        {
            var tempRoom = _context.Rooms.Where(d => d.RoomName == RoomName).FirstOrDefault();
            Moisture latestTemp = await _context.Moistures.Where(d => d.Room == tempRoom).OrderByDescending(x=>x.ID).FirstAsync();
            return Ok(latestTemp.MoistureValue);
        }


        // GET: Moistures
        [HttpGet("latestchart/{RoomName}")]
        public async Task<IActionResult> latestchartIndex(string RoomName)
        {
            var tempRoom = _context.Rooms.Where(d => d.RoomName == RoomName).FirstOrDefault();
            List<Moisture> latestCharTemps = await _context.Moistures.Where(d => d.Room == tempRoom).OrderByDescending(x => x.ID).Take(600).ToListAsync();
            latestCharTemps.Reverse();
            List<float> latestCharTempsAverage = new List<float>();
            for (int i1 = 0; i1 < 10; i1++)
            {
                float avgTemp = 0;
                for (int i2 = 0; i2 < 60; i2++)
                {
                    if (((i1 * 60) + i2) >= latestCharTemps.Count())
                    {
                        avgTemp /= i2 + 1;
                        latestCharTempsAverage.Add(avgTemp);
                        return Ok(latestCharTempsAverage);
                    }
                    avgTemp += latestCharTemps[(i1 * 60) + i2].MoistureValue;
                }
                avgTemp /= 60;
                latestCharTempsAverage.Add(avgTemp);
            }
            return Ok(latestCharTempsAverage);
        }

        // POST: Moistures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Moisture Moisture)
        {
            if (ModelState.IsValid)
            {
                var tempRoom = _context.Rooms.Where(d => d.RoomName == Moisture.Room.RoomName).FirstOrDefault();
                Moisture.Time = DateTime.Now;
                Moisture.Room = tempRoom;
                _context.Add(Moisture);
                await _context.SaveChangesAsync();
            }
            return Ok(Moisture);
        }

        private bool MoistureExists(int id)
        {
            return _context.Moistures.Any(e => e.ID == id);
        }
    }
}
