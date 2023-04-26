using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Data.Utils;
using BAOS.Web.Domain.Models;
using BAOS.Web.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BAOS.Web.Data.Services
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly BAOSDbContext _context;

        public UserRepository(BAOSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> Register(User register)
        {
            var entity = await _context.Users
                .FirstOrDefaultAsync(q => q.Email == register.Email);
            if (entity == null)
            {
                register.Password = Encryptor.EncryptMD5(register.Password);

                return await base.AddAsync(register);
            }
            return null;
        }

        public async Task<bool> Login(LoginViewModel model)
        {
            var entity = await _context.Users
                .FirstOrDefaultAsync(q => q.Email == model.Email && q.Password == Encryptor.EncryptMD5(model.Password));

            return entity != null;
        }

        public async Task<User> GetByEmail(string email)
        {
            var entity = await _context.Users
                .FirstOrDefaultAsync(q => q.Email == email);

            return entity;
        }
    }
}
