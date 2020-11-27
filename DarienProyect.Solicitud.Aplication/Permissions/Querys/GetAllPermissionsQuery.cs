
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

    public class GetAllPermissionsQuery : IRequest<IEnumerable<PermissionDto>>
    {

        public bool WithTypePermission { get; set; } = false;
    }
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<PermissionDto>>
    {

        SolicitudDbContext _solicitudDbContext;

        public GetAllPermissionsQueryHandler(SolicitudDbContext solicitudDbContext)
        {
            _solicitudDbContext = solicitudDbContext;
        }

        public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {

            try
            {

                if (request.WithTypePermission)
                {

                    var _resultswith = await _solicitudDbContext.AllInclude<Permission>(x => x.TypePermission);
                    return await Task.FromResult<IEnumerable<PermissionDto>>(_resultswith.ProjectedAsCollection<PermissionDto>());

                }
                
                var _results = await _solicitudDbContext.All<Permission>();
                return await Task.FromResult<IEnumerable<PermissionDto>>(_results.ProjectedAsCollection<PermissionDto>());

            }
            catch (HttpRequestExceptionEx ex)
            {

                return Enumerable.Empty<PermissionDto>().ToList().AsReadOnly();
            }





        }
    }
}
