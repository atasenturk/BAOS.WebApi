using AutoMapper;
using BAOS.Web.Data.Contracts;
using BAOS.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Web.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BAOS.Tests.ApiFixtures
{
    [TestFixture]
    public class RequestControllerFixture
    {
        private Mock<IUserRepository> mockUserRepo;
        private Mock<IResultRepository> mockResultRepo;
        private Mock<IRequestRepository> mockRequestRepo;
        private RequestController _requestController;
        private Mock<IMapper> _mapper;


        [OneTimeSetUp]
        public async Task Setup()
        {
            mockUserRepo = new Mock<IUserRepository>();
            mockResultRepo = new Mock<IResultRepository>();
            mockRequestRepo = new Mock<IRequestRepository>();
            _mapper = new Mock<IMapper>();
            _requestController = new RequestController(mockUserRepo.Object, _mapper.Object, mockResultRepo.Object,
                mockRequestRepo.Object);
        }


        [Test]
        public async Task GetResultsByUserIdTest()
        {
            mockResultRepo.Setup(q => q.GetAllRequestsById(1))
                .ReturnsAsync(new List<UserRequest>
                {
                    new()
                    {
                        Answers = "123123",
                        Protocol = 1,
                        RequestTime = DateTime.Now,
                    }
                });

            var result = await _requestController.GetResultsByUserId(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetRequestById()
        {
            mockRequestRepo.Setup(q => q.GetRequestById(1))
                .ReturnsAsync(new UserRequest
                {
                    Answers = "123123",
                    Protocol = 1,
                    RequestTime = DateTime.Now,
                });

            var result = await _requestController.GetRequestById(1);
            Console.WriteLine(result.ToString());
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }

}
