using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MusicViewer.Helpers
{
    public class SqliteBaseRepo
    {
        public static string DbFile
        {
            get { return @"C:/data/vsworkspace/MusicXML/MusicXML/Databases/PublicDJDb.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection(ConfigurationManager.ConnectionStrings["MUSIC"].ConnectionString);
        }

    }
}
