using Logic.Interfaces.Repositories.Base;
using Logic.Models.Images;

namespace Logic.Interfaces.Repositories.Images
{

    public interface IImageUserTagRepository : IRepository<UserTagImage>
    {
        public bool DeleteAllFromImage(Guid id);
        public bool DeleteAllFromUser(Guid id);
    }
}
