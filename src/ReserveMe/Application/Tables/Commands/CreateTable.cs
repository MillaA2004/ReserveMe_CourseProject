using Application.Common.Services.Data;
using Domain.Entities;
using MediatR;
using Shared.Dtos.Tables;

namespace Application.Tables.Commands
{
    public record CreateTableCommand(TableDto Table) : IRequest<int>;

    public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTableCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            var entity = new Table
            {
                VenueId = request.Table.VenueId,
                TableNumber = request.Table.TableNumber,
                Capacity = request.Table.Capacity,
                IsActive = request.Table.IsActive
            };

            _context.Tables.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
