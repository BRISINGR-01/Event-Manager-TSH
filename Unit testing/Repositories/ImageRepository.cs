using Infrastructure.SQL;
using Logic;
using Shared;
using Shared.Errors;
using Logic.Models.Images;
using MySql.Data.MySqlClient;
using Infrastructure.DatabaseManagers;
using Logic.Interfaces.Repositories;

namespace Unit_testing.Repositories
{
    public class ImageRepository : MockRepository<Image>, IImageRepository
    {
        public ImageRepository() : base()
        {
            AddData(new Image(Guid.Parse("be23ec52-e0ec-4b42-8e19-ce8a5c757509"), "", new MemoryStream(), Shared.Enums.ImageType.Shared));
        }
        new public bool Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}
