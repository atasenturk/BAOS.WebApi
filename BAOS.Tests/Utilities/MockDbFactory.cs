using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Web.Data;
using BAOS.Web.Data.Utils;
using BAOS.Web.Domain.Models;

namespace BAOS.Tests.Mock
{
    public sealed class MockDbFactory
    {
        public DbContextOptions<BAOSDbContext> Options { get; }

        public MockDbFactory(string dbName)
        {
            Options = new DbContextOptionsBuilder<BAOSDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        public void SeedData()
        {
            using (var context = new BAOSDbContext(Options))
            {
                context.Users.Add(new User()
                {
                    Id = 1,
                    UserName = "test321",
                    Email = "test321@gmail.com",
                    Password = Encryptor.EncryptMD5("pass"),
                    Requests = new List<Request>()
                });

                context.Requests.Add(new Request()
                {
                    Answers = "answers",
                    RequestTime = DateTime.Now,

                });

                context.SaveChanges();
            }
        }
    }
}
