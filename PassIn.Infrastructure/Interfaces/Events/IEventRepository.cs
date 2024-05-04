using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Interfaces.Events
{
    public interface IEventRepository
    {
        Task<Event> Add(Event entity);
        Task<Event> GetEventById(Guid id);
    }
}
