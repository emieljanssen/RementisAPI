using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public IActionResult GetAll()
        {

            //GET all profiles and their agenda items
            var profile = _context.Profile.FirstOrDefault();
            var agendaitem = _context.Agendadata.FirstOrDefault(t => t.CostumerId == profile.CustomerId);
            var allprofiles = _context.Profile.ToList();
            var items = _context.Agendadata.ToList();
            var Time = DateTime.Now.TimeOfDay;
            DateTime thisDay = DateTime.Today;

            //Check if agenda item failed
            foreach (Agendadata a in items) {
                if (a.EndTime < Time && a.EndDate < thisDay && a.State != "completed")
                {
                    a.State = "failed";
                    _context.Agendadata.Update(a);
                    _context.SaveChanges();
                }
                else
                {
                    //already completed
                }
            }



            //create the body with all profiles
            Object Profiles =
                new JObject(
                    new JProperty("Profile",
                        new JArray(
                            from p in allprofiles
                            orderby p.CustomerId
                            select new JObject(
                                new JProperty("Id", p.CustomerId),
                                new JProperty("FirstName", p.Voornaam),
                                new JProperty("LastName", p.Achternaam),
                                new JProperty("Gender", p.Geslacht),
                                new JProperty("Agenda",
                                    new JArray(
                                        from a in items
                                        where a.CostumerId == p.CustomerId
                                        group a by a.StartDate
                                        into g
                                        orderby g.Count() descending
                                        select new JObject(
                                            new JProperty("Date", g.FirstOrDefault().StartDate.ToString("MM-dd-yyyy").Replace('-','/')),
                                            new JProperty("Items",
                                                new JArray(
                                                    from i in g
                                                    where i.CostumerId == p.CustomerId
                                                    orderby i.StartTime
                                                    select new JObject(
                                                        new JProperty("Title", i.Title),
                                                        new JProperty("CustomerId", i.CostumerId),
                                                        new JProperty("Description", i.Description),
                                                        new JProperty("StartDate", i.StartDate),
                                                        new JProperty("EndDate", i.EndDate),
                                                        new JProperty("StartTime", i.StartTime),
                                                        new JProperty("EndTime", i.EndTime),
                                                        new JProperty("Priority", i.Priority),
                                                        new JProperty("State", i.State)
                                                    )
                                                )
                                            )
                                         )
                                     )
                                )
                           )
                         )
                     )
                  );

//            profiles: [
//    {
//      customerId: 1,
//      voornaam: "Herbert",
//      achternaam: "Kartoffelsalat",
//      geslacht: "male",
//      agenda: [
//        {
//                    date: "11-01-18",
//          items: [
//            {
//                        messageId: 1,
//              title: "Medicatie innemen",
//              customerId: 1,
//              description: "Neem 5 paracetamol en 2 ibuprofen",
//              startTime: "11:00",
//              endTime: "11:00",
//              priority: true,
//              state: agendaPointStatesEnum.failed
//            },
//            {
//                        messageId: 4,
//              title: "Medicatie innemen",
//              customerId: 1,
//              description: "Neem 5 paracetamol en 2 ibuprofen",
//              startTime: "11:00",
//              endTime: "11:00",
//              priority: true,
//              state: agendaPointStatesEnum.failed
//            },
//          ]
//        }
//      ]
//    }
//  ]
//
//  failed: "failed",
//  pending: "pending",
//  completed: "completed"
//  male: "male",
//  female: "female",
            


            return new JsonResult(Profiles);
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
