using Autopood.Domain;
using Autopood.Dto;
using Autopood.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Autopood.Services
{
    public class CarsServices : ICarsServices
    {
        private readonly AutopoodContext _context;
        private readonly IFilesServices _files;

        public CarsServices(AutopoodContext context, IFilesServices files)
        {
            _context = context;
            _files = files;

        }

        public async Task<Car> Create(CarDto dto)
        {
            Car car = new Car();
            FileToDatabase file = new FileToDatabase();
            car.Id = dto.Id;
            car.Price = dto.Price;
            car.Mark = dto.Mark;
            car.Description = dto.Description;
            car.CreatedAt = dto.CreatedAt;
            car.ModifiedAt = dto.ModifiedAt;

            await _context.Cars.AddAsync(car);
            if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, car);
            }
            await _context.SaveChangesAsync();

            return car;
        }
        public async Task<Car> Update(CarDto dto)
        {
            var domain = new Car()
            {
                Id = dto.Id,
                Price = dto.Price,
                Mark = dto.Mark,
                Description = dto.Description,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = dto.ModifiedAt,
            };

            if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, domain);
            }

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();
            var images = await _context.FilesToDatabase
                .Where(x => x.CarId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                    ImageTitle = y.ImageTitle,
                    CarId = y.CarId
                }).ToArrayAsync();

            await _files.RemoveImagesFromDatabase(images);
            _context.Cars.Remove(carId);

            return carId;

        }
        public async Task<Car> GetAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}