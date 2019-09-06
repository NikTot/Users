using AutoMapper;
using SAU.DTO;
using SAU.Models;

namespace SAU.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Models.System, SystemDTO>();
                cfg.CreateMap<JQLFilter, JQLFilterDTO>();
            });
        }
    }
    
}