using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class LocalPath
    {
        private static string rootPath { get => Path.Combine(Environment.CurrentDirectory, "wwwroot"); }
        private static string imageFolderPath { get => Path.Combine(rootPath, "images"); }
        public static string UserImageFolder { get => Path.Combine(imageFolderPath, "user"); }
        public static string FullBackgroundImageFolder { get => Path.Combine(imageFolderPath, "event-background", "full"); }
        public static string ThumbnailImageFolder { get => Path.Combine(imageFolderPath, "event-background", "thumbnail"); }
        public static string SharedImageFolder { get => Path.Combine(imageFolderPath, "shared"); }
        public static string GuidToPath(Guid id) => id.ToString().Replace("-", "");
        public static string? ToRelative(string? path) => path?.Replace(rootPath, "");
        private static string? FindImagePathFromId(Guid id, Guid branchId, string folder)
        {
            string fileName = GuidToPath(id);
            string directory = Path.Combine(folder, GuidToPath(branchId));
            if (!Directory.Exists(directory)) return null;

            foreach (var file in Directory.GetFiles(directory))
            {
                if (Path.GetFileNameWithoutExtension(file) == fileName) return file;
            }

            return null;
        }
        public static string FullEventImagePath(Guid branchId, string fileName) => Path.Combine(CheckFolder(FullBackgroundImageFolder, GuidToPath(branchId)), fileName);
        public static string? FindFullEventImagePath(Guid branchId, Guid id) => FindImagePathFromId(id, branchId, FullBackgroundImageFolder);
        public static string ThumbnailImagePath(Guid branchId, string fileName) => Path.Combine(CheckFolder(ThumbnailImageFolder, GuidToPath(branchId)), fileName);
        public static string? FindThumbnailEventImagePath(Guid branchId, Guid id) => FindImagePathFromId(id, branchId, ThumbnailImageFolder);
        public static string UserImagePath(Guid branchId, string fileName) => Path.Combine(CheckFolder(UserImageFolder, GuidToPath(branchId)), fileName);
        public static string? FindUserImagePath(Guid branchId, Guid id) => FindImagePathFromId(id, branchId, UserImageFolder);
        public static string SharedImagePath(Guid branchId, string fileName) => Path.Combine(CheckFolder(SharedImageFolder, GuidToPath(branchId)), fileName);
        public static string? FindSharedImagePath(Guid branchId, Guid id) => FindImagePathFromId(id, branchId, SharedImageFolder);
        private static string CheckFolder(params string[] folders)
        {
            string path = Path.Combine(folders);;
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
