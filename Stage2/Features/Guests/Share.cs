using MediatR;
using Stage2.Data;
using Stage2.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stage2.Features.Guests
{
    public class Share
    {
        public class Command : IRequest
        {
            public int BookingId { get; set; }
            public int HostId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly ReservationContext _context;

            public Handler(ReservationContext context)
            {
                _context = context;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var booking = _context.Guests
                 .Where(p => p.Id == request.HostId)
                 .SelectMany(p => p.Bookings)
                 .Where(p => p.Id == request.BookingId)
                 .FirstOrDefault();

                var invitedGuest = _context.Guests
                    .Where(p => p.Email == request.Email)
                    .FirstOrDefault();

                if (invitedGuest == null)
                {
                    invitedGuest = new Guest()
                    {
                        Name = request.Name,
                        Email = request.Email,
                    };

                    invitedGuest.Bookings.Add(booking);
                    
                    _context.Guests.Add(invitedGuest);
                }
                else
                {
                    invitedGuest.Bookings.Add(booking);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
