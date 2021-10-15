using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackingListApp.Models;

namespace PackingListApp.Interfaces
{
    public interface IUserServices
    {
        List<UserModel> GetAll();

        int Add(NewUserModel testmodel);

        UserModel Get(int id);
        int Put(int id, UserModel item);

        void Delete(UserModel item);
    }
}
