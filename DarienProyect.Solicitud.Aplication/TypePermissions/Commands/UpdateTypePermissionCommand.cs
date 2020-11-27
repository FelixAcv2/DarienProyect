
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
    public class UpdateTypePermissionCommand : IRequest<bool>
    {

        public TypePermissionDto TypePermission { get; set; }

    }


    public class UpdateTypePermissionCommandHandler : IRequestHandler<UpdateTypePermissionCommand, bool>
    {

        SolicitudDbContext _solicitudDbContext;

        public UpdateTypePermissionCommandHandler(SolicitudDbContext solicitudDbContext)
        {
            _solicitudDbContext = solicitudDbContext;
        }

        public async Task<bool> Handle(UpdateTypePermissionCommand request, CancellationToken cancellationToken)
        {
            bool _rowAfect = false;

            if (request.TypePermission == null)
            {

                throw new NotFoundException("Tipo Permission", "No puede estar en blanco");
            }



            bool _error = false;
           
            //check preconditions
            if (request.TypePermission == null)
                throw new ArgumentException("warning_CannotAddEntityWithEmptyInformation");

            return await Task.Run<bool>(async () => {


                //get persisted item
                var _persisted = await _solicitudDbContext.FindByKey<Permission>(request.TypePermission.Id);


                if (_persisted != null) 
                {
                    //materialize
                    var _current = await MaterializepPermissionFromDto(request.TypePermission);


                    try
                    {

                        _solicitudDbContext.Update<TypePermission>(_current);
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


        private async Task<TypePermission> MaterializepPermissionFromDto(TypePermissionDto  permission)
        {
           
            var _typePermission = await _solicitudDbContext.FindByKey<TypePermission>(permission.Id);
            _typePermission.ChangeCurrentIdentity(permission.Id);

            var _current = new TypePermission { 
            
                Description=permission.Description
            
            };

            _current.ChangeCurrentIdentity(permission.Id);

            return _current;
        }

       
    }


}
