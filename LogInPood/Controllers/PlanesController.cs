using Microsoft.AspNetCore.Mvc;
using Autopood.Models.Plane;
using Autopood.ServiceInterface;
using Autopood.Dto;
using Microsoft.EntityFrameworkCore;
using Autopood.Data;

namespace Autopood.Controllers
{
    public class PlanesController : Controller
    {
        private readonly AutopoodContext _context;
        private readonly IPlanesServices _planesServices;
        private readonly IFilesServices _filesServices;
        public PlanesController
            (
                AutopoodContext context,
                IPlanesServices planesServices,
                IFilesServices filesServices
            )
        {
            _context = context;
            _planesServices = planesServices;
            _filesServices = filesServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Planes
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new PlaneIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Model = x.Model,
                    Year = x.Year,
                    Register = x.Register,
                    SerialNumber = x.SerialNumber,
                    Engine = x.Engine,
                    Propeller = x.Propeller,
                    TotalTime = x.TotalTime,
                    Seats = x.Seats,
                    Inspection = x.Inspection,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt,
                });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            PlaneCreateUpdateViewModel plane = new PlaneCreateUpdateViewModel();
            return View("CreateUpdate", plane);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PlaneCreateUpdateViewModel vm)
        {
            var dto = new PlaneDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                Model = vm.Model,
                Year = vm.Year,
                Register = vm.Register,
                SerialNumber = vm.SerialNumber,
                Engine = vm.Engine,
                Propeller = vm.Propeller,
                TotalTime = vm.TotalTime,
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
                    PlaneId = x.PlaneId,
                }).ToArray()
            };
            var result = await _planesServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var plane = await _planesServices.GetAsync(id);
            if (plane == null)
            {
                return NotFound();
            }
            var photos = await _context.FilesToDatabase
                .Where(x => x.PlaneId == id)
                .Select(y => new ImageViewModel
                {
                    PlaneId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image.gif;base64,{0}", Convert.ToBase64String(y.ImageData))

                }).ToArrayAsync();
            var vm = new PlaneCreateUpdateViewModel();

            vm.Id = plane.Id;
            vm.Price = plane.Price;
            vm.Name = plane.Name;
            vm.Description = plane.Description;
            vm.Model = plane.Model;
            vm.Year = plane.Year;
            vm.Register = plane.Register;
            vm.SerialNumber = plane.SerialNumber;
            vm.Engine = plane.Engine;
            vm.Propeller = plane.Propeller;
            vm.TotalTime = plane.TotalTime;
            vm.Seats = plane.Seats;
            vm.Inspection = plane.Inspection;
            vm.CreatedAt = plane.CreatedAt;
            vm.ModifiedAt = plane.ModifiedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(PlaneCreateUpdateViewModel vm)
        {
            var dto = new PlaneDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                Model = vm.Model,
                Year = vm.Year,
                Register = vm.Register,
                SerialNumber = vm.SerialNumber,
                Engine = vm.Engine,
                Propeller = vm.Propeller,
                TotalTime = vm.TotalTime,
                Seats = vm.Seats,
                Inspection = vm.Inspection,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    PlaneId = x.PlaneId,
                }).ToArray()
            };
            var result = await _planesServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {

            var plane = await _planesServices.GetAsync(id);
            if (plane == null)
            {
                return NotFound();
            }
            var photos = await _context.FilesToDatabase
                .Where(x => x.PlaneId == id)
                .Select(y => new ImageViewModel
                {
                    PlaneId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
            var vm = new PlaneDetailsViewModel();

            vm.Id = plane.Id;
            vm.Price = plane.Price;
            vm.Name = plane.Name;
            vm.Description = plane.Description;
            vm.Model = plane.Model;
            vm.Year = plane.Year;
            vm.Register = plane.Register;
            vm.SerialNumber = plane.SerialNumber;
            vm.Engine = plane.Engine;
            vm.Propeller = plane.Propeller;
            vm.TotalTime = plane.TotalTime;
            vm.Seats = plane.Seats;
            vm.Inspection = plane.Inspection;
            vm.CreatedAt = plane.CreatedAt;
            vm.ModifiedAt = plane.ModifiedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var plane = await _planesServices.GetAsync(id);
            if (plane == null)
            {
                return NotFound();
            }
            var photos = await _context.FilesToDatabase
                .Where(x => x.PlaneId == id)
                .Select(y => new ImageViewModel
                {
                    PlaneId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData)),
                }).ToArrayAsync();

            var vm = new PlaneDeleteViewModel();


            vm.Id = plane.Id;
            vm.Price = plane.Price;
            vm.Name = plane.Name;
            vm.Description = plane.Description;
            vm.Model = plane.Model;
            vm.Year = plane.Year;
            vm.Register = plane.Register;
            vm.SerialNumber = plane.SerialNumber;
            vm.Engine = plane.Engine;
            vm.Propeller = plane.Propeller;
            vm.TotalTime = plane.TotalTime;
            vm.Seats = plane.Seats;
            vm.Inspection = plane.Inspection;
            vm.CreatedAt = plane.CreatedAt;
            vm.ModifiedAt = plane.ModifiedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var planeId = await _planesServices.Delete(id);
            if (planeId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };
            var image = await _filesServices.RemoveImage(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}