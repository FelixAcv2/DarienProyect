
using Acv2.SharedKernel.Application;
using Acv2.SharedKernel.Application.Exceptions;
using Acv2.SharedKernel.Crosscutting.Exceptions;
using Acv2.SharedKernel.Crosscutting.Logging;
using Acv2.SharedKernel.Crosscutting.Validator;
using DarienProyect.Solicitud.Data;
using DarienProyect.Solicitud.Domain;
using DarienProyect.Solicitud.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DarienProyect.Solicitud.Aplication.Permissions.Commands
{
    public class CreatePermissionCommand : IRequest<bool>
    {

        public PermissionDto Permission { get; set; }

    }


    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, bool>
    {

        SolicitudDbContext _solicitudDbContext;

        public CreatePermissionCommandHandler(SolicitudDbContext solicitudDbContext)
        {
            _solicitudDbContext = solicitudDbContext;
        }

        public async Task<bool> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            bool _rowAfect = false;

            if (request.Permission == null)
            {

                throw new NotFoundException("Permission", "No puede ser nula");
            }



            try
            {

                var _newpermission = await CreatePermission(request.Permission);
                    SavePermission(_newpermission);
                _rowAfect = true;

               // LoggerFactory.CreateLog().LogInfo("Permission create");
                return await Task.FromResult(_rowAfect);

            }
            catch (HttpRequestExceptionEx ex)
            {
               // LoggerFactory.CreateLog().LogError(ex.Source, ex);
                throw ex;
            }




        }
        private async Task<Permission> CreatePermission(PermissionDto permission)
        {

            var _typepermission = await _solicitudDbContext.FindByKey<TypePermission>(permission.TypePermissionId);
            _typepermission.ChangeCurrentIdentity(permission.TypePermissionId);

            var _permission = Permission.CreatePermission(permission.Name, permission.LastName,
                                                          permission.TypePermissionId);

            //set identity
            _permission.ChangeCurrentIdentity(permission.Id);

            return _permission;
        }

        void SavePermission(Permission permission)
        {
            try
            {
                var entityValidator = EntityValidatorFactory.CreateValidator();

                if (entityValidator.IsValid(permission)) 
                {

                    _solicitudDbContext.Permissions.Add(permission);
                    _solicitudDbContext.Commit();
                   
                }
                else 
                    throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(permission));
            }
            catch (HttpRequestExceptionEx ex)
            {

                throw ex;
            }
        }



    }


}
