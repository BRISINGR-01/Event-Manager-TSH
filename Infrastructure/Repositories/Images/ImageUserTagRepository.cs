using Infrastructure.Tables.Images;
using Logic.Interfaces.Repositories.Images;

namespace Infrastructure.Repositories.Images
{
    public class ImageUserTagRepository : DatabaseRepository, IImageUserTagRepository
    {
        public ImageUserTagRepository(DatabaseManager db) : base(db, ImageUserTagTable.TableName) { }
        public bool DeleteAllFromImage(Guid id)
        {
            sql.Delete
                .Where(ImageUserTagTable.ImagePath).Equals(id)
                .Execute();

            return true;
        }
        public bool DeleteAllFromUser(Guid id)
        {
            sql.Delete
                .Where(ImageUserTagTable.UserId).Equals(id)
                .Execute();

            return true;
        }
    }
}
