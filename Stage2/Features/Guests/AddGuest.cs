using MediatR;
using Stage2.Data;
using Stage2.Domain;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Stage2.Features.Guests
{
    public class AddGuest
    {
        public class Command : IRequest
        {
            [Required]
            public string Name { get; set; }

            [Required]
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
                var guest = new Guest()
                {
                    Name = request.Name,
                    Email = request.Email
                };

                _context.Guests.Add(guest);

                await _context.SaveChangesAsync();
            }
        }
    }
}
