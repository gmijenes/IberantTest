using PackingListApp.DTO;
using PackingListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackingListApp.Interfaces
{
    public interface IUserServices
    {
        List<UserModel> GetAllUsers();

        int AddUser(NewUserModel Usermodel);

        UserModel GetUser(int id);
        int PutUser(int id, UserModel item);
        object Delete(int id);
    }
}
