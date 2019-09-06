using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using SAU.DTO;
using SAU.Models;
using Unity;

namespace SAU.Repositories
{
    public class SystemRepository : IRepository<SystemDTO, int>
    {
        [Dependency]
        public DBContext Context { get; set; }

        public IEnumerable<SystemDTO> GetAll()
        {
            var systems = Context.Systems
                .Include(u => u.Users)
                .Where(type => !String.IsNullOrEmpty(type.Name))
                .Where(s => s.IsActive == true)
                .ToList();
            return Mapper.Map<IList<Models.System>, IEnumerable<SystemDTO>>(systems);
        }

        public IEnumerable<SystemDTO> GetByName(string systemName)
        {
            var system = Context.Systems
                .Include(u => u.Users)
                .Where(s => s.IsActive == true && s.Name == systemName)
                .ToList();
            return Mapper.Map<IList<Models.System>, IEnumerable<SystemDTO>>(system);
        }

        public SystemDTO Get(int id)
        {
            return Mapper.Map<Models.System, SystemDTO>(Context.Systems.FirstOrDefault(r => r.Id == id && r.IsActive == true));
        }

        public void Add(SystemDTO entity)
        {
            var system = Mapper.Map<SystemDTO, Models.System>(entity);
            Context.Systems.Add(system);
            Context.SaveChanges();
        }

        public void Remove(SystemDTO entity)
        {
            var system = Mapper.Map<SystemDTO, Models.System>(entity);
            var obj = Context.Systems.Find(system.Id);
            if (obj != null) Context.Systems.Remove(obj);
            Context.SaveChanges();
        }

        public void Update(SystemDTO entity)
        {
            var system = Mapper.Map<SystemDTO, Models.System>(entity);
            var systemDB = Context.Systems.Include(u => u.Users).FirstOrDefault(s => s.Id == system.Id);
            if (systemDB != null)
            {
                systemDB.Name = system.Name;
                systemDB.IsActive = system.IsActive;
                if (entity.Users != null)
                {
                    var userList = new List<User>();
                    foreach (var user in entity.Users.ToList())
                    {
                        var userDB = Context.Users.Include(s => s.Systems).FirstOrDefault(u => u.Id == user.Id);
                        userList.Add(userDB);
                    }

                    var deletedUser = systemDB.Users.Except(userList).ToList();
                    var addedUser = userList.Except(systemDB.Users).ToList();
                    deletedUser.ForEach(u => systemDB.Users.Remove(u));
                    addedUser.ForEach(u => systemDB.Users.Add(u));
                }
            }
            Context.Entry(systemDB).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}