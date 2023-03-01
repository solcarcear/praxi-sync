using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bussines_manager.Model
{
    [Table("contact")]
    public class Contact
    {
        public int Id { get; set; }

        [StringLength(maximumLength:200)]
        public string Nombre { get; set; }

        [StringLength(maximumLength: 200)]
        public string Tipo { get; set; }

        [StringLength(maximumLength: 200)]
        public string Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? ModifyOn { get; set; }

        public string? IdCreatio { get; set; }
        public string Apellido { get; set; }


    }
}
