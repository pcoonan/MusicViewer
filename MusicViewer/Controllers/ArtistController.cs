using MusicViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicViewer.Controllers
{
    [RoutePrefix("artist")]
    public class ArtistController : MasterController
    {
        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            ArtistViewModel viewModel = new ArtistViewModel
            {
                Artists = ArtistRepo.GetArtists()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(ArtistViewModel viewModel)
        {
            viewModel.Artists = ArtistRepo.GetArtists(viewModel.Term);
            return View(viewModel);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            Artist artist = ArtistRepo.Get(id);
            artist.Albums = AlbumRepo.GetAlbumsByArtist(id);
            return View(artist);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public ActionResult Edit(int id)
        {
            Artist artist = ArtistRepo.Get(id);
            artist.Albums = AlbumRepo.GetAlbumsByArtist(id);
            return View(artist);
        }
    }
}