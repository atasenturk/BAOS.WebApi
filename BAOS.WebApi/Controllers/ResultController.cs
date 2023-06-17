using AutoMapper;
using BAOS.Web.Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAOS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;

        public ResultController(IUserRepository userRepository, IMapper mapper, IResultRepository resultRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _resultRepository = resultRepository;
        }

    }
}
