using Application.Common.Services.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.Tables;

namespace Application.Tables.Commands
{
    public record UpdateTableCommand(TableDto Table) : IRequest<Unit>;

    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTableCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tables
                .FirstOrDefaultAsync(t => t.Id == request.Table.Id, cancellationToken);

            if (entity == null)
            {
                // Handle not found
                return Unit.Value;
            }

            entity.TableNumber = request.Table.TableNumber;
            entity.Capacity = request.Table.Capacity;
            entity.IsActive = request.Table.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
