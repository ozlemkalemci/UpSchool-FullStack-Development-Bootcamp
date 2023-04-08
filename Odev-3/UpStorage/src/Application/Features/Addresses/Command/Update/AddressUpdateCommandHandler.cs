using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Extensions;
using MediatR;

namespace Application.Features.Addresses.Command.Update
{
    public class AddressUpdateCommandHandler : IRequestHandler<AddressUpdateCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressUpdateCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(AddressUpdateCommand request, CancellationToken cancellationToken)
        {
            var address = await _applicationDbContext.Addresses
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (address == null)
            {
                throw new NotFoundException(nameof(Addresses), request.Id);
            }

            address.Name = request.Name;
            address.District = request.District;
            address.AddressLine1 = request.AddressLine1;
            address.AddressLine2 = request.AddressLine2;
            address.AddressType = request.AddressType;
            address.PostCode = request.PostCode;
            address.CreatedOn = DateTimeOffset.Now;
            address.CreatedByUserId = null;
            address.IsDeleted = false;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return new Response<int>($"The address was successfully updated.", address.Id);

        }
    }
}
