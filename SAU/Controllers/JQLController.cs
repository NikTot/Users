using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using SAU.DTO;
using SAU.Models;
using SAU.Repositories;
using SAU.Services;

namespace SAU.Controllers
{
    public class JQLController : Controller
    {
        private readonly IRepository<JQLFilterDTO, int> _repository;
        private readonly IRepository<SystemDTO, int> _repositorySystem;
        private JiraService _jiraService;

        public JQLController(IRepository<JQLFilterDTO, int> repository, IRepository<SystemDTO, int> repositorySystem)
        {
            _repository = repository;
            _repositorySystem = repositorySystem;
            _jiraService = new JiraService();
        }

        public ActionResult Index()
        {
            var jqlFilters = _repository.GetAll();
            return View(jqlFilters);
        }

        public ActionResult List()
        {
            var jqlFilters = _repository.GetAll();
            var systems = new SelectList(_repositorySystem.GetAll(), "Id", "Name");
            ViewBag.Systems = systems;
            return PartialView("_List", jqlFilters);
        }

        public ActionResult Do()
        {
            IList<JQLFilterDTO> jqlFilters =  (IList<JQLFilterDTO>)_repository.GetAll();
            _jiraService.Get(jqlFilters);
            return Redirect("Index");
        }

        public ActionResult DoId(int id)
        {
            var jqlFilters = new List<JQLFilterDTO>();
            var jqlFilter = _repository.Get(id);
            jqlFilters.Add(jqlFilter);
             _jiraService.Get(jqlFilters);
            return Redirect("Index");
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            var jqlFilter = new JQLFilterDTO();
            var systems = _repositorySystem.GetAll();
            SelectList Systems = new SelectList(systems, "Id", "Name");
            ViewBag.Systems = Systems;
            return PartialView("_Create", jqlFilter);
        }

        // POST: Class/Create
        [HttpPost]

        [ValidateAntiForgeryToken]
        public HttpResponseMessage Create(JQLFilterDTO jqlFilter)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            jqlFilter.Hidden = false;
            _repository.Add(jqlFilter);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // GET: Class/Edit/5
        public ActionResult Edit(int id)
        {
            var jqlFilter = _repository.Get(id);
            var systems = _repositorySystem.GetAll();
            SelectList jqlFilterSystems = new SelectList(systems, "Id", "Name", jqlFilter.SystemId);
            ViewBag.Systems = jqlFilterSystems;
            return PartialView("_Edit", jqlFilter);
        }

        // POST: Class/Edit/5
        [HttpPost]
        public HttpResponseMessage Edit(JQLFilterDTO jqlFilter)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            _repository.Update(jqlFilter);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // GET: Class/Delete/5
        public ActionResult Delete(int id)
        {
            var jqlFilters = _repository.Get(id);
            var systems = _repositorySystem.Get(jqlFilters.SystemId);
            jqlFilters.System = systems;
            return PartialView("_Delete", jqlFilters);
        }

        // POST: Class/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Delete(JQLFilterDTO jqlFilter)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            jqlFilter.Hidden = true;
            _repository.Update(jqlFilter);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}