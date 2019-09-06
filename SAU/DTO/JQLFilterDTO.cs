using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SAU.DTO
{
    public class JQLFilterDTO
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Имя класса")]
        public int SystemId { get; set; }

        [DisplayName("Имя класса")]
        public SystemDTO System { get; set; }

        [Required]
        [DisplayName("JQL фильтр")]
        public string JqlString { get; set; }
        public bool Hidden { get; set; }
        public bool IsSelected { get; set; }
    }
}