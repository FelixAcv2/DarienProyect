using Acv2.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DarienProyect.Solicitud.Domain
{
   public class Permission:Entity, IValidatableObject
    {

        public string Name { get; set; }

        public string  LastName { get; set; }

        public int TypePermissionId { get; set; }

        public TypePermission TypePermission { get; set; }

        public DateTime DatePermission { get; set; }

        public Permission()
        {

        }



        /// <summary>
        /// Associate existing Type Permission to this Permission
        /// </summary>
        /// <param name="typePermission"></param>
        public void SetTheTypePermissionForThisPermission(TypePermission typePermission)
        {
            if (typePermission == null
                ||
                typePermission.IsTransient())
            {
                throw new ArgumentException("exception_CannotAssociateTransientOrNullCommission");
            }

            //fix relation
            this.TypePermissionId = typePermission.ID;

            this.TypePermission = typePermission;
        }


        /// <summary>
        /// Set the typePermissionId reference for this permission
        /// </summary>
        /// <param name="typePermissionId"></param>
        public void SetTheTypePermissionReference(int? typePermissionId)
        {
            if (typePermissionId != null)
            {
                //fix relation
                this.TypePermissionId = typePermissionId.Value;

                this.TypePermission = null;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Check name property
            if (String.IsNullOrWhiteSpace(this.Name))
            {
                validationResults.Add(new ValidationResult("validation_AssociationNameCannotBeNull",
                                                           new string[] { "Name" }));
            }



            if (String.IsNullOrWhiteSpace(this.LastName))
            {
                validationResults.Add(new ValidationResult("validation_AssociationNameCannotBeNull",
                                                           new string[] { "LastName" }));
            }

            if (String.IsNullOrWhiteSpace(this.DatePermission.Date.ToString()))
            {
                validationResults.Add(new ValidationResult("validation_AssociationNameCannotBeNull",
                                                           new string[] { "Date" }));
            }

            return validationResults;
        }




        public static Permission CreatePermission(string name, string lastname,
                                                  TypePermission  typePermission)
        {

            var _newPermission = new Permission
            {

               
                Name = name,
                LastName =lastname

            };

            _newPermission.DatePermission = DateTime.Now;

            _newPermission.SetTheTypePermissionForThisPermission(typePermission);
            return _newPermission;

        }

        public static Permission CreatePermission(string name, string lastname,
                                        int typepermissionid)
        {


            var _newPermission = new Permission
            {


                Name = name,
                LastName = lastname

            };

            _newPermission.DatePermission = DateTime.Now;

            _newPermission.SetTheTypePermissionReference(typepermissionid);
            return _newPermission;

        }



    }
}
