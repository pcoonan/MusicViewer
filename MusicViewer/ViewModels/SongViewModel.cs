using MusicViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicViewer.Models
{
    public class SongViewModel
    {
        public IEnumerable<Song> Songs { get; set; }
        public string Term { get; set; }
    }
}