using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAOS.Tests.Mock;
using BAOS.Web.Data;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Data.Services;
using BAOS.Web.Domain.Models;
using BAOS.Web.Domain.ViewModels;
using BAOS.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BAOS.Tests.ApiFixtures
{
    [TestFixture]
    public class UserControllerFixture
    {
        private Mock<IUserRepository> mockUserRepo;
        private Mock<IResultRepository> mockResultRepo;
        private UserController _userController;
        private Mock<IMapper> _mapper;


        [OneTimeSetUp]
        public async Task Setup()
        {
            mockUserRepo = new Mock<IUserRepository>();
            mockResultRepo = new Mock<IResultRepository>();
            _mapper = new Mock<IMapper>();
            _userController = new UserController(mockUserRepo.Object, _mapper.Object, mockResultRepo.Object);
        }

        [Test]
        public async Task GetByEmailTest()
        {
            mockUserRepo.Setup(q => q.GetByEmail("test@gmail.com"))
                .ReturnsAsync(new User()
                {
                    Id = 1,
                    Email = "test@gmail.com",
                    UserName = "12412",
                    Password = "124124",
                    Requests = new List<Request>()
                });

            var result = await _userController.GetByEmail("test@gmail.com");

            Assert.AreEqual("test@gmail.com", result.Email);
        }

        [Test]
        public async Task GetAllUsersTest()
        {
            mockUserRepo.Setup(q => q.GetAllAsync())
                .ReturnsAsync(new List<User>
                {
                    new ()
                    {
                        Id = 1,
                        Email = "test@gmail.com",
                        UserName = "12412",
                        Password = "124124",
                        Requests = new List<Request>()
                    }
                    
                });

            var result = await _userController.GetAllUsers();

            Assert.AreEqual("test@gmail.com", result[0].Email);
        }


        [Test]
        public async Task LoginTest()
        {
            var returnValue = new LoginViewModel() { Email = "test@gmail.com", Password = "test" };

            mockUserRepo.Setup(q => q.Login(returnValue))
                .ReturnsAsync(true);

            var result = await _userController.Login(returnValue);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task RegisterTest()
        {
            var returnValue = new User{ };

            mockUserRepo.Setup(q => q.Register(returnValue)).ReturnsAsync(returnValue);

            var result = await _userController.Login(new LoginViewModel
            {
                Email = "test@gmail.com",
                Password = "test"
            });

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task GetAllRequestsByIdTest()
        {
            mockResultRepo.Setup(q => q.GetAllRequestsById(1)).ReturnsAsync(new List<UserRequest>());

            var result = await _userController.GetAllRequestsById(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Update()
        {
            var user = new User
            {
                Email = "test@gmail.com",
                UserName = "12412",
                Password = "124124",
            };

            mockUserRepo.Setup(q => q.GetByIdAsync(1)).ReturnsAsync(user);

            mockUserRepo.Setup(q => q.UpdateAsync(user)).ReturnsAsync(user);

            var result = await _userController.Update(1, new RegisterViewModel()
            {
                Email = "test@gmail.com",
                UserName = "12412",
                Password = "124124",
            });

            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
