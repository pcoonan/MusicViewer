using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicViewer.Helpers
{
    class DataContext : IDisposable
    {
        private SQLiteConnection _SongDB;

        public SQLiteConnection SongDB
        {
            get
            {
                return _SongDB == null ? _SongDB = SimpleDbConnection() : _SongDB;
            }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            var conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["MUSIC"].ConnectionString);
            conn.Open();
            return conn;
        }

        public void Dispose()
        {
            SongDB.Close();
        }
    }
}
