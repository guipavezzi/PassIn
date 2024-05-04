using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Interfaces.Attendees
{
    public interface IAttendeesRepository
    {
        Task<Attendee> Add(Attendee entity);
    }
}
