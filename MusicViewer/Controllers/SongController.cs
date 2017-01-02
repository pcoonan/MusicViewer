using MusicViewer.IRepos;
using MusicViewer.Models;
using MusicViewer.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicViewer.Controllers
{
    [RoutePrefix("song")]
    [Route("{action=index}")]

    public class SongController : MasterController
    {
        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            SongViewModel viewModel = new SongViewModel
            {
                Songs = SongRepo.GetAllSongs()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(SongViewModel viewModel)
        {
            viewModel.Songs = SongRepo.GetAllSongs(viewModel.Term);
            return View(viewModel);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            Song song = SongRepo.GetSong(id);
            return View(song);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public ActionResult Edit(int id)
        {
            Song song = SongRepo.GetSong(id);
            return View(song);
        }

        [HttpPost]
        public ActionResult Edit(Song song)
        {
            SongRepo.UpdateSong(song);
            return RedirectToAction("Get", new { id = song.Id });
        }

        [HttpGet]
        [Route("{albumId}/add")]
        public ActionResult Add(int albumId)
        {
            return View(new Song(albumId));
        }

        [HttpPost]
        public ActionResult Add(Song song)
        {
            SongRepo.AddSong(song);
            return RedirectToAction("Get", new { id = song.Id });
        }
    }
}