using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody] int gigId)
        {
            var userId = User.Identity.GetUserId();

            var exists = _context.Attendaces.Any(a => a.AttendeeId == userId && a.GigId == gigId);

            if (exists)
                return BadRequest("The attendace already exists");

            var attendace = new Attendace
            {
                GigId = gigId,
                AttendeeId = userId
            };

            _context.Attendaces.Add(attendace);

            _context.SaveChanges();

            return Ok();    
        }
    }
}
