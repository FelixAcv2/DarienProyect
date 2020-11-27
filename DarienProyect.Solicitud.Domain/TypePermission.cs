using Acv2.SharedKernel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DarienProyect.Solicitud.Domain
{
    public class TypePermission:Entity, IValidatableObject
    {
        public string Description { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public TypePermission()
        {
            Permissions = new Collection<Permission>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Check name property
            if (String.IsNullOrWhiteSpace(this.Description))
            {
                validationResults.Add(new ValidationResult("validation_AssociationNameCannotBeNull",
                                                           new string[] { "Name" }));
            }
            return validationResults;
        }
    }
}