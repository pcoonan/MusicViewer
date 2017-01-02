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
using System.Xml;

namespace MusicViewer.Helpers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> files = new List<string>();
            Util util = new Util();
            util.ScanDirectory(args[0], ref files);
            ProcessFiles(ref files, util);
            //GenerateXML();
            //var mismatch = from tag in tags
            //               where tag.Tag.AlbumArtists.FirstOrDefault() != tag.Tag.Performers.FirstOrDefault()
            //               select tag;
            Console.Write($"{files.Count} files exist. Press any key to exit.");
            Console.ReadKey();
        }

        private static void GenerateXML()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter writer = XmlWriter.Create(@"C:\data\vsworkspace\MusicXML\MusicXML\Outputs\library.xml", settings);
            ArtistRepo repo = new ArtistRepo();
            IEnumerable<Artist> artists = repo.GetArtists();
            foreach(Artist artist in artists)
            {
                writer.WriteStartAttribute("Artist");
                writer.WriteAttributeString("artistName", artist.Name);
                ProcessArtist(artist.Id, ref writer);
                writer.WriteEndAttribute();
            }
            writer.Close();
        }

        private static void ProcessArtist(long artistId, ref XmlWriter writer)
        {
            AlbumRepo repo = new AlbumRepo();
            IEnumerable<Album> albums = repo.GetAlbums(artistId);
            foreach (Album album in albums)
            {
                writer.WriteStartAttribute("Album");
                writer.WriteAttributeString("albumName", album.Name);
                writer.WriteAttributeString("year", album.Year.ToString());
                ProcessAlbum(album.Id, ref writer);
                writer.WriteEndAttribute();
            }
        }

        private static void ProcessAlbum(long albumId, ref XmlWriter writer)
        {
            SongRepo repo = new SongRepo();
            IEnumerable<Song> songs = repo.GetAllSongsByAlbum(albumId);
            foreach (Song song in songs)
            {
                writer.WriteStartAttribute(song.Title);
                writer.WriteAttributeString("track", song.Track.ToString());
                writer.WriteAttributeString("path", song.Path);
                writer.WriteEndAttribute();
            }
        }

        public static void ScanDirectory(string path, ref List<string> outputFiles)
        {
            if (!Directory.Exists(path)) return;
            string[] files = Directory.GetFiles(path);
            outputFiles.AddRange(files.Where(file => file.EndsWith(".mp3")));
            string[] directories = Directory.GetDirectories(path);
            foreach(string dir in directories)
            {
                ScanDirectory(dir, ref outputFiles);
            }
        }

        public static void ProcessFiles(ref List<string> filepaths, Util util)
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
                    PrintLine(e.ToString() + $"errah {file}");
                    continue;
                }
                Song s = new Song();
                s.Title = tags.Tag.Title;
                s.Artist.Name = tags.Tag.Performers.FirstOrDefault();
                s.Album.Name = tags.Tag.Album;
                s.Album.Year = (int) tags.Tag.Year;
                s.Track = (int) tags.Tag.Track;
                s.Path = file;
               
                Console.WriteLine("Errah: " + repo.AddSong(s));
                //System.Threading.Thread newThread = new System.Threading.Thread(() => util.GetMetaData(file));
                //newThread.Start();
            }
        }

        private static void PrintLine(object file)
        {
            Debug.WriteLine(file);
        }

        private static void Print(object file)
        {
            Debug.Write(file);
        }



        //public void GetMetaData()
        //{
        //    ref List<TagLib.File> files; string file;
        //    try
        //    {
        //        files.Add(TagLib.File.Create(file));
        //    }
        //    catch (PathTooLongException e)
        //    {
        //        PrintLine(file);
        //    }
        //}
    }
}
