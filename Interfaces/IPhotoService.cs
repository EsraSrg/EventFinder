using CloudinaryDotNet.Actions;

namespace EventFinder.Interfaces
{
    public interface IPhotoService{

        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);


    }
}