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
    public class Get
    {
        public class Query : IRequest<Model>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly ReservationContext _context;
            public Handler(ReservationContext context)
            {
                _context = context;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                var guest = await _context.Guests.Include(p => p.Bookings)
                    .Where(p => p.Id == request.Id)
                    .Select(p => new Model()
                    {
                        Id = request.Id,
                        Name = p.Name,
                        Email = p.Email,
                        Bookings = p.Bookings.Select(b => new Model.Booking()
                        {
                            BookingDateTime = b.BookingDateTime,
                        })
                    })
                    .FirstOrDefaultAsync();

                if (guest == null)
                {
                    throw new ArgumentNullException($"Guest {request.Id} not found.");
                }

                return guest;
            }
        }

        public class Model
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }

            public IEnumerable<Booking> Bookings { get; set; }

            public class Booking
            {
                public DateTime BookingDateTime { get; set; }
            }
        }
    }
}
