using Logic.Interfaces;
using Logic.Utilities;
using Shared.Enums;
using Shared.Errors;

namespace Logic.Models.Images
{
    public class Image : IEntity
    {
        public Guid Id { get; private set; }
        public Guid BranchId { get; private set; }
        public byte[] Data { get; private set; }
        public string FilePath { get; private set; }
        public ImageType Type { get; private set; }
        public static Image New(Guid imageId, Guid branchId, string fileName, Stream fs, ImageType type) => new(imageId, branchId, fileName, fs, type);
        public Image(Guid imageId, Guid branchId, string fileName, Stream fs, ImageType type)
        {
            Type = type;
            try
            {
                using BinaryReader br = new(fs);

                Data = br.ReadBytes((int)fs.Length);
            }
            catch
            {
                throw new ClientException("Couldn't upload image");
            }
            Id = imageId;
            BranchId = branchId;
            FilePath = LocalPath.GuidToPath(imageId) + Path.GetExtension(fileName);
        }
    }
}
