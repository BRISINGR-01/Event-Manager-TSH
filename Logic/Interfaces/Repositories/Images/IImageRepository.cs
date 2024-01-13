using Logic.Interfaces.Repositories.Base;
using Logic.Models.Images;
using Shared.Enums;

namespace Logic.Interfaces.Repositories.Images
{
    public interface IImageRepository : IRepository<Image>
    {
        bool Create(Image image);
        bool DeleteType(Guid id, ImageType type);
        List<Image> GetAll(int? offset);
    }
}
