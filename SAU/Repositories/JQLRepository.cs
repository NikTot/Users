using AutoMapper;
using SAU.DTO;
using SAU.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace SAU.Repositories
{
    public class JQLRepository : IRepository<JQLFilterDTO, int>
    {
        [Dependency]
        public DBContext Context { get; set; }

        public IEnumerable<JQLFilterDTO> GetAll()
        {
            var jqlFilters = Context.JqlFilters
                .Include(s => s.System)
                .Where(c => c.Hidden == false)
                .ToList();
            return Mapper.Map<IList<JQLFilter>, IEnumerable<JQLFilterDTO>>(jqlFilters);
        }

        ///
        public IEnumerable<JQLFilterDTO> GetByName(string systemName)
        {
            var jqlFilter = new List<JQLFilter>();
            jqlFilter = null;
            return Mapper.Map<IList<JQLFilter>, IEnumerable<JQLFilterDTO>>(jqlFilter);
        }

        public JQLFilterDTO Get(int id)
        {
            return Mapper.Map<JQLFilter, JQLFilterDTO>(Context.JqlFilters.Include(s => s.System).FirstOrDefault(j => j.Id == id && j.Hidden == false));
        }

        public void Add(JQLFilterDTO entity)
        {
            var jqlFilter = Mapper.Map<JQLFilterDTO, JQLFilter>(entity);
            Context.JqlFilters.Add(jqlFilter);
            Context.SaveChanges();
        }

        public void Remove(JQLFilterDTO entity)
        {
            var jqlFilter = Mapper.Map<JQLFilterDTO, JQLFilter>(entity);
            var obj = Context.JqlFilters.Find(jqlFilter.Id);
            if (obj != null) Context.JqlFilters.Remove(obj);
            Context.SaveChanges();
        }

        public void Update(JQLFilterDTO entity)
        {
            var jqlFilter = Mapper.Map<JQLFilterDTO, JQLFilter>(entity);
            var jqlFilterDB = Context.JqlFilters.Include(s => s.System).FirstOrDefault(s => s.Id == jqlFilter.Id);
            if (jqlFilterDB != null)
            {
                jqlFilterDB.JqlString = jqlFilter.JqlString;
                jqlFilterDB.SystemId = jqlFilter.SystemId;
                jqlFilterDB.Hidden = jqlFilter.Hidden;
            }
            Context.Entry(jqlFilterDB).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}