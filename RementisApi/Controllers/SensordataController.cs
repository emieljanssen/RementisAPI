using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RementisApi.Models;


namespace RementisApi.Controllers
{
    [Route("api/[controller]")]
    public class SensordataController : Controller
    {

        private readonly SensordataContext _context;

        public SensordataController(SensordataContext context)
        {
            _context = context;

            if (_context.SensorItems.Count() == 0)
            {
                _context.SensorItems.Add(new SensorItem { SensorId = "testsensor" });
                _context.SaveChanges();
            }
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<SensorItem> GetAll()
        {
            return _context.SensorItems.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetSensordata")]
        public IActionResult GetById(long id)
        {
            var item = _context.SensorItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] SensorItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }


            //check of er een status van agendaitem moet worden ge-update
            //verander timestamp, starttime en endtime naar Time value!
            var agendaitem = _context.Agendadata.FirstOrDefault(t => t.StartTime < item.Timestamp && t.EndTime > item.Timestamp);
            agendaitem.State = "succes";

            //Geef sensoritem een id
            var sensoritemhighid = _context.SensorItems.LastOrDefault();
            item.Id = sensoritemhighid.Id +1;

            _context.Agendadata.Update(agendaitem);
            _context.SensorItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetSensordata", new { id = item.Id }, item);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody] SensorItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var sensordata = _context.SensorItems.FirstOrDefault(t => t.Id == item.Id);
            if (sensordata == null)
            {
                return NotFound();
            }

            sensordata.CostumerId = item.CostumerId;
            sensordata.SensorId = item.SensorId;
            sensordata.Timestamp = item.Timestamp;
            sensordata.Value = item.Value;

            _context.SensorItems.Update(sensordata);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var sensordata = _context.SensorItems.FirstOrDefault(t => t.Id == id);
            if (sensordata == null)
            {
                return NotFound();
            }

            _context.SensorItems.Remove(sensordata);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
