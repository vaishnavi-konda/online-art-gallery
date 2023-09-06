using ARTGALLERYRESTSERVICE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARTGALLERYRESTSERVICE.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        IConfiguration config;
        SecurityService service;
        public SecurityController(IConfiguration _config, SecurityService service)
        {
            config = _config;
            this.service = service;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(ArtGalleryCredentialsModel model)
        {
            TokenAndRole? tokenAndRole = service.AuthenticateUserAndGetToken(model);
            if (tokenAndRole == null)
            {
                return BadRequest("Invalid UserName or Password..");
            }
            else
                return Ok(tokenAndRole);
        }
    }
}
