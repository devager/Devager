namespace Devager.Extensions.Folder
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class FolderExtensions
    {
        public static void CreateFolder(this string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static Boolean CreateFile(this String txt, string file)
        {
            if (!File.Exists(file))
            {
                using (var sw = File.CreateText(file))
                {
                    sw.WriteLine("Oluşturulma Tarihi : " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                }
                return false;
            }

            using (var sr = new StreamReader(file))
            {
                var contents = sr.ReadToEnd();

                if (contents.Contains(txt))
                    return true;
            }
            return false;


        }

        public static string KlasorVarmi(this string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }

        public static List<string> FileFind(string rootPath , string file)
        {
            return Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories)
                .Where(r => Path.GetFileName(r) == file)
                .ToList();
        }

        public static void FileSave(this string txt, string file)
        {
            using (var tmp = new StreamWriter(file, true))
            {
                tmp.WriteLine(txt);
            }
        }

        public static void NovetoDeleteFolder(this string file, string target)
        {
            File.Move(file, target);

            var path = Path.GetDirectoryName(file);
            var count = Directory.GetFiles(path);
            if (count.Length < 1)
            {
                Directory.Delete(path);
            }

        }
    }
}
