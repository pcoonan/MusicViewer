using System.Collections.Generic;
using System.ComponentModel;

namespace MusicViewer.Models
{
    public class Artist
    {
        public int Id { get; set; }
        [DisplayName("Artist")]
        public string Name { get; set; }
        public IEnumerable<Album> Albums { get; internal set; }

    }
}