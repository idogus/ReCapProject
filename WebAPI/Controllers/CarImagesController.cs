using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Utilities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageService _carImageService;
        private readonly IWebHostEnvironment _webHostEnv;
        public string DefaultImagePath { get; }

        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnv)
        {
            _carImageService = carImageService;
            _webHostEnv = webHostEnv;
            DefaultImagePath = Path.Combine(_webHostEnv.WebRootPath, @"Upload");
        }

        [HttpPost("add")]
        public IActionResult Post([FromForm(Name = ("file"))] IFormFile file, [FromForm] CarImage carImage)
        {
            var check = CheckFormValues(file, carImage);
            if (check != null) return BadRequest(check);

            //if (file == null || file.Length <= 0)
            //    return BadRequest("Dosya seçmediniz");

            //if (carImage.CarId == 0)
            //    return BadRequest("Araç seçmediniz");

            //var fileExtension = Path.GetExtension(file.FileName);
            //if (!(fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png"))
            //    return BadRequest(@"Seçilen dosya uygun değil! '*.jpg' || '*.png'");

            var imagePath = ImageUploader.ImageUpload(DefaultImagePath, file);

            carImage.ImagePath = imagePath.Result;

            var result = _carImageService.Add(carImage);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = "file")] IFormFile file, [FromForm] CarImage carImage)
        {
            var check = CheckFormValues(file, carImage);
            if (check != null) return BadRequest(check);

            var imagePath = ImageUploader.ImageUpload(DefaultImagePath, file);
            ImageUploader.ImageDelete(Path.Combine(DefaultImagePath, carImage.ImagePath));

            carImage.ImagePath = imagePath.Result;

            var result = _carImageService.Update(carImage);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("remove")]
        public IActionResult Post(CarImage carImage)
        {
            ImageUploader.ImageDelete(Path.Combine(DefaultImagePath, carImage.ImagePath));
            var result = _carImageService.Delete(carImage);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        private string CheckFormValues(IFormFile file, CarImage carImage)
        {
            if (file == null || file.Length <= 0)
                return "Dosya seçmediniz";

            if (carImage.CarId == 0)
                return "Araç seçmediniz";

            var fileExtension = Path.GetExtension(file.FileName);
            if (!(fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png"))
                return @"Seçilen dosya uygun değil! '*.jpg' || '*.png'";

            return null;
        }
    }
}
