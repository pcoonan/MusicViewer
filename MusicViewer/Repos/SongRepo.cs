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
    public class SongRepo : SqliteBaseRepo, MusicViewer.IRepos.ISongRepo
    {
        public long AddSong(Song song)
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                song.Artist.Id = cnn.Query<int>(
                    @"INSERT INTO Artist 
                     SELECT null, @Name
                     WHERE NOT EXISTS(SELECT 1 FROM Artist WHERE Name = @Name);
                     SELECT Id
                     FROM Artist
                     WHERE Name=@Name;", new { Name = song.Artist.Name }).FirstOrDefault();
                song.Album.Id = cnn.Query<int>(
                    @"INSERT INTO Album 
                     SELECT null, @Name, @ArtistId, @Year
                     WHERE NOT EXISTS(SELECT 1 FROM Album WHERE Name=@Name AND ArtistId=@ArtistId AND Year=@Year);
                     SELECT Id
                     FROM Album
                     WHERE Name = @Name AND Id = @ArtistId AND Year = @Year;", 
                    new { Name = song.Album.Name, ArtistId = song.Artist.Id, Year = song.Album.Year }).FirstOrDefault();
                song.Id = cnn.Query<int>(
                    @"INSERT INTO Song 
                     SELECT null, @Title, @AlbumId, @Track, @Path
                     WHERE NOT EXISTS(SELECT 1 FROM Song WHERE Title=@Title AND AlbumId=@AlbumId AND Track=@Track);
                     SELECT Id
                     FROM Song
                     WHERE Title=@Title AND AlbumId=@AlbumId AND Track=@Track;",
                    new { Title = song.Title, AlbumId = song.Album.Id, Track = song.Track, Path = song.Path}).FirstOrDefault();
                return song.Id;
            }
        }

        public IEnumerable<Song> GetAllSongs()
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Song> result = cnn.Query<Song, Album, Artist, Song>(
                    @"SELECT *
                      FROM Song s
                            inner join Album al
                                on s.AlbumId=al.Id
                            inner join Artist ar
                                on al.ArtistId=ar.Id",
                    (song, album, artist) =>
                    {
                        album.Artist = artist;
                        song.Album = album;
                        return song;
                    });
                return result;
            }
        }

        public IEnumerable<Song> GetAllSongsByAlbum(long id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Song> result = cnn.Query<Song, Album, Artist, Song>(
                    @"SELECT *
                      FROM Song s
                            inner join Album al
                                on s.AlbumId=al.Id
                            inner join Artist ar
                                on al.ArtistId=ar.Id
                      WHERE al.Id = @id", 
                    (song, album, artist) =>
                    {
                        album.Artist = artist;
                        song.Album = album;
                        return song;
                    },
                    new { id });
                return result;
            }
        }

        public IEnumerable<Song> GetAllSongsByArtist(long id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Song> result = cnn.Query<Song, Album, Artist, Song>(
                    @"SELECT *
                      FROM Song s
                            inner join Album al
                                on s.AlbumId=al.Id
                            inner join Artist ar
                                on al.ArtistId=ar.Id
                      WHERE ar.Id = @id",
                    (song, album, artist) =>
                    {
                        album.Artist = artist;
                        song.Album = album;
                        return song;
                    }, 
                    new { id });
                return result;
            }
        }

        public Song GetSong(long id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                Song result = cnn.Query<Song, Album, Artist, Song>(
                    @"SELECT *
                      FROM Song s
                            inner join Album al
                                on s.AlbumId=al.Id
                            inner join Artist ar
                                on al.ArtistId=ar.Id
                      WHERE s.Id = @id",
                    (song, album, artist) =>
                    {
                        album.Artist = artist;
                        song.Album = album;
                        return song;
                    }, 
                    new { id }).FirstOrDefault();
                return result;
            }
        }

        private static void CreateDatabase()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    @"create table if not exists Song
                      (
                         Id                             integer primary key,
                         Title                          varchar(100),
                         AlbumId                        integer not null,
                         Track                          integer,
                         Path                           varchar(300) not null
                      )");
                cnn.Execute(
                    @"create table if not exists Album
                      (
                         Id                             integer primary key,
                         Name                           varchar(100),
                         ArtistId                       integer not null,
                         Year                           integer
                      )");
                cnn.Execute(
                    @"create table if not exists Artist
                      (
                         Id                                   integer primary key,
                         Name                                 varchar(100)
                      )");
            }
        }

        public void UpdateSong(Song song)
        {
            if (!File.Exists(DbFile)) return;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Query<Song>(
                    @"UPDATE Song
                      SET Title=@Title, AlbumId=@Album_Id, Track=@Track, Path=@Path
                      WHERE Id = @Id", song);
                // Figure out logic for album/artist, maybe use the other repos
                // and specify whats being updated
            }
        }

        public void DeleteSong(long id)
        {
            if (!File.Exists(DbFile)) return;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Query<Song>(
                    @"DELETE FROM Song
                      WHERE Id = @id", new { id });
                // Add additional logic to delete album/artist if 
                // deleted song was last
            }
        }

        public IEnumerable<Song> GetAllSongs(string term)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Song> result = cnn.Query<Song>(
                    @"SELECT *
                      FROM Song s
                            inner join Album al
                                on s.AlbumId=al.Id
                            inner join Artist ar
                                on al.ArtistId=ar.Id
                      WHERE s.Title LIKE @term || '%'", new { term });
                return result;
            }
        }

        public SongRepo()
        {
            CreateDatabase();
        }
    }
}
