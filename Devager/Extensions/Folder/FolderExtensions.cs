namespace Devager.Extensions.Folder
{
    using System;
    using System.IO;

    public static class FolderExtensions
    {
        public static void CreateFolder(this string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static Boolean FileFind(this String txt, string file)
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

        public static void FileSave(this string txt, string file)
        {
            using (var tmp = new StreamWriter(file, true))
            {
                tmp.WriteLine(txt);
            }
        }

    }
}
