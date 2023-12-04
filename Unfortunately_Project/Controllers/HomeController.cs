using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Dynamic;
using Unfortunately_Project.Models;

namespace Unfortunately_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicApp musicApp;

        private void LoadArtists()
        {
            ViewBag.Artists = new SelectList(musicApp.Artists.ToList(), nameof(Artist.Id), nameof(Artist.FirstName), nameof(Artist.LastName));
        }
        private void LoadGenres()
        {
            ViewBag.Genres = new SelectList(musicApp.Genres.ToList(), nameof(Genre.Id), nameof(Genre.Name));
        }
        private void LoadPlaylists()
        {
            ViewBag.Playlists = new SelectList(musicApp.Playlists.ToList(), nameof(Playlist.Id), nameof(Playlist.Name));
        }

        public HomeController(MusicApp musicApp)
        {
            this.musicApp = musicApp;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var item = musicApp.Tracks.ToList();

            dynamic model = new ExpandoObject();

            model.Tracks = item;
            LoadArtists();
            LoadGenres();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}