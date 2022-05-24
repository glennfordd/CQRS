using MediatR;
using Stage2.Data;
using Stage2.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Stage2.Features.Restaurants
{
    public class Add
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
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
                var restaurant = new Restaurant() { Name = request.Name };
                _context.Restaurants.Add(restaurant);

                await _context.SaveChangesAsync();
            }
        }
    }
}
