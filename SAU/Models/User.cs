using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAU.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя пользователя")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан логин пользователя")]
        [StringLength(200)]
        public string Login { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public virtual ICollection<System> Systems { get; set; }

    }
}