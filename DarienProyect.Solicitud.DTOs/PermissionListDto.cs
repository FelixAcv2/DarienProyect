using System;
using System.Collections.Generic;
using System.Text;

namespace DarienProyect.Solicitud.DTOs
{
  public  class PermissionListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public int TypePermissionId { get; set; }
        public DateTime DatePermission { get; set; }


    }
}
