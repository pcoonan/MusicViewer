using MusicViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicViewer.IRepos
{
    public interface IAlbumRepo
    {
        Album GetAlbum(int id);
        IEnumerable<Album> GetAlbums(string term = null);
    }
}
