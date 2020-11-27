
using Acv2.SharedKernel.Application;
using Acv2.SharedKernel.Crosscutting.Exceptions;
using DarienProyect.Solicitud.Data;
using DarienProyect.Solicitud.Domain;
using DarienProyect.Solicitud.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DarienProyect.Solicitud.Aplication.Permissions.Querys
{
    public class GetPermissionsQuery : IRequest<PermissionDto>
    {

        public int Id { get; set; }
        public bool WithTypePermission { get; set; } = false;
    }
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery,PermissionDto>
    {

        SolicitudDbContext _solicitudDbContext;

        public GetPermissionsQueryHandler(SolicitudDbContext solicitudDbContext)
        {
            _solicitudDbContext = solicitudDbContext;
        }

        public async Task<PermissionDto> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {

            try
            {

                if (request.WithTypePermission)
                {

                    var _resultwith = await _solicitudDbContext.FindByInclude<Permission>(x => x.ID == request.Id, x => x.TypePermission);
                    return await Task.FromResult<PermissionDto>(_resultwith.ProjectedAs<PermissionDto>());

                }

                var _result = await _solicitudDbContext.FindByKey<Permission>(request.Id);
                return await Task.FromResult<PermissionDto>(_result.ProjectedAs<PermissionDto>());

            }
            catch (HttpRequestExceptionEx ex)
            {

                return Enumerable.Empty<PermissionDto>().FirstOrDefault();
            }
        }
    }
}
