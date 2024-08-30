using Autopood.Domain;
using Autopood.Dto;
using Autopood.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace Autopood.Services
{
    public class PlanesServices : IPlanesServices
    {
        private readonly AutopoodContext _context;
        private readonly IFilesServices _files;

        public PlanesServices(AutopoodContext context, IFilesServices files)
        {
            _context = context;
            _files = files;

        }

        public async Task<Plane> Create(PlaneDto dto)
        {
            Plane plane = new Plane();
            FileToDatabase file = new FileToDatabase();
            plane.Id = dto.Id;
            plane.Price = dto.Price;
            plane.Name = dto.Name;
            plane.Description = dto.Description;
            plane.CreatedAt = dto.CreatedAt;
            plane.ModifiedAt = dto.ModifiedAt;

            await _context.Planes.AddAsync(plane);
            if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, plane);
            }
            await _context.SaveChangesAsync();

            return plane;
        }
        public async Task<Plane> Update(PlaneDto dto)
        {
            var domain = new Plane()
            {
                Id = dto.Id,
                Price = dto.Price,
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = dto.ModifiedAt,
            };

            if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, domain);
            }

            _context.Planes.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
        public async Task<Plane> Delete(Guid id)
        {
            var planeId = await _context.Planes
                .FirstOrDefaultAsync(x => x.Id == id);
            _context.Planes.Remove(planeId);
            await _context.SaveChangesAsync();
            var images = await _context.FilesToDatabase
                .Where(x => x.PlaneId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                    ImageTitle = y.ImageTitle,
                    PlaneId = y.PlaneId
                }).ToArrayAsync();

            await _files.RemoveImagesFromDatabase(images);
            _context.Planes.Remove(planeId);

            return planeId;

        }
        public async Task<Plane> GetAsync(Guid id)
        {
            var result = await _context.Planes
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}