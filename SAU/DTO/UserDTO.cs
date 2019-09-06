using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SAU.DTO
{
    public class UserDTO
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Name { get; set; }

        [DisplayName("Логин")]
        [Required(ErrorMessage = "Не указан логин пользователя")]
        public string Login { get; set; }

        [DisplayName("Активен")]
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public bool IsSelected { get; set; }

        public virtual IList<SystemDTO> Systems { get; set; }
 
    }
}