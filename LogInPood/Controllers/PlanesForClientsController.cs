using Autopood.Data;
using Autopood.Models.Plane;
using Autopood.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Autopood.Controllers
{
    public class PlanesForClientsController : Controller
    {
        private readonly AutopoodContext _context;
        private readonly IPlanesServices _planesServices;
        private readonly IFilesServices _filesServices;
        public PlanesForClientsController
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
    }
}