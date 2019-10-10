using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute
    {
        /*
         * Value: Trae el valor de la propiedad en donde se ha colocado el atributo
         *        en este caso: "PrimeraLetraMayusculaAttribute".
         *        
         * ValidationContext: Trae informacion acerca del contexto en el que se esta ejecutando la aplicacion
         *                    Podria traer el objeto que se esta validando pero traeria una perdida de versatilidad
         *                    porque estaria dependiendo de una clase en concreto.
        */
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Debe validar una sola cosa, de las otras validaciones se encargan atributos tales como [Required]
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            string firstLetter = value.ToString()[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
                return new ValidationResult("La primera letra debe ser mayúscula");

            return ValidationResult.Success;
        }
    }
}
