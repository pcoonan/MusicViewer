using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicViewer.Models;

namespace MusicViewer.IRepos
{
    public interface IArtistRepo
    {
        IEnumerable<Artist> GetArtists(string term = null);
        Artist Get(int id);
    }
}
