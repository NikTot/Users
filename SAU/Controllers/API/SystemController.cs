using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SAU.DTO;
using SAU.Repositories;

namespace SAU.Controllers.API
{
    public class SystemController : ApiController
    {
        private readonly IRepository<SystemDTO, int> _repository;

        public SystemController(IRepository<SystemDTO, int> repository)
        {
            _repository = repository;
        }

        public IEnumerable<ApiSystemDTO> Get(string systemName)
        {
            var systems = _repository.GetByName(systemName);
            var result = new List<ApiSystemDTO>();
            foreach (var system in systems)
            {
                var users = new List<ApiUserDTO>();
                foreach (var user in system.Users.Where(u => u.IsActive == true))
                {
                    users.Add(new ApiUserDTO()
                    {
                        Name = user.Name,
                        Login = user.Login
                    });
                }
                result.Add(new ApiSystemDTO()
                {
                    System = system.Name,
                    Users = users
                });
            }
            return result;
        }

        public IEnumerable<ApiSystemDTO> Get()
        {
            var systems = _repository.GetAll();
            var result = new List<ApiSystemDTO>();
            foreach (var system in systems)
            {
                var users = new List<ApiUserDTO>();
                foreach (var user in system.Users.Where(u => u.IsActive == true))
                {
                    users.Add(new ApiUserDTO()
                    {
                        Name = user.Name,
                        Login = user.Login
                    });
                }
                result.Add(new ApiSystemDTO()
                {
                    System = system.Name,
                    Users = users
                });
            }
            return result;
        }
    }
}
