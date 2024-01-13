using Shared.Enums;

namespace Logic.Utilities
{
    public static class LocalPath
    {
        private static string rootPath => Path.Combine(Environment.CurrentDirectory, "wwwroot");
        private static string imageFolderPath => Path.Combine(rootPath, "images");

        public static string GuidToPath(Guid id) => id.ToString().Replace("-", "");
        public static string? ToRelative(string? path) => path?.Replace(rootPath, "");

        public static string? FindImagePath(Guid id, Guid branchId, ImageType type)
        {
            string directory = Path.Combine(EnumToPath(type), GuidToPath(branchId));
            if (!Directory.Exists(directory)) return null;

            string fileName = GuidToPath(id);
            foreach (var file in Directory.GetFiles(directory))
            {
                if (Path.GetFileNameWithoutExtension(file) == fileName) return ToRelative(file);
            }

            return null;
        }

        public static string GetValidImagePath(string filePath, Guid branchId, ImageType type)
        {
            string directory = Path.Combine(EnumToPath(type), GuidToPath(branchId));
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            return Path.Combine(directory, filePath);
        }
        private static string EnumToPath(ImageType type)
        {
            return type switch
            {
                ImageType.User => Path.Combine(imageFolderPath, "user"),
                ImageType.Thumbnail => Path.Combine(imageFolderPath, "event-background", "thumbnail"),
                ImageType.Background => Path.Combine(imageFolderPath, "event-background", "full"),
                ImageType.Shared => Path.Combine(imageFolderPath, "shared"),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
