using MediatR;
using Microsoft.EntityFrameworkCore;
using Stage2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stage2.Features.Guests
{
    public class GetAll
    {
        public class Query : IRequest<IEnumerable<Model>>
        {

        }

        public class Handler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly ReservationContext _context;
            public Handler(ReservationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var guests = await _context.Guests.Include(p => p.Bookings)
                    .Select(p => new Model
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Email = p.Email,
                        LastBooking = p.Bookings.Max(p => p.BookingDateTime)
                    }).ToListAsync();

                return guests;
            }
        }

        public class Model
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime? LastBooking { get; set; }
        }
    }
}
