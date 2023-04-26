using BAOS.ModelRunner;
using BAOS.Web.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAOS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BAOSModelController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RunModel(ModelFeatures features)
        {
            BAOSModel model = new BAOSModel();
            //string result = model.Run(features);
            string result = "LAN";
            return Ok(result);
        }
    }
}
