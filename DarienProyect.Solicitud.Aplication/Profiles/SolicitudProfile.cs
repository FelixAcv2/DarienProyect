using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DarienProyect.Solicitud.Domain;
using DarienProyect.Solicitud.DTOs;

namespace DarienProyect.Solicitud.Aplication.Profiles
{
   public class SolicitudProfile:Profile
    {

        public SolicitudProfile()
        {
            Mapper.Initialize(_cfg=> {


                _cfg.CreateMap<Permission, PermissionDto>(MemberList.None).ReverseMap().ForAllOtherMembers(u => u.Ignore());
                _cfg.CreateMap<IEnumerable<Permission>, IEnumerable<PermissionDto>>(MemberList.None).ReverseMap().ForAllOtherMembers(u => u.Ignore());

                _cfg.CreateMap<TypePermission, TypePermissionDto>(MemberList.None).ReverseMap().ForAllOtherMembers(u => u.Ignore());
                _cfg.CreateMap<IEnumerable<TypePermission>, IEnumerable<TypePermissionDto>>(MemberList.None).ReverseMap().ForAllOtherMembers(u => u.Ignore());


            });
        }
    }
}
