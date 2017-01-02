using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicViewer.Helpers
{
    class Util
    {
        private static List<string> files = new List<string>();
        private static List<TagLib.File> tags = new List<TagLib.File>();
        public  void ScanDirectory(string path, ref List<string> outputFiles)
        {
            if (!Directory.Exists(path)) return;
            string[] files = Directory.GetFiles(path);
            outputFiles.AddRange(files.Where(file => file.EndsWith(".mp3")));
            string[] directories = Directory.GetDirectories(path);
            foreach (string dir in directories)
            {
                ScanDirectory(dir, ref outputFiles);
            }
        }

        //public static void ProcessFiles(ref List<string> filepaths, ref List<TagLib.File> files)
        //{
        //    foreach (string file in filepaths)
        //    {
        //        ThreadStart start = new ThreadStart(GetMetaData);
        //        System.Threading.Thread newThread = new System.Threading.Thread(GetMetaData(ref files, file);
        //    }
        //}

        public void GetMetaData(string file)
        {
            try
            {
                tags.Add(TagLib.File.Create(file));
            }
            catch (PathTooLongException e)
            {
                PrintLine(e.ToString() + $"erra {file}");
            }
        }

        public List<TagLib.File> GetTags()
        {
            return tags;
        }

        private static void PrintLine(string file)
        {
            Console.WriteLine(file);
        }
    }
}
