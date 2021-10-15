using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackingListApp.Interfaces;
using PackingListApp.Models;
using PackingListApp.EntityFramework;

namespace PackingListApp.Services
{
    public class UserServices : IUserServices
    {
        private readonly TestContext _context;
        public UserServices(TestContext context)
        {
            _context = context;
        }


        public int Add(NewUserModel userDTO)
        {
            var newuser = new UserModel()
            {
                Address = userDTO.Address,
                Name = userDTO.Name,
                LastName = userDTO.LastName,
                IsAdmin = userDTO.IsAdmin,
                AdminType = (TypOfAdmin)userDTO.AdminType
            };
            _context.UserModels.Add(newuser
            );
            _context.SaveChanges();
            return newuser.Id;
        }

        public UserModel Get(int id)
        {
            return _context.UserModels.FirstOrDefault(t => t.Id == id);
        }

        public List<UserModel> GetAll()
        {
            return _context.UserModels.ToList();
        }

        public int Put(int id, UserModel item)
        {
            var itemput = _context.UserModels.FirstOrDefault(t => t.Id == id);
            itemput.Address = item.Address;
            itemput.Name = item.Name;
            itemput.LastName = item.LastName;
            itemput.IsAdmin = item.IsAdmin;
            itemput.AdminType = item.AdminType;
            _context.SaveChanges();
            return id;

        }

        public void Delete(UserModel item)
        {
            _context.UserModels.Remove(item);
            _context.SaveChanges();
        }
    }
}
