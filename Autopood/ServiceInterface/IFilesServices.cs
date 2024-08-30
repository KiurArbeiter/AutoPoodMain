﻿using Autopood.Domain;
using Autopood.Dto;

namespace Autopood.ServiceInterface
{
    public interface IFilesServices
    {
        void UploadFilesToDatabase(PlaneDto dto, Plane domain);

        Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto);
        Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);
        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
    }
}