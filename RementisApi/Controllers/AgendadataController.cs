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
    [Route("api/Agendadata")]
    public class AgendadataController : Controller
    {
        private readonly SensordataContext _context;

        public AgendadataController(SensordataContext context)
        {
            _context = context;

            if (_context.Agendadata.Count() == 0)
            {
                _context.Agendadata.Add(new Agendadata { Description = "test" });
                _context.SaveChanges();
            }
        }


        // GET: api/Agendadata
        [HttpGet]
        public IEnumerable<Agendadata> Getall()
        {
            return _context.Agendadata.ToList();
        }

        // GET: api/Agendadata/5
        [HttpGet("{id}", Name = "GetAgendadata")]
        public IActionResult GetById(long id)
        {
            var item = _context.Agendadata.FirstOrDefault(t => t.MessageId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Agendadata item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Agendadata.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetAgendadata", new { id = item.MessageId }, item);
        }

        // PUT: api/Agendadata/5
        [HttpPut]
        public IActionResult Update([FromBody] Agendadata item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var agendadata = _context.Agendadata.FirstOrDefault(t => t.MessageId == item.MessageId);
            if (agendadata == null)
            {
                return NotFound();
            }

            //update alle values

            _context.Agendadata.Update(agendadata);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var agendadata = _context.Agendadata.FirstOrDefault(t => t.MessageId == id);
            if (agendadata == null)
            {
                return NotFound();
            }

            _context.Agendadata.Remove(agendadata);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
