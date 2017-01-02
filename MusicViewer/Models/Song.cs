using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicViewer.Models
{
    public class Song
    {

        public Song(int albumId)
        {
            Album = new Album(albumId);
        }

        public Song()
        {
            Album = new Album();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Track { get; set; }
        public string Path { get; set; }

        public Album Album { get; set; }
        public Artist Artist
        {
            get { return Album?.Artist; }
            set { Album.Artist = value; }
        }
    }
}
