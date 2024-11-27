using Microsoft.AspNetCore.Mvc;
using Autopood.Models.Car;
using Autopood.ServiceInterface;
using Autopood.Dto;
using Microsoft.EntityFrameworkCore;
using Autopood.Data;

namespace Autopood.Controllers
{
    [Route("client-cars")]
    public class CarsClientController : Controller
    {
        private readonly AutopoodContext _context;
        private readonly ICarsServices _carsServices;
        private readonly IFilesServices _filesServices;

        public CarsClientController(
            AutopoodContext context,
            ICarsServices carsServices,
            IFilesServices filesServices
        )
        {
            _context = context;
            _carsServices = carsServices;
            _filesServices = filesServices;
        }

        [HttpGet("index")]
        public IActionResult IndexClient()
        {
            var result = _context.Cars
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Mark = x.Mark,
                    Model = x.Model,
                    Seats = x.Seats,
                    Price = x.Price,
                    Year = x.Year,
                    Engine = x.Engine
                });

            return View("IndexClient", result);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> DetailsClient(Guid id)
        {
            var car = await _carsServices.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var photos = await _context.FilesToDatabase
                .Where(x => x.CarId == id)
                .Select(y => new ImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CarDetailsViewModel
            {
                Id = car.Id,
                Price = car.Price,
                Model = car.Model,
                Mileage = car.Mileage,
                Inspection = car.Inspection,
                SerialNumber = car.SerialNumber,
                Register = car.Register,
                Tires = car.Tires,
                Seats = car.Seats,
                Year = car.Year,
                Mark = car.Mark,
                Engine = car.Engine,
                Description = car.Description,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };
            vm.Image.AddRange(photos);

            return View("DetailsClient", vm);
        }


    }
}
