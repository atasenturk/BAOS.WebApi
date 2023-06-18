using BAOS.ModelRunner;
using BAOS.Web.Data.Contracts;
using BAOS.Web.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAOS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BAOSModelController : ControllerBase
    {
        private readonly IRequestRepository _requestRepository;

        public BAOSModelController(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        [HttpPost]
        public async Task<IActionResult> RunModel(ModelFeatures features)
        {
            BAOSModel model = new BAOSModel();
            //string result = model.Run(features);
            string result = "LAN";
            int protocol;
            switch (result)
            {
                case "LAN":
                    protocol = 1;
                    break;
                case "WAN":
                    protocol = 2;
                    break;
                case "LPWAN":
                    protocol = 3;
                    break;
                default: protocol = 0;
                    break;
            }

            if (await _requestRepository.AddRequest(features.userId, features.answers, protocol))
            {
                return Ok(result);
            }
            else return BadRequest();
        }
    }
}
