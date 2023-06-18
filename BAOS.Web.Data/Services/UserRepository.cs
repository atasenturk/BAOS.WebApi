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
using Microsoft.Win32;

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
            if (await _context.Users.AnyAsync(q => q.Email == register.Email || q.UserName == register.UserName) )
            {
                return null;
            }

            register.Password = Encryptor.EncryptMD5(register.Password);
            return await base.AddAsync(register);
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

        public async Task<User>UpdateAsync(User entity)
        {
            entity.Password = Encryptor.EncryptMD5(entity.Password);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false; 
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
