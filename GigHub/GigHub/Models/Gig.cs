using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendace> Attendaces { get; private set; }

        public Gig()
        {
            Attendaces = new Collection<Attendace>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = new Notification(NotificationType.GigCanceled, this);

            foreach (var attendee in /*attendees*/ Attendaces.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
    
}