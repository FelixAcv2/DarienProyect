using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DarienProyect.Solicitud.DTOs
{
   public class TypePermissionDto
    {

        public int Id { get; set; }

        public string Description { get; set; }

       // public List<PermissionDto> Permissions { get; set; }


        public TypePermissionDto()
        {
            //Permissions = new List<PermissionDto>();
        }

    }

}
