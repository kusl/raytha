﻿using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Raytha.Application.Common.Exceptions;
using Raytha.Application.Common.Interfaces;
using Raytha.Application.Common.Models;

namespace Raytha.Application.Views.Queries;

public class GetViewById
{
    public record Query : GetEntityByIdInputDto, IRequest<IQueryResponseDto<ViewDto>>
    {
    }

    public class Handler : RequestHandler<Query, IQueryResponseDto<ViewDto>>
    {
        private readonly IRaythaDbContext _db;
        public Handler(IRaythaDbContext db)
        {
            _db = db;
        }
        protected override IQueryResponseDto<ViewDto> Handle(Query request)
        {
            var entity = _db.Views
                .Include(p => p.WebTemplate)
                .Include(p => p.Route)
                .Include(p => p.ContentType)
                .ThenInclude(p => p.ContentTypeFields)
                .FirstOrDefault(p => p.Id == request.Id.Guid);

            if (entity == null)
                throw new NotFoundException("View", request.Id);

            return new QueryResponseDto<ViewDto>(ViewDto.GetProjection(entity));
        }
    }
}
