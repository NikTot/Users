using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using SAU.DTO;
using Unity;
using SAU.Models;

namespace SAU.Repositories
{
    public class UserRepository : IRepository<UserDTO, int>
    {
        [Dependency]
        public DBContext Context { get; set; }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = Context.Users
                .Include(s => s.Systems)
                .Where(u => u.IsDeleted == false)
                .ToList();
            return Mapper.Map<IList<User>, IEnumerable<UserDTO>>(users);
        }

        public UserDTO Get(int id)
        {
            return Mapper.Map<User, UserDTO>(Context.Users
                .Include(s => s.Systems)
                .FirstOrDefault(u => u.Id == id && u.IsDeleted == false));
        }

        public IEnumerable<UserDTO> GetByName(string name)
        {
            var users = Context.Users
                .Where(u => u.Name == name && u.IsDeleted == false)
                .ToList();
            return Mapper.Map<IList<User>, IEnumerable<UserDTO>>(users);
        }

        public void Add(UserDTO entity)
        {
            var user = Mapper.Map<UserDTO, User>(entity);
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public void Remove(UserDTO entity)
        {
            var user = Mapper.Map<UserDTO, User>(entity);
            var obj = Context.Users.Find(user.Id);
            if (obj != null) Context.Users.Remove(obj);
            Context.SaveChanges();
        }

        public void Update(UserDTO entity)
        {
            var user = Mapper.Map<UserDTO, User>(entity);
            var userDB = Context.Users.Include(s => s.Systems).FirstOrDefault(u => u.Id == user.Id);
            if (userDB != null)
            {
                userDB.Name = user.Name;
                userDB.Login = user.Login;
                userDB.IsActive = user.IsActive;
                userDB.IsDeleted = user.IsDeleted;
                if (entity.Systems != null)
                {
                    var systemList = new List<Models.System>();
                    foreach (var system in entity.Systems.ToList())
                    {
                        var systemInDB = Context.Systems.Include(u => u.Users).FirstOrDefault(s => s.Id == system.Id);
                        systemList.Add(systemInDB);
                    }

                    var deletedSystem = userDB.Systems.Except(systemList).ToList();
                    var addedSystem = systemList.Except(userDB.Systems).ToList();
                    deletedSystem.ForEach(s => userDB.Systems.Remove(s));
                    addedSystem.ForEach(s => userDB.Systems.Add(s));
                }
            }

            Context.Entry(userDB).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}