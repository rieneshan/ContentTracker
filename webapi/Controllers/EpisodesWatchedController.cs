using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EpisodesWatchedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EpisodesWatchedController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetSeriesWatched")]
        public async Task<ActionResult<IEnumerable<SeriesWatched>>> GetSeriesWatched()
        {
            if (_context.seriesWatched == null)
            {
                return NotFound();
            }

            var user = HttpContext.User.Identity.Name;
            return await _context.seriesWatched.Where(m => m.username == user).ToListAsync();
        }

        
        [Authorize]
        [HttpGet("GetEpisodesWatched")]
        public async Task<ActionResult<IEnumerable<EpisodesWatched>>> GetepisodesWatched(string name)
        {
          if (_context.episodesWatched == null)
          {
              return NotFound();
          }

            var user = HttpContext.User.Identity.Name;
            return await _context.episodesWatched.Where(m => m.username == user && m.seriesName == name).ToListAsync();
        }

        [Authorize]
        [HttpPost("AddEpisodesWatched")]
        public async Task<bool> AddEpisodesWatched([FromBody] EpisodesWatched episodesWatched)
        {
          if (_context.episodesWatched == null)
          {
              return false;
          }
            var user = HttpContext.User.Identity.Name;

            episodesWatched.username = user;
            episodesWatched.insertdate = DateTime.Now;

            _context.episodesWatched.Add(episodesWatched);
            await _context.SaveChangesAsync();

            return true;
        }

        [Authorize]
        [HttpPost("AddSeriesWatched")]
        public async Task<bool> AddSeriesWatched(string name)
        {
            if (_context.episodesWatched == null)
            {
                return false;
            }
            var user = HttpContext.User.Identity.Name;

            SeriesWatched seriesWatched = new SeriesWatched();
            seriesWatched.username = user;
            seriesWatched.insertdate = DateTime.Now;
            seriesWatched.seriesName = name;

            _context.seriesWatched.Add(seriesWatched);
            await _context.SaveChangesAsync();

            return true;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisodesWatched(int id)
        {
            if (_context.episodesWatched == null)
            {
                return NotFound();
            }
            var episodesWatched = await _context.episodesWatched.FindAsync(id);
            if (episodesWatched == null)
            {
                return NotFound();
            }

            _context.episodesWatched.Remove(episodesWatched);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpisodesWatchedExists(int id)
        {
            return (_context.episodesWatched?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
