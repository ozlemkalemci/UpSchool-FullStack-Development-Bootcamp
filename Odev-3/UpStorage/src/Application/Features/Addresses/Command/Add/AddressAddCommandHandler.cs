using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Extensions;
using MediatR;

namespace Application.Features.Addresses.Command.Add
{
    public class AddressAddCommandHandler : IRequestHandler<AddressAddCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public async Task<Response<int>> Handle(AddressAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.Name.IsContainsChar(3))
            {
                throw new Exception();
            }

            var address = new Address()
            {
                Name = request.Name,
                UserId = request.UserId,
                CountryId = request.CountryId,
                CityId = request.CityId,
                District = request.District,
                PostCode = request.PostCode,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                AddressType = request.AddressType,
                CreatedOn = DateTimeOffset.Now,
                CreatedByUserId = null,
                IsDeleted = false,
            };

            await _applicationDbContext.Addresses.AddAsync(address, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<int>($"The new address \"{address.Name}\"named was succesfully added.", address.Id);

        }
    }
}
