using PackingListApp.DTO;
using PackingListApp.Interfaces;
using PackingListApp.EntityFramework;
using PackingListApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace PackingListApp.Services
{
    public class UserServices : IUserServices

    {
        private readonly TestContext _context;
        public UserServices(TestContext context)
        {
            _context = context;
        }
        int IUserServices.AddUser(NewUserModel Usermodel)
        {
            var newUser = new UserModel()
            {
                Name = Usermodel.Name,
                LastName = Usermodel.LastName,
                Address = Usermodel.Address,
            };
            _context.UserModels.Add(newUser);
            _context.SaveChanges();
            return newUser.Id;
        }

        List<UserModel> IUserServices.GetAllUsers()
        {
            return _context.UserModels.ToList();
        }

        UserModel IUserServices.GetUser(int id)
        {
            return _context.UserModels.FirstOrDefault(t => t.Id == id);
        }

        int IUserServices.PutUser(int id, UserModel item)
        {
            var itemput = _context.UserModels.FirstOrDefault(t => t.Id == id);
            itemput.Name = item.Name;
            itemput.LastName = item.LastName;
            item.Address = item.Address;
            _context.SaveChanges();
            return id;
        }
    }
}
