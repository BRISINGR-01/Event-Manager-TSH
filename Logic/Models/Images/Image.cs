using Logic.Interfaces;
using Shared;
using Shared.Enums;
using Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Images
{
    public class Image: IEntity
    {
        public Guid Id { get; private set; }
        public byte[] Data { get; private set; }
        public string FilePath { get; private set; }
        public ImageType Type { get; private set; }
        public Image(Guid id, string fileName)
        {
            FilePath = LocalPath.GuidToPath(id) + Path.GetExtension(fileName);
            Id = id;
            Data = Array.Empty<byte>();
            Type = default;
        }
        public Image(Guid id, string fileName, Stream fs, ImageType type)
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
            Id = id;
            FilePath = LocalPath.GuidToPath(id) + Path.GetExtension(fileName);
        }
    }
}
