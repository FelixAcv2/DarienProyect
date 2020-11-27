
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
    public class GetTypePermissionsQuery : IRequest<TypePermissionDto>
    {

        public int Id { get; set; }
      
    }
    public class GetTypePermissionsQueryHandler : IRequestHandler<GetTypePermissionsQuery, TypePermissionDto>
    {

        SolicitudDbContext _solicitudDbContext;

        public GetTypePermissionsQueryHandler(SolicitudDbContext solicitudDbContext)
        {
            _solicitudDbContext = solicitudDbContext;
        }

        public async Task<TypePermissionDto> Handle(GetTypePermissionsQuery request, CancellationToken cancellationToken)
        {

            try
            {

               
                var _result = await _solicitudDbContext.FindByKey<TypePermission>(request.Id);
                return await Task.FromResult<TypePermissionDto>(_result.ProjectedAs<TypePermissionDto>());

            }
            catch (HttpRequestExceptionEx ex)
            {

                return Enumerable.Empty<TypePermissionDto>().FirstOrDefault();
            }
        }
    }
}
