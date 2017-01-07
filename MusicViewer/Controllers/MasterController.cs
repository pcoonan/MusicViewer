using MusicViewer.Helpers;
using MusicViewer.IRepos;
using MusicViewer.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MusicViewer.Controllers
{
    public class MasterController : Controller
    {
        protected ISongRepo SongRepo;
        protected IAlbumRepo AlbumRepo;
        protected IArtistRepo ArtistRepo;
        public MasterController()
        {
            SongRepo = new SongRepo();
            AlbumRepo = new AlbumRepo();
            ArtistRepo = new ArtistRepo();
        }

        public ActionResult RefreshLibrary()
        {
            ThreadStart start = new ThreadStart(BeginProcess);
            Thread childThread = new Thread(start);
            childThread.Start();
            return RedirectToAction("Index", "Home");
        }

        private static void BeginProcess()
        {
            List<string> files = new List<string>();
            //Util util = new Util();
            Util.ScanDirectory($"D:/Music", ref files);
            Util.ProcessFiles(ref files);
        }
    }
}