using Logic.Interfaces.Repositories.Images;
using Logic.Models.Images;
using Logic.Utilities;
using Shared.Enums;
using Shared.Errors;
using ImageEntity = Logic.Models.Images.Image;
using ResizableImage = SixLabors.ImageSharp.Image;

namespace Infrastructure.Repositories.Images
{
    public class ImageRepository : IImageRepository
    {
        public ImageRepository() { }
        public bool Create(ImageEntity Image)
        {
            try
            {
                using var image = ResizableImage.Load(Image.Data);

                image.Save(LocalPath.GetValidImagePath(Image.FilePath, Image.BranchId, Image.Type));
                if (ImageType.Background == Image.Type)
                {
                    if (image.Width > 256)
                    {
                        int height = image.Height * (256 / image.Width);
                        image.Mutate(x => x.Resize(height, 256));
                    }
                    image.Save(LocalPath.GetValidImagePath(Image.FilePath, Image.BranchId, ImageType.Thumbnail));
                }
            }
            catch
            {
                throw new ClientException("An error occurred while trying to upload the image");
            }

            return true;
        }
        public bool Update(ImageEntity image) => Create(image);
        public ImageEntity? GetById(Guid id)
        {
            return null;
        }
        public bool Delete(ImageEntity image)
        {
            //try
            //{
            //    if (image.Type == ImageType.Background || image.Type == ImageType.Thumbnail)
            //    {
            //        File.Delete(LocalPath.FindFullEventImagePath(image.BranchId, id)!);
            //        File.Delete(LocalPath.FindThumbnailEventImagePath(image.BranchId, id)!);
            //    }
            //    else if (ImageType.User == image.Type)
            //    {
            //        string? path = LocalPath.FindUserImagePath(image.BranchId, id);
            //        if (path != null)
            //        {
            //            File.Delete(path);
            //        }
            //    }
            //    else
            //    {
            //        File.Delete(LocalPath.FindSharedImagePath(image.BranchId, id)!);
            //    }
            //}
            //catch
            //{
            //    throw new DeveloperException("Couldn't delete image");
            //}

            return true;
        }
        public List<ImageEntity> GetAll(int? offset = 0)
        {
            return new();
        }
        public bool DeleteType(Guid id, ImageType type)
        {
            return true;
        }
    }
}
