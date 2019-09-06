using System.Collections.Generic;

namespace SAU.DTO
{
    public class ApiSystemDTO
    {
        public string System { get; set; }
        public IEnumerable<ApiUserDTO> Users { get; set; }
    }
}