﻿namespace Application.Cities.Queries.GetCities
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Mapster;
    using MapsterMapper;
    using MediatR;

    using Common.Interfaces;
    using Common.Models;
    using Dto;

    public class GetAllCitiesQuery : IRequest<ServiceResult<List<CityDto>>>
    {

    }

    public class GetCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, ServiceResult<List<CityDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<CityDto>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            List<CityDto> list = await _context.Cities
                .Include(x => x.Districts)
                .ThenInclude(c => c.Villages)
                .ProjectToType<CityDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<CityDto>>(ServiceError.NotFount);

        }
    }
}
