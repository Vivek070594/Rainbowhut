using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rainbowhut1._0.Domain;
using Rainbowhut1._0.Helper;
using Rainbowhut1._0.Model;

namespace Rainbowhut1._0.Controllers
{
    [EnableCors("RainbowhutPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadDataController : ControllerBase
    {
        private UploadDataDomain _uploaddataDomain;
        private readonly ILogger<UploadDataController> _logger;
        private readonly IConfiguration configuration;

        public UploadDataController(UploadDataDomain uploadDomain, ILogger<UploadDataController> logger, IConfiguration configuration)
        {
            _uploaddataDomain = uploadDomain;
            _logger = logger;
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ProfileImageUpdate()
        {
            try
            {
                IFormFile files = Request.Form.Files[0];
                int output= await _uploaddataDomain.ProfileImageUpdate(files);
                if (output == 1)
                {
                    return new JsonResult("Success");
                }
                else
                {
                    return new JsonResult("Failed");
                }
               // return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost, DisableRequestSizeLimit]
        [Route("[action]")]
        public async Task<IActionResult> GalleryImageAdd()
        {
            try
            {
                IFormFile files = Request.Form.Files[0];
                int output = await _uploaddataDomain.GalleryImageAdd(files);
                if (output == 1)
                {
                    return new JsonResult("Success");
                }
                else
                {
                    return new JsonResult("Failed");
                }
                //return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SlideImageAdd()
        {
            try
            {
                IFormFile files = Request.Form.Files[0];
                int output = await _uploaddataDomain.SlideImageAdd(files);
                if (output == 1)
                {
                    return new JsonResult("Success");
                }
                else
                {
                    return new JsonResult("Failed");
                }
                //return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> QrCodeFileAdd()
        {
            try
            {
                IFormFile files = Request.Form.Files[0];
                QrCodeViewModel qrview=await _uploaddataDomain.QrCodeFileAdd(files);
                if (qrview != null)
                {
                    return new JsonResult(qrview);
                }
                else
                {
                    return new JsonResult("Failed");
                }
                //return Ok(qrview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GalleryImageDelete([FromBody] int id)
        {
            try
            {
                int output = await _uploaddataDomain.GalleryImageDelete(id);
                if (output == 1)
                {
                    return new JsonResult("Success");
                }
                else
                {
                    return new JsonResult("Failed");
                }
                //return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SlideImageDelete([FromBody] int id)
        {
            try
            {
                int output = await _uploaddataDomain.SlideImageDelete(id);
                if (output == 1)
                {
                    return new JsonResult("Success");
                }
                else
                {
                    return new JsonResult("Failed");
                }
                //return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProfileImage()
        {
            try
            {
                var result = await this._uploaddataDomain.GetProfileImage();
                if (result != null)
                {
                    return new JsonResult(result);
                }
                else
                {
                    return new JsonResult("Failed");
                }
                //return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetSlideShowImage()
        {
            try
            {
                var result = await this._uploaddataDomain.GetSlideShowImage();
                if (result != null)
                {
                    return new JsonResult(result);
                }
                else
                {
                    return new JsonResult("Failed");
                }
                //return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetGalleryImage()
        {
            try
            {
                var result = await this._uploaddataDomain.GetGalleryImage();
                if (result != null)
                {
                    return new JsonResult(result);
                }
                else
                {
                    return new JsonResult("Failed");
                }
                //return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllImage()
        {
            try
            {
                var result = await this._uploaddataDomain.GetAllImage();
                if (result != null)
                {
                    return new JsonResult(result);
                }
                else
                {
                    return new JsonResult("Failed");
                }
                //return Ok(result);
                //return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> DownloadFiles(int id, Guid key)
        {

            QrCodeModel qr = await _uploaddataDomain.GetQrCodeFiles(id, key);
            if (qr == null)
            {
                Templates tmp = new Templates();
                return Content(tmp.NotFound().ToString(), "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                byte[] bytes = System.IO.File.ReadAllBytes(qr.Path);
                string filename = qr.Path.Split("-")[1];
                //byte[] temp_backToBytes = Convert.FromBase64String(qr.Data);
                return File(bytes, "application/pdf", filename);
            }


        }

    }
}
