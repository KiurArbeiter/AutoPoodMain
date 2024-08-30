using Autopood.Domain;
using Autopood.Dto;
using Autopood.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace Autopood.Services
{
    public class FilesServices : IFilesServices
    {
        private readonly AutopoodContext _context;
        private readonly IHostingEnvironment _webHost;
        public FilesServices(AutopoodContext context, IHostingEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        public void UploadFilesToDatabase(PlaneDto dto, Plane domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var photo in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = photo.FileName,
                            PlaneId = domain.Id,
                        };
                        photo.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FilesToDatabase.Add(files);
                    }
                }
            }
        }
        public async Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto)
        {
            var image = await _context.FilesToDatabase
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();
            _context.FilesToDatabase.Remove(image);
            await _context.SaveChangesAsync();
            return image;
        }
        public async Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var image = await _context.FilesToDatabase
                    .Where(x => x.Id == dto.Id)
                    .FirstOrDefaultAsync();
                _context.FilesToDatabase.Remove(image);
                await _context.SaveChangesAsync();
            }
            return null;
        }
        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var imageId = await _context.FilesToApi
                    .FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);
                var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\" + imageId.ExistingFilePath;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                _context.FilesToApi.Remove(imageId);
                await _context.SaveChangesAsync();
            }
            return null;
        }
        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            var imageId = await _context.FilesToApi
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\" + imageId.ExistingFilePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _context.FilesToApi.Remove(imageId);
            await _context.SaveChangesAsync();
            return null;
        }
    }
}