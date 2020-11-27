
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

namespace DarienProyect.Solicitud.Aplication.TypePermissions.Querys
{

    public class GetAllTypePermissionsQuery : IRequest<IEnumerable<TypePermissionDto>>
    {

      //  public bool WithPermission { get; set; } = false;
    }
    public class GetAllTypePermissionsQueryHandler : IRequestHandler<GetAllTypePermissionsQuery, IEnumerable<TypePermissionDto>>
    {

        SolicitudDbContext _solicitudDbContext;

        public GetAllTypePermissionsQueryHandler(SolicitudDbContext solicitudDbContext)
        {
            _solicitudDbContext = solicitudDbContext;
        }

        public async Task<IEnumerable<TypePermissionDto>> Handle(GetAllTypePermissionsQuery request, CancellationToken cancellationToken)
        {

            try
            {

                //if (request.WithPermission)
                //{

                //    var _resultswith = await _solicitudDbContext.AllInclude<TypePermission>(x => x.Permissions);
                //    return await Task.FromResult<IEnumerable<TypePermissionDto>>(_resultswith.ProjectedAsCollection<TypePermissionDto>());

                //}
                
                var _results = await _solicitudDbContext.All<TypePermission>();
                return await Task.FromResult<IEnumerable<TypePermissionDto>>(_results.ProjectedAsCollection<TypePermissionDto>());

            }
            catch (HttpRequestExceptionEx ex)
            {

                return Enumerable.Empty<TypePermissionDto>().ToList().AsReadOnly();
            }

        }
    }
}
