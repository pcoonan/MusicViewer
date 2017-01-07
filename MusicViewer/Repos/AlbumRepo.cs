using Dapper;
using MusicViewer.Helpers;
using MusicViewer.IRepos;
using MusicViewer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicViewer.Repos
{
    class AlbumRepo : SqliteBaseRepo, IAlbumRepo
    {
        public Album GetAlbum(int id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Album> result = cnn.Query<Album, Artist, Album>(
                    @"SELECT *
                      FROM Album al
                      INNER JOIN Artist ar
                        ON al.ArtistId = ar.Id
                      WHERE al.Id = @id", 
                    (album, artist)=>
                    {
                        album.Artist = artist;
                        return album;
                    },
                    new { id });
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<Album> GetAlbums(string term = null)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Album> result = cnn.Query<Album, Artist, Album>(
                    @"SELECT *
                      FROM Album al
                      INNER JOIN Artist ar
                        ON al.ArtistId = ar.Id
                      WHERE al.Name LIKE @term || '%'",
                    (album, artist) =>
                    {
                        album.Artist = artist;
                        return album;
                    }, 
                    new { term = term ?? "" });
                return result;
            }
        }

        public IEnumerable<Album> GetAlbums(long id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Album> result = cnn.Query<Album, Artist, Album>(
                    @"SELECT *
                      FROM Album al
                      INNER JOIN Artist ar
                        ON al.ArtistId = ar.Id
                      WHERE al.ArtistId = @id",
                    (album, artist) =>
                    {
                        album.Artist = artist;
                        return album;
                    }, 
                    new { id });
                return result;
            }
        }

        public IEnumerable<Album> GetAlbumsByArtist(int id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Album> result = cnn.Query<Album, Artist, Album>(
                    @"SELECT *
                      FROM Album al
                      INNER JOIN Artist ar
                        ON al.ArtistId = ar.Id
                      WHERE ar.Id = @id",
                    (album, artist) =>
                    {
                        album.Artist = artist;
                        return album;
                    },
                    new { id});
                return result;
            }
        }
    }
}
