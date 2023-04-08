using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Command.Delete
{
    public class AddressDeleteCommandHandler : IRequestHandler<AddressDeleteCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(AddressDeleteCommand request, CancellationToken cancellationToken)
        {
            var address = await _applicationDbContext.Addresses
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (address == null)
            {
                throw new NotFoundException(nameof(Addresses), request.Id);
            }

            address.CreatedOn = DateTimeOffset.Now;
            address.CreatedByUserId = null;
            address.IsDeleted = true;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return new Response<int>($"The address was successfully deleted.", address.Id);
        }
    }
}
