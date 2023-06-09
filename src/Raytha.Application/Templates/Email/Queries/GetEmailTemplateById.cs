﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Raytha.Application.Common.Exceptions;
using Raytha.Application.Common.Interfaces;
using Raytha.Application.Common.Models;

namespace Raytha.Application.Templates.Email.Queries;

public class GetEmailTemplateById
{
    public record Query : GetEntityByIdInputDto, IRequest<IQueryResponseDto<EmailTemplateDto>>
    {
    }

    public class Handler : RequestHandler<Query, IQueryResponseDto<EmailTemplateDto>>
    {
        private readonly IRaythaDbContext _db;
        public Handler(IRaythaDbContext db)
        {
            _db = db;
        }
        protected override IQueryResponseDto<EmailTemplateDto> Handle(Query request)
        {
            var entity = _db.EmailTemplates
                .FirstOrDefault(p => p.Id == request.Id.Guid);

            if (entity == null)
                throw new NotFoundException("EmailTemplate", request.Id);

            return new QueryResponseDto<EmailTemplateDto>(EmailTemplateDto.GetProjection(entity));
        }
    }
}
