using SAU.DTO;
using SAU.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SAU.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<UserDTO, int> _userRepository;
        private readonly IRepository<SystemDTO, int> _systemRepository;

        public UserController(IRepository<UserDTO, int> userRepository, IRepository<SystemDTO, int> systemRepository)
        {
            _userRepository = userRepository;
            _systemRepository = systemRepository;
        }

        public ActionResult Index()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }

        public ActionResult List()
        {
            var users = _userRepository.GetAll();
            return PartialView("_List", users);
        }


        // GET: User/Create
        public ActionResult Create()
        {
            var user = new UserDTO();
            return PartialView("_Create", user);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Create(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            user.IsActive = true;
            _userRepository.Add(user);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public ActionResult Edit(int id)
        {
            var user = _userRepository.Get(id);
            return PartialView("_Edit", user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Edit(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            _userRepository.Update(user);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // GET: User/Delete/5`
        public ActionResult Delete(int id)
        {
            var user = _userRepository.Get(id);
            return PartialView("_Delete", user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Delete(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            user.IsDeleted = true;
            _userRepository.Update(user);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public ActionResult Systems(int id)
        {
            var user = _userRepository.Get(id);
            var systems = _systemRepository.GetAll();
            var userSystems = new List<SystemDTO>();
            foreach (var system in systems)
            {
                var userContains = false;
                foreach (var users in system.Users)
                {
                    if (users.Id == user.Id)
                    {
                        userContains = true;
                        break;
                    }
                }
                system.IsSelected = userContains;
                userSystems.Add(system);
            }

            var userSystem = new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                IsActive = user.IsActive,
                Systems = userSystems
            };

            return PartialView("_ListSystems", userSystem);
        }

        // POST: nUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Systems(UserDTO userSystems)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var user = _userRepository.Get(userSystems.Id);
            user.Systems.Clear();
            var systemList = new List<SystemDTO>();
            foreach (var system in userSystems.Systems.Where(s => s.IsSelected == true).ToList())
            {
                systemList.Add(system);
            }

            user.Systems = systemList;
            _userRepository.Update(user);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}