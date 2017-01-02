using MusicViewer.IRepos;
using MusicViewer.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}