using MusicViewer.Models;
using MusicViewer.Repos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicViewer.Helpers
{
    class Util
    {
        public static void ScanDirectory(string path, ref List<string> outputFiles)
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

        public static void ProcessFiles(ref List<string> filepaths)
        {
            SongRepo repo = new SongRepo();
            foreach (string file in filepaths)
            {
                TagLib.File tags = null;
                try
                {
                    tags = TagLib.File.Create(file);
                }
                catch (Exception e) when (e is PathTooLongException || e is TagLib.CorruptFileException)
                {
                    Debug.WriteLine(e.ToString() + $"Error reading tags for: {file}");
                    continue;
                }
                Song s = new Song();
                s.Title = tags.Tag.Title;
                s.Artist.Name = tags.Tag.Performers.FirstOrDefault();
                s.Album.Name = tags.Tag.Album;
                s.Album.Year = (int)tags.Tag.Year;
                s.Track = (int)tags.Tag.Track;
                s.Path = file;

                Debug.WriteLine("Song added: " + repo.AddSong(s));
            }
        }
    }
}
