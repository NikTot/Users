using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SAU.DTO
{
    public class SystemDTO
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Не указано имя системы")]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }

        public virtual IList<UserDTO> Users { get; set; }
        public virtual ICollection<JQLFilterDTO> JQLFilters { get; set; }
    }
}