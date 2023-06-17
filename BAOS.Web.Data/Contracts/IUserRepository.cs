using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Web.Domain.Models;
using BAOS.Web.Domain.ViewModels;

namespace BAOS.Web.Data.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> Register(User register); 
        Task<bool> Login(LoginViewModel model);
        Task<User> GetByEmail(string email);
        Task<User> UpdateAsync(User entity);
    }
}
