using System.Linq.Expressions;
using NUnit.Framework;
using NSubstitute;
using System.Threading.Tasks;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Data.Services;
using BAOS.Web.Domain.Models;
using BAOS.Web.Data;
using Microsoft.EntityFrameworkCore;
using BAOS.Web.Data.Utils;
using BAOS.Web.Domain.ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.Win32;
using BAOS.Tests.Mock;

[TestFixture]
public class UserRepositoryTests
{
    private DbContextOptions<BAOSDbContext> _options;
    private BAOSDbContext _context;
    private Mock<UserRepository> _userRepository;

    [OneTimeSetUp]
    public async Task Setup()
    {
        var dbFactory = new MockDbFactory("BAOSDb");
        _context = new BAOSDbContext(dbFactory.Options);
        _userRepository = new Mock<UserRepository>(_context);
        dbFactory.SeedData();
    }


    [Test]
    public async Task Register_UniqueUser_ReturnsUser()
    {
        var user = new User()
        {
            UserName = "test123",
            Email = "test123@gmail.com",
            Password = "pass",
            Requests = new List<Request>()
        };
        

        var result = (await _userRepository.Object.Register(user));
        // Assert
        Assert.AreEqual(result.UserName, user.UserName);
        Assert.AreEqual(result.Email, user.Email);

    }

    [Test]
    public async Task Login_Test()
    {
        var returnValue = new User()
        {
            Id = 100,
            UserName = "test",
            Email = "test@gmail.com",
            Password = "password",
            Requests = new List<Request>()
        };

        _context.Users.Add(returnValue);

        var result = await _userRepository.Object.Login(new LoginViewModel()
        {
            Email = "test321@gmail.com",
            Password = "pass"
        });

        Assert.AreEqual(result, true);

    }

    [Test]
    public async Task GetByEmail_Test()
    {
        var returnValue = new User()
        {
            UserName = "test",
            Email = "test@gmail.com",
            Password = "password",
            Requests = new List<Request>()
        };

        _context.Users.Add(returnValue);
        var result = await _userRepository.Object.GetByEmail("test@gmail.com");


        Assert.AreEqual(result.Email, returnValue.Email);
        Assert.AreEqual(result.Id, returnValue.Id);
        Assert.AreEqual(result.UserName, returnValue.UserName);
    }

    [Test]
    public async Task DeleteById_Test()
    {
        var returnValue = new User()
        {
            Id = 100,
            UserName = "test",
            Email = "test@gmail.com",
            Password = "password",
            Requests = new List<Request>()
        };

        _context.Users.Add(returnValue);

        var result =  await _userRepository.Object.DeleteById(100);
        // Act
        // Assert
        Assert.AreEqual(result, true);
    }

    [Test]
    public async Task UpdateAsync_Test()
    {
        var returnValue = new User()
        {
            UserName = "test",
            Email = "test@gmail.com",
            Password = "password",
            Requests = new List<Request>()
        };

        _context.Users.Add(returnValue);

        var returnValue2 = new User()
        {
            UserName = "test",
            Email = "test@gmail.com",
            Password = "password",
            Requests = new List<Request>()
        };

        var result = await _userRepository.Object.UpdateAsync(returnValue2);
        // Act
        // Assert
        Assert.AreEqual(result, returnValue2.UserName);

    }
}
