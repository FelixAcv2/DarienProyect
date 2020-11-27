
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
    public class UpdatePermissionCommand : IRequest<bool>
    {

        public PermissionDto Permission { get; set; }

    }


    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, bool>
    {

        SolicitudDbContext _solicitudDbContext;

        public UpdatePermissionCommandHandler(SolicitudDbContext solicitudDbContext)
        {
            _solicitudDbContext = solicitudDbContext;
        }

        public async Task<bool> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            bool _rowAfect = false;

            if (request.Permission == null)
            {

                throw new NotFoundException("Permission", "No puede estar en blanco");
            }



            bool _error = false;
           
            //check preconditions
            if (request.Permission == null)
                throw new ArgumentException("warning_CannotAddEntityWithEmptyInformation");

            return await Task.Run<bool>(async () => {


                //get persisted item
                var _persisted = await _solicitudDbContext.FindByKey<Permission>(request.Permission.Id);


                if (_persisted != null) 
                {
                    //materialize
                    var _current = await MaterializepPermissionFromDto(request.Permission);


                    try
                    {

                        _solicitudDbContext.Update<Permission>(_current);
                        _rowAfect = true;
                    }
                    catch (Exception ex)
                    {
                        //_uniOfWork.RollbackChanges();
                        _error = true;

                        throw ex;
                    }

                    if (!_error)
                    {
                        //commit unit of work
                        await _solicitudDbContext.SaveChangesAsync();
                    }


                }
                else
                    LoggerFactory.CreateLog().LogWarning("warning_CannotUpdateNonExistingEntity");

                return _rowAfect;


            });



        }


        private async Task<Permission> MaterializepPermissionFromDto(PermissionDto  permission)
        {
           
            var _typePermission = await _solicitudDbContext.FindByKey<TypePermission>(permission.TypePermissionId);
            _typePermission.ChangeCurrentIdentity(permission.TypePermissionId);

            var _current = Permission.CreatePermission(permission.Name,
                                                       permission.LastName,
                                                       _typePermission);

            _current.ChangeCurrentIdentity(permission.Id);

            return _current;
        }

       
    }


}
