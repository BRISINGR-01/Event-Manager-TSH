using Logic.Managers;
using Logic;
using Logic.Models;
using Logic.Models.Images;
using Logic.Models.Events;
using static System.Net.Mime.MediaTypeNames;
using Shared;
using Shared.Errors;
using Shared.Enums;
using Logic.Interfaces.Repositories;

namespace Domain.Managers
{
    public class ImageManager : BaseManager<Logic.Models.Images.Image, IImageRepository>
    {
        public ImageManager(IImageRepository repository, IdentityUser user) : base(repository, user) { }
        public new Result Create(Logic.Models.Images.Image image)
        {
            var res = Result<bool>.From(() => VerifiedRepository().Create(image), CRUD.CREATE, "image");

            return res.IsSuccessful && res.Value ? Result.Success : res.Fail;
        }
    }
}