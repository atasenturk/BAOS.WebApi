using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAOS.Tests.Mock;
using BAOS.Web.Data;
using BAOS.Web.Data.Services;
using BAOS.Web.Domain.Models;
using BAOS.WebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BAOS.Tests.ApiFixtures
{
    [TestFixture]
    public class UserControllerFixture
    {
        private Mock<UserRepository> _userRepository;
        private Mock<ResultRepository> _resultRepository;
        private UserController _userController;
        private Mock<IMapper> _mapper;
        [OneTimeSetUp]
        public async Task Setup()
        {
            _userRepository = new Mock<UserRepository>();
            _resultRepository = new Mock<ResultRepository>();
            _mapper = new Mock<IMapper>();
            _userController = new UserController(_userRepository.Object, _mapper.Object, _resultRepository.Object);
        }

        [Test]
        public void GetByEmail_Test()
        {
            _userRepository.Setup(q => q.GetByEmail("test@gmail.com"))
                .ReturnsAsync(new User()
                {
                    Email = "test@gmail.com"
                });

            var result = _userController.GetByEmail("test@gmail.com");

            Assert.AreEqual("test@gmail.com", result.Result.Email);
        }
    }
}
