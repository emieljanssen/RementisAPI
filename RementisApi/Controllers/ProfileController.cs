using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RementisApi.Models;

namespace RementisApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Profile")]
    public class ProfileController : Controller
    {

        private readonly SensordataContext _context;

        public ProfileController(SensordataContext context)
        {
            _context = context;

            if (_context.Profile.Count() == 0)
            {
                _context.Profile.Add(new Profile { CustomerId = 1 });
                _context.SaveChanges();
            }
        }

        // GET: api/Profile
        [HttpGet]
        public IEnumerable<Profile> GetAll()
        {
            return _context.Profile.ToList();
        }

        // GET: api/Profile/5
        [HttpGet("{id}", Name = "GetProfile")]
        public IActionResult GetById(long id)
        {
            var item = _context.Profile.FirstOrDefault(t => t.CustomerId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST: api/Profile
        [HttpPost]
        public IActionResult Create([FromBody] Profile item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Profile.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetProfile", new { id = item.CustomerId }, item);
        }

        // PUT: api/Profile/5
        [HttpPut]
        public IActionResult Update([FromBody] Profile item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var profile = _context.Profile.FirstOrDefault(t => t.CustomerId == item.CustomerId);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profile.Update(profile);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var profile = _context.Profile.FirstOrDefault(t => t.CustomerId == id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profile.Remove(profile);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
