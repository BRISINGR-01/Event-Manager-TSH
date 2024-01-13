using Logic.Interfaces.Repositories.Images;
using Logic.Models.Images;
using Shared.Enums;

namespace Mocks.Repositories
{
    public class ImageRepository : MockRepository<Image>, IImageRepository
    {
        public ImageRepository() : base()
        {
            AddData(new Image(MockData.ImageIds[0], MockData.BranchIds[0], "", new MemoryStream(), ImageType.Shared));
        }
        public bool DeleteType(Guid id, ImageType type)
        {
            return true;
        }
    }
}
