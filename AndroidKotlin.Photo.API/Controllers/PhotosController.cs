using AndroidKotlin.Photo.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndroidKotlin.Photo.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length >0)
            {
                var randomFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", randomFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);
                }

                var returnPath = "photos/" + randomFileName;

                return Ok(new {Url = returnPath});
            }
            else
            {
                return BadRequest("Photo is null");
            }
        }

        [HttpDelete]
        public  IActionResult PhotoDelete(PhotoDeleteDto photoDeleteDto)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoDeleteDto.Url);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return NoContent();
            }

            return BadRequest("Photo not found");
        }

    }
}
