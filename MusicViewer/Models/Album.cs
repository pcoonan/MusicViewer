using System.Collections.Generic;
using System.ComponentModel;

namespace MusicViewer.Models
{
    public class Album
    {
        public Album()
        {
            Artist = new Artist();
        }
        public Album(int albumId) : base()
        {
            Id = albumId;
        }

        public int Id { get; set; }
        [DisplayName("Album")]
        public string Name { get; set; }
        public int Year { get; set; }
        public Artist Artist { get; set; }
        public IEnumerable<Song> Songs { get; set; }
    }
}