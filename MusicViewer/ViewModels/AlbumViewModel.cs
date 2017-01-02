using MusicViewer.Models;
using System.Collections.Generic;

namespace MusicViewer.Models
{
    public class AlbumViewModel
    {
        public IEnumerable<Album> Albums { get; set; }
        public string Term { get; set; }
    }
}