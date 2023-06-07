using Shared;
using Shared.Errors;
using Logic.Models.Images;
using Logic.Interfaces.Repositories;
using Logic;
using Shared.Enums;

namespace Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private Guid branchId;

        public Guid BranchId { get => branchId; }
        public ImageRepository(string connectionString, Guid branchId)
        {
            this.branchId = branchId;
        }
        public List<Logic.Models.Images.Image> GetAll(int? offset) => throw new DeveloperException("This is not the intended use");

        public bool Create(Logic.Models.Images.Image Image)
        {
            try
            {
                using var image = SixLabors.ImageSharp.Image.Load(Image.Data);

                switch (Image.Type)
                {
                    case ImageType.Thumbnail:
                    case ImageType.Background:
                        image.Save(LocalPath.FullEventImagePath(BranchId, Image.FilePath));

                        if (image.Width > 256)
                        {
                            int height = image.Height * (256 / image.Width);
                            image.Mutate(x => x.Resize(height, 256));
                        }

                        image.Save(LocalPath.ThumbnailImagePath(BranchId, Image.FilePath));
                        break;
                    case ImageType.User:
                        image.Save(LocalPath.UserImagePath(BranchId, Image.FilePath));
                        break;
                    case ImageType.Shared:
                        image.Save(LocalPath.SharedImagePath(BranchId, Image.FilePath));
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                throw new ClientException("An error occurred while trying to upload the image");
            }

            return true;
        }
        public bool Update(Logic.Models.Images.Image Image) => Create(Image);
        public bool Delete(Guid id) => throw new NotImplementedException();
        public bool DeleteType(Guid id, ImageType type)
        {
            try
            {
                if (type == ImageType.Background || type == ImageType.Thumbnail)
                {
                    File.Delete(LocalPath.FindFullEventImagePath(BranchId, id)!);
                    File.Delete(LocalPath.FindThumbnailEventImagePath(BranchId, id)!);
                }
                else if (ImageType.User == type)
                {
                    string? path = LocalPath.FindUserImagePath(BranchId, id);
                    if (path != null)
                    {
                        File.Delete(path);
                    }
                }
                else
                {
                    File.Delete(LocalPath.FindSharedImagePath(branchId, id)!);
                }
            }
            catch
            {
                throw new DeveloperException("Couldn't delete image");
            }

            return true;
        }
        public Logic.Models.Images.Image? FindSingleBy(Guid id) => throw new NotImplementedException();
    }
}
