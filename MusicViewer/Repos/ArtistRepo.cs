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
    class ArtistRepo : SqliteBaseRepo, IArtistRepo
    {
        public Artist Get(int id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Artist> result = cnn.Query<Artist>(
                    @"SELECT *
                      FROM Artist
                      WHERE Id = @id",
                    new { id });
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<Artist> GetArtists(string term = null)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                IEnumerable<Artist> result = cnn.Query<Artist>(
                    @"SELECT *
                      FROM Artist
                      WHERE Name LIKE @Term || '%';",
                    new { Term = term ?? "" });
                return result;
            }
        }

    }
}
