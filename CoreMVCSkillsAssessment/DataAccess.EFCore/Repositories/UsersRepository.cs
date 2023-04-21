using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.EFCore.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(AppContext context):base(context) { 
        }
        public User GetUser(int? id)
        {
            return _context.Users.Include(u => u.EmpType).Single(u => u.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return GetUsersByEmail(email).FirstOrDefault();
        }

        public int GetUserIdByEmail(string email)
        {
            return GetUsersByEmail(email).FirstOrDefault()?.Id ?? 0;
        }

        public bool EmailExists(string email)
        {
            return GetUsersByEmail(email).Any();
        }

        public bool CanUserLogin(User user)
        {
            return user.IsActive;
        }

        //public IEnumerable<SelectListItem> GetEmployeeTypeList()
        //{
        //    return _context.EmployeeTypes.OrderBy(e => e.SortOrder)
        //        .Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() })
        //        .ToList();
        //}

        private IQueryable<User> GetUsersByEmail(string email)
        {
            return _context.Users.Include(u => u.EmpType)
                .Where(u => u.Email.ToUpper() == email.ToUpper());
        }
    }
}
