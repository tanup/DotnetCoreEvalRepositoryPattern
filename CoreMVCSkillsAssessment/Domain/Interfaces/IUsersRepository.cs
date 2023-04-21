using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        User GetUser(int? id);

        User GetUserByEmail(string email);

        public int GetUserIdByEmail(string email);

        public bool EmailExists(string email);

        public bool CanUserLogin(User user);

        //public IEnumerable<SelectListItem> GetEmployeeTypeList();
    }
}
