using Application.Common.Services.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tables.Commands
{
    public record DeleteTableCommand(int TableId) : IRequest<Unit>;

    public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTableCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tables
                .FirstOrDefaultAsync(t => t.Id == request.TableId, cancellationToken);

            if (entity != null)
            {
                _context.Tables.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
