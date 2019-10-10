using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        [PrimeraLetraMayuscula]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "El campo nombre debe tener entre {2} y {1} caracteres.")]
        public string Nombre { get; set; }
    }
}
