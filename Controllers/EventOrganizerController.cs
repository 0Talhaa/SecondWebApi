using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NewWebApi.Data;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventOrganizerController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public EventOrganizerController(ApplicationDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            var events = db.Events.Include(q => q.EventType).ToList();
            return Ok(events);
        }

        [HttpGet("EventsTypes/")]
        public IActionResult GetEventsTypes()
        {
            var eventTypes = db.EventTypes.ToList();
            return Ok(eventTypes);
        }

        [HttpPost]
        public IActionResult AddEvent(EventDTO ev)
        {
            if (ev != null)
            {
                Event newEvent = new Event()
                {
                    NoOfGuests = ev.NoOfGuests,
                    CustomerName = ev.CustomerName,
                    Date = ev.Date,
                    EventtypeId = ev.EventtypeId,
                };
                db.Events.Add(newEvent);
                db.SaveChanges();
                return Ok(newEvent);
            }
            else
            {
                return BadRequest("INVALID DATA");
            }
        }

        [HttpGet("{eventId}")]
        public IActionResult GetEvent(int eventId)
        {
            var eventItem = db.Events.Include(q => q.EventType).SingleOrDefault(j => j.Id == eventId);
            if (eventItem == null)
            {
                return NotFound("Event Not Found");
            }
            return Ok(eventItem);
        }

        [HttpPut]
        public IActionResult EditEvent(EventDTO ev)
        {
            if (ev != null)
            {
                var eventItem = db.Events.Find(ev.Id);
                if (eventItem != null)
                {
                    eventItem.NoOfGuests = ev.NoOfGuests;
                    eventItem.CustomerName = ev.CustomerName;
                    eventItem.Date = ev.Date;
                    eventItem.EventtypeId = ev.EventtypeId;

                    db.Events.Update(eventItem);
                    db.SaveChanges();
                    return Ok(eventItem);
                }
                else
                {
                    return NotFound("Event Not Found");
                }
            }
            else
            {
                return BadRequest("INVALID DATA");
            }
        }

        [HttpDelete]
        public IActionResult DeleteEvent(EventDTO ev)
        {
            if (ev != null)
            {
                var eventItem = db.Events.Find(ev.Id);
                if (eventItem != null)
                {
                    db.Events.Remove(eventItem);
                    db.SaveChanges();
                    return Ok(eventItem);
                }
                else
                {
                    return NotFound("Event Not Found");
                }
            }
            else
            {
                return BadRequest("INVALID DATA");
            }
        }

        [HttpGet("Search/{q}")]
        public IActionResult SearchEvent(string q)
        {
            if (q != null)
            {
                var Event = db.Events.Include(t => t.EventType).Where(t => t.CustomerName.Contains(q) || t.EventType.Type.Contains(q));
                if (Event != null)
                {
                    
                    return Ok(Event);
                }
                else
                {
                    return Ok("Event Not Found");
                }
            }
            else
            {
                return BadRequest("INVALID DATA");
            }
        }

        [HttpGet("Pagination/{PageNo}/{PageSize}")]
        public IActionResult Pagination(int PageNo=1, int PageSize=2)
        {
            int pageno = PageNo;
            if (pageno < 1) pageno = 1;
            int pagesize = PageSize;
            if (pagesize < 1) pagesize = 1;
            
                var getevents = db.Events.Include(i => i.EventType).Skip((pageno - 1) * pagesize).Take(pagesize)  ;
                if (getevents != null)
                {

                    return Ok(getevents);
                }
                else
                {
                    return Ok("Event Not Found");
                }
            }
        }
}
