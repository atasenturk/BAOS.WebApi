using AutoMapper;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Domain.Models;
using BAOS.Web.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BAOS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper, IResultRepository resultRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _resultRepository = resultRepository;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
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
            
            var result = await _userRepository.Register(user);
            if (result == null) return BadRequest();
            return Ok();
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        [HttpGet("{email}")]
        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterViewModel userViewModel)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            _mapper.Map(userViewModel, existingUser);

            var updatedUser = await _userRepository.UpdateAsync(existingUser);

            if (updatedUser == null)
            {
                return BadRequest(); 
            }

            return Ok(updatedUser);
        }

        [HttpGet("requests/{userId}")]
        public async Task<IActionResult> GetAllRequestsById(int userId)
        {
            var userRequest = await _resultRepository.GetAllRequestsById(userId);

            return Ok(userRequest);
        }

    }
}
