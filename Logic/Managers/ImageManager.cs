using Logic.Interfaces.Repositories.Images;
using Logic.Utilities;
using Shared.Enums;

namespace Domain.Managers
{
    public class ImageManager
    {
        private IImageRepository repository;
        public ImageManager(IImageRepository repository)
        {
            this.repository = repository;
        }
        public Result Create(Logic.Models.Images.Image image)
        {
            return Result.From(() => repository.Create(image));
        }
        public Result DeleteType(Guid id, ImageType type)
        {
            return Result.From(() => repository.DeleteType(id, type));
        }
    }
}