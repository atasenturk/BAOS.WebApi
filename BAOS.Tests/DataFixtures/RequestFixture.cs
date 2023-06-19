using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Tests.Mock;
using BAOS.Web.Data;
using BAOS.Web.Data.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BAOS.Tests.DataFixtures
{
    [TestFixture]
    public class RequestFixture
    {
        private DbContextOptions<BAOSDbContext> _options;
        private BAOSDbContext _context;
        private Mock<UserRepository> _userRepository;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var dbFactory = new MockDbFactory("BaosDB");
            _context = new BAOSDbContext(dbFactory.Options);
            _userRepository = new Mock<UserRepository>(_context);
            dbFactory.SeedData();
        }
    }
}
