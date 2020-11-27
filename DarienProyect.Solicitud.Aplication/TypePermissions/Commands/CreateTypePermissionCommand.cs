
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

namespace DarienProyect.Solicitud.Aplication.TypePermissions.Commands
{
    public class CreateTypePermissionCommand : IRequest<bool>
    {

        public TypePermissionDto TypePermission { get; set; }

    }


    public class CreateTypePermissionCommandHandler : IRequestHandler<CreateTypePermissionCommand, bool>
    {

        SolicitudDbContext _solicitudDbContext;

        public CreateTypePermissionCommandHandler(SolicitudDbContext solicitudDbContext)
        {
            _solicitudDbContext = solicitudDbContext;
        }

        public async Task<bool> Handle(CreateTypePermissionCommand request, CancellationToken cancellationToken)
        {
            bool _rowAfect = false;

            if (request.TypePermission == null)
            {

                throw new NotFoundException("Tipo Permission", "No puede ser nula");
            }



            try
            {

                var _newpermission = await CreatePermission(request.TypePermission);
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
        private async Task<TypePermission> CreatePermission(TypePermissionDto permission)
        {

            var _typepermission =new  TypePermission{ 
            
                Description=permission.Description
            };

            //set identity
            _typepermission.ChangeCurrentIdentity(permission.Id);

            return _typepermission;
        }

        void SavePermission(TypePermission  typePermission)
        {
            try
            {
                var entityValidator = EntityValidatorFactory.CreateValidator();

                if (entityValidator.IsValid(typePermission)) 
                {

                    _solicitudDbContext.TypePermissions.Add(typePermission);
                    _solicitudDbContext.Commit();
                   
                }
                else 
                    throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(typePermission));
            }
            catch (HttpRequestExceptionEx ex)
            {

                throw ex;
            }
        }



    }


}
