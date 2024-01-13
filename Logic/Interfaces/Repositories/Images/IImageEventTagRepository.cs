using Logic.Interfaces.Repositories.Base;
using Logic.Models.Images;

namespace Logic.Interfaces.Repositories.Images
{

    public interface IImageEventTagRepository : IRepository<EventTagImage>
    {
        public bool DeleteAllFromImage(Guid id);
        public bool DeleteAllFromEvent(Guid id);
        public List<EventTagImage> FindAllFromEvent(Guid EventId);
    }
}
