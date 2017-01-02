using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicViewer.Controllers
{
    public class ArtistController : MasterController
    {
        // GET: Artist
        public ActionResult Index()
        {
            return View();
        }
    }
}