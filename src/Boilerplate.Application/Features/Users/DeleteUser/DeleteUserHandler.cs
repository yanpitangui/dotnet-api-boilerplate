using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, bool>
{
    private readonly IContext _context;

    public DeleteUserHandler(IContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        // TODO: This can cause an exception. Replace this with Oneof<bool, notFound>.
        _context.Users.Remove(user!);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}