using Application.Common.Services.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Requests.Venues;

namespace Application.Venues.Commands
{
    public record UpdateVenueCommand(int VenueId, SaveVenueRequest Data) : IRequest;

    public class UpdateVenueCommandHandler : IRequestHandler<UpdateVenueCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateVenueCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Venues
                .FirstOrDefaultAsync(v => v.Id == request.VenueId, cancellationToken);

            if (entity == null)
            {
                return;
            }

            entity.Name = request.Data.Name;
            entity.Description = request.Data.Description;
            entity.VenueTypeId = request.Data.VenueTypeId;
            entity.Latitude = request.Data.Latitude;
            entity.Longitude = request.Data.Longitude;
            entity.LogoUrl = request.Data.LogoUrl;
            entity.ImageUrl = request.Data.ImageUrl;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
