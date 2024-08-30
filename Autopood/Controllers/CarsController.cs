using Autopood.Dto;
using Autopood.Models.Car;
using Autopood.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Autopood.Controllers
{
    public class CarsController : Controller
    {
        private readonly AutopoodContext _context;
        private readonly ICarsServices _carsServices;
        private readonly IFilesServices _filesServices;

        public CarsController
            (
                AutopoodContext context,
                ICarsServices carsServices,
                IFilesServices filesServices
            )
        {
            _context = context;
            _carsServices = carsServices;
            _filesServices = filesServices;
        }

        public IActionResult Index()
        {
            var result = _context.Cars
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Mark = x.Mark,
                    Model = x.Model,
                    Seats = x.Seats,
                    Engine = x.Engine,
                });
            return View(result);
        }
    }
    [HttpGet]
    public IActionResult Create()
    {
        CarCreateUpdateViewModel car = new CarCreateUpdateViewModel();
        return View("CreateUpdate", car);
    }
    [HttpPost]
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
            return RedirectToAction(nameoff(Index));
        }

        return RedirectToAction(nameoff(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var car = await _carsServices.GetAsync(id)
    }
}
