using Infrastructure.Tables.Images;
using Logic.Interfaces.Repositories.Images;
using Logic.Models.Images;

namespace Infrastructure.Repositories.Images
{

    public class ImageEventTagRepository : DatabaseRepository, IImageEventTagRepository
    {
        public ImageEventTagRepository(DatabaseManager db) : base(db, ImageEventTagTable.TableName) { }
        public bool DeleteAllFromImage(Guid id)
        {
            sql.Delete
                .Where(ImageEventTagTable.ImagePath).Equals(id)
                .Execute();

            return true;
        }
        public bool DeleteAllFromEvent(Guid id)
        {
            sql.Delete
                .Where(ImageEventTagTable.EventId).Equals(id)
                .Execute();

            return true;
        }

        public List<EventTagImage> FindAllFromEvent(Guid EventId)
        {
            return sql.Select
                .All
                .Where(ImageEventTagTable.EventId).Equals(EventId)
                .FinishSelect
                .Get<EventTagImage>();
        }
    }
}
