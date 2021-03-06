using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCode_WebApplication.Domain.Models
{
    public class Owner : Profile
    {
        public override int Id { get; set; }
        public override string UserName { get; set; }
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override string Cellphone { get; set; }
        public override string Email { get; set; }
        public override string Password { get; set; }
        public long Ruc { get; set; }

        public Restaurant Restaurant { get; set; }
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}