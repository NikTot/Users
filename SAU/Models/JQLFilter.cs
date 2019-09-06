using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SAU.Models
{
    public class JQLFilter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Это поле обязательное")]
        [DisplayName("Имя класса")]
        public int SystemId { get; set; }

        [Required(ErrorMessage = "Это поле обязательное")]
        [DisplayName("JQL фильтр")]
        public string JqlString { get; set; }
        public bool Hidden { get; set; }

        public System System { get; set; }
    }
}