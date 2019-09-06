using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAU.Models
{
    public class System
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }
       
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<JQLFilter> JQLFilters { get; set; }
    }
}