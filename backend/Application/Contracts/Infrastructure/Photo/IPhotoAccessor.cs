using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Contracts.Infrastructure.Photo
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);

        Task<PhotoUploadResult> UpdatePhoto(IFormFile file, string publicId);

        Task<string> DeletePhoto(string publicId);
    }
}