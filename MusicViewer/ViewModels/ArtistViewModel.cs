using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicViewer.Models
{
    public class ArtistViewModel
    {
        public IEnumerable<Artist> Artists { get; set; }
        public string Term { get; set; }
    }
}