using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rainbowhut1._0.Domain;
using Rainbowhut1._0.Helper;
using Rainbowhut1._0.Model;

namespace Rainbowhut1._0.Controllers
{
    //[EnableCors("RainbowhutPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadDataController : ControllerBase
    {
        private UploadDataDomain _uploaddataDomain;
        private readonly ILogger<UploadDataController> _logger;

        public UploadDataController(UploadDataDomain uploadDomain, ILogger<UploadDataController> logger)
        {
            _uploaddataDomain = uploadDomain;
            _logger = logger;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ProfileImageUpdate()
        {
            try
            {
                IFormFile files = Request.Form.Files[0];
                await _uploaddataDomain.ProfileImageUpdate(files);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GalleryImageAdd()
        {
            try
            {
                IFormFile files = Request.Form.Files[0];
                await _uploaddataDomain.GalleryImageAdd(files);
                return Ok();
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
                await _uploaddataDomain.SlideImageAdd(files);
                return Ok();
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
                return Ok(qrview);
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
                await _uploaddataDomain.GalleryImageDelete(id);
                return Ok();
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
                await _uploaddataDomain.SlideImageDelete(id);
                return Ok();
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
                return Ok(result);
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
                return Ok(result);
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
                return Ok(result);
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
                return Ok(result);
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
                byte[] temp_backToBytes = Convert.FromBase64String(qr.Data);
                return File(temp_backToBytes, qr.ContentType, qr.FileName);
            }


        }

    }
}
