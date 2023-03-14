using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BAOS.Web.Data.Services
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
