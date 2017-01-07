using MusicViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicViewer.Controllers
{
    [RoutePrefix("album")]
    public class AlbumController : MasterController
    {
        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            AlbumViewModel viewModel = new AlbumViewModel
            {
                Albums = AlbumRepo.GetAlbums()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(AlbumViewModel viewModel)
        {
            viewModel.Albums = AlbumRepo.GetAlbums(viewModel.Term);
            return View(viewModel);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            Album album = AlbumRepo.GetAlbum(id);
            album.Songs = SongRepo.GetAllSongsByAlbum(id);
            return View(album);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public ActionResult Edit(int id)
        {
            Album album = AlbumRepo.GetAlbum(id);
            return View(album);
        }
    }
}