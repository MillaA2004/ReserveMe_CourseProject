using Common.Enums;

namespace Application.Reservations.Commands
{
	using Application.Common.Services.Data;
	using Domain.Entities;
	using MediatR;
	using Shared.Requests.Reservations;

	public record CreateReservationCommand(SaveReservationRequest Data) : IRequest;

	public class CreateReservationCommandHandler
		: IRequestHandler<CreateReservationCommand>
	{
		private readonly IApplicationDbContext _context;

		public CreateReservationCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		async Task IRequestHandler<CreateReservationCommand>.Handle(CreateReservationCommand request, CancellationToken cancellationToken)
		{
			var entity = new Reservation()
			{
				UserId = request.Data.UserId,
				VenueId = request.Data.VenueId,
				TableNumber = request.Data.TableNumber,
				GuestsCount = request.Data.GuestsCount,
				ContactName = request.Data.ContactName,
				ContactPhone = request.Data.ContactPhone,
				ContactEmail = request.Data.ContactEmail,
				ReservationTime = request.Data.ReservationTime,
				Status = (ReservationStatus)request.Data.Status,
			};

			await _context.Reservations.AddAsync(entity);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
