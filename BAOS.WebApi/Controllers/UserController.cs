﻿using AutoMapper;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Domain.Models;
using BAOS.Web.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAOS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (await _userRepository.Login(login))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            User user = _mapper.Map<User>(register);
            await _userRepository.Register(user);
            return Ok();
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}