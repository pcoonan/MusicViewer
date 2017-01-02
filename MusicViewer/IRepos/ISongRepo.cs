using MusicViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicViewer.IRepos
{
    public interface ISongRepo
    {
        Song GetSong(long id);
        long AddSong(Song song);
        void UpdateSong(Song song);
        void DeleteSong(long id);
        IEnumerable<Song> GetAllSongs();
        IEnumerable<Song> GetAllSongsByAlbum(long id);
        IEnumerable<Song> GetAllSongsByArtist(long id);
        IEnumerable<Song> GetAllSongs(string term);
    }
}
