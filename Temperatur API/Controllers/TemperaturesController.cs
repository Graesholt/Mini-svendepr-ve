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
    public class TemperaturesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TemperaturesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Temperatures
        [HttpGet("latest/{RoomName}")]
        public async Task<IActionResult> latestIndex(string RoomName)
        {
            var tempRoom = _context.Rooms.Where(d => d.RoomName == RoomName).FirstOrDefault();
            try
            {
                Temperature latestTemp = await _context.Temperatures.Where(d => d.Room == tempRoom).OrderByDescending(x => x.ID).FirstAsync();
                return Ok(latestTemp.TemperatureCentigrade);
            }
            catch
            {
                return Ok("");
            }
        }


        // GET: Temperatures
        [HttpGet("latestchart/{RoomName}")]
        public async Task<IActionResult> latestchartIndex(string RoomName)
        {
            var tempRoom = _context.Rooms.Where(d => d.RoomName == RoomName).FirstOrDefault();
            List<Temperature> latestCharTemps = await _context.Temperatures.Where(d => d.Room == tempRoom).OrderByDescending(x => x.ID).Take(600).ToListAsync();
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
                    avgTemp += latestCharTemps[(i1 * 60) + i2].TemperatureCentigrade;
                }
                avgTemp /= 60;
                latestCharTempsAverage.Add(avgTemp);
            }
            return Ok(latestCharTempsAverage);
        }

        // POST: Temperatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Temperature temperature)
        {
            if (ModelState.IsValid)
            {
                var tempRoom = _context.Rooms.Where(d => d.RoomName == temperature.Room.RoomName).FirstOrDefault();
                temperature.Time = DateTime.Now;
                temperature.Room = tempRoom;
                _context.Add(temperature);
                await _context.SaveChangesAsync();
            }
            return Ok(temperature);
        }

        private bool TemperatureExists(int id)
        {
            return _context.Temperatures.Any(e => e.ID == id);
        }
    }
}
