using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rainbowhut1._0.Domain;
using Rainbowhut1._0.Model;

namespace Rainbowhut1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private MailDomain _mailDomain;
        private readonly ILogger<MailController> _logger;

        public MailController(MailDomain mailDomain, ILogger<MailController> logger)
        {
            _mailDomain = mailDomain;
            _logger = logger;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOtp()
        {
            try
            {
                int otp=await _mailDomain.SendOtp();
                return Ok(otp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SendEmail([FromBody] ContactUs contact)
        {
            try
            {
                await _mailDomain.SendEmailAsync(contact);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
