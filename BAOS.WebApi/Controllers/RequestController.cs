using AutoMapper;
using BAOS.Web.Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAOS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public RequestController(IUserRepository userRepository, IMapper mapper, IResultRepository resultRepository, IRequestRepository requestRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _resultRepository = resultRepository;
            _requestRepository = requestRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetResultsByUserId(int userId)
        {
            var userRequest = await _resultRepository.GetAllRequestsById(userId);

            return Ok(userRequest);
        }

        [HttpGet("id/{requestId}")]
        public async Task<IActionResult> GetRequestById(int requestId)
        {
            var userRequest = await _requestRepository.GetRequestById(requestId);

            if (userRequest != null)
            {
                return Ok(userRequest);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
