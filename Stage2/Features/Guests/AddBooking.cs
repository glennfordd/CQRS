using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stage2.Data;
using Stage2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stage2.Features.Guests
{
    public class AddBooking
    {
        public class Query : IRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IRequest
        {
            public int GuestId { get; set; }
            public IEnumerable<SelectListItem> Restaurants { get; set; }
            public int SelectedRestaurantId { get; set; }

            public DateTime BookingDateTime { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly ReservationContext _context;

            public QueryHandler(ReservationContext context)
            {
                _context = context;
            }

            public async Task<Command> Handle(Query request, CancellationToken cancellationToken)
            {
                var model = new Command()
                {
                    GuestId = request.Id,
                    BookingDateTime = DateTime.Now,
                    Restaurants = await _context.Restaurants.Select(p => new SelectListItem()
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    }).ToListAsync()
                };

                return model;
            }
        }

        public class CommandHandler : AsyncRequestHandler<Command>
        {
            private readonly ReservationContext _context;

            public CommandHandler(ReservationContext context)
            {
                _context = context;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var guest = _context.Guests.Find(request.GuestId);
                var booking = new Booking()
                {
                    BookingDateTime = request.BookingDateTime,
                    RestaurantId = request.SelectedRestaurantId,
                };

                guest.Bookings.Add(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
