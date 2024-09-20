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

        [HttpGet("create")]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel car = new CarCreateUpdateViewModel();
            return View("CreateUpdate", car);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Mark = vm.Mark,
                Description = vm.Description,
                Price = vm.Price,
                Model = vm.Model,
                Year = vm.Year,
                Register = vm.Register,
                SerialNumber = vm.SerialNumber,
                Engine = vm.Engine,
                Seats = vm.Seats,
                Milage = vm.Milage,
                Tires = vm.Tires,
                Inspection = vm.Inspection,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    CarId = x.CarId,
                }).ToArray()
            };
            var result = await _carsServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(IndexClient));
            }

            return RedirectToAction(nameof(IndexClient));
        }

        [HttpGet("update/{id}")]
        public async Task<IActionResult> Update(Guid id)
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

            var vm = new CarCreateUpdateViewModel
            {
                Id = car.Id,
                Price = car.Price,
                Mark = car.Mark,
                Milage = car.Milage,
                Description = car.Description,
                Model = car.Model,
                Year = car.Year,
                Register = car.Register,
                SerialNumber = car.SerialNumber,
                Engine = car.Engine,
                Tires = car.Tires,
                Seats = car.Seats,
                Inspection = car.Inspection,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Price = vm.Price,
                Inspection = vm.Inspection,
                Milage = vm.Milage,
                Mark = vm.Mark,
                Engine = vm.Engine,
                Register = vm.Register,
                Tires = vm.Tires,
                Seats = vm.Seats,
                Year = vm.Year,
                SerialNumber = vm.SerialNumber,
                Model = vm.Model,
                Description = vm.Description,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    CarId = x.CarId,
                }).ToArray()
            };
            var result = await _carsServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(IndexClient));
            }

            return RedirectToAction(nameof(IndexClient));
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
                Milage = car.Milage,
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

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
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
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData)),
                }).ToArrayAsync();

            var vm = new CarDeleteViewModel
            {
                Id = car.Id,
                Price = car.Price,
                Model = car.Model,
                Milage = car.Milage,
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

            return View(vm);
        }

        [HttpPost("delete-confirmation")]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _carsServices.Delete(id);
            if (carId == null)
            {
                return RedirectToAction(nameof(IndexClient));
            }

            return RedirectToAction(nameof(IndexClient));
        }

        [HttpPost("remove-image")]
        public async Task<IActionResult> RemoveImage(ImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };
            var image = await _filesServices.RemoveImage(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(IndexClient));
            }

            return RedirectToAction(nameof(IndexClient));
        }
    }
}
