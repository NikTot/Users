using SAU.DTO;
using SAU.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SAU.Controllers
{
    public class SystemController : Controller
    {
        private readonly IRepository<UserDTO, int> _userRepository;
        private readonly IRepository<SystemDTO, int> _systemRepository;

        public SystemController(IRepository<UserDTO, int> userRepository, IRepository<SystemDTO, int> systemRepository)
        {
            _userRepository = userRepository;
            _systemRepository = systemRepository;
        }

        // GET: Systems
        public ActionResult Index()
        {
            var systems = _systemRepository.GetAll();
            return View(systems);
        }

        public ActionResult List()
        {
            var systems = _systemRepository.GetAll();
            return PartialView("_List", systems);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            var system = new SystemDTO();
            return PartialView("_Create", system);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Create(SystemDTO system)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            system.IsActive = true;
            _systemRepository.Add(system);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public ActionResult Edit(int id)
        {
            var system = _systemRepository.Get(id);
            return PartialView("_Edit", system);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Edit(SystemDTO system)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            _systemRepository.Update(system);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        // GET: User/Delete/5`
        public ActionResult Delete(int id)
        {
            var system = _systemRepository.Get(id);
            return PartialView("_Delete", system);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Delete(SystemDTO system)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            system.IsActive = false;
            _systemRepository.Update(system);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public ActionResult Users(int id)
        {
            var systemDB = _systemRepository.Get(id);
            var userDB = _userRepository.GetAll();
            var userList = new List<UserDTO>();
            foreach (var user in userDB)
            {
                var systemContains = false;
                foreach (var system in user.Systems)
                {
                    if (system.Id == systemDB.Id)
                    {
                        systemContains = true;
                        break;
                    }
                }

                user.IsSelected = systemContains;
                userList.Add(user);
            }

            var systemDTO = new SystemDTO()
            {
                Id = systemDB.Id,
                Name = systemDB.Name,
                IsActive = systemDB.IsActive,
                Users = userList
            };

            return PartialView("_ListUsers", systemDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Users(SystemDTO systemDTO)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var system = _systemRepository.Get(systemDTO.Id);
            system.Users.Clear();
            var userList = new List<UserDTO>();
            foreach (var user in systemDTO.Users.Where(s => s.IsSelected == true).ToList())
            {
                userList.Add(user);
            }

            system.Users = userList;
            _systemRepository.Update(system);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}