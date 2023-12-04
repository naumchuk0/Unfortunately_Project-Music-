using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.Runtime.ConstrainedExecution;
using Unfortunately_Project.Models;

namespace Unfortunately_Project.Controllers
{
    public class MusicController : Controller
    {
        private readonly MusicApp musicApp;
        public MusicController(MusicApp musicApp)
        {
            this.musicApp = musicApp;
        }
        private void LoadArtists()
        {
            ViewBag.Artists = new SelectList(musicApp.Artists.ToList(), nameof(Artist.Id), nameof(Artist.FirstName), nameof(Artist.LastName));
        }
        private void LoadFavorites()
        {
            ViewBag.Favorites = new SelectList(musicApp.Favorites.ToList(), nameof(Favorite.Id), nameof(Favorite.UserName), nameof(Favorite.TrackId));
        }
        private void LoadGenres()
        {
            ViewBag.Genres = new SelectList(musicApp.Genres.ToList(), nameof(Genre.Id), nameof(Genre.Name));
        }
        private void LoadPlaylists()
        {
            ViewBag.Playlists = new SelectList(musicApp.Playlists.ToList(), nameof(Playlist.Id), nameof(Playlist.Name));
        }
        private void LoadTracks()
        {
            ViewBag.Tracks = new SelectList(musicApp.Tracks.ToList(), nameof(Track.Id), nameof(Track.Name), nameof(Track.ArtistId), nameof(Track.GenreId));
        }
        [HttpGet]
        public IActionResult LoadMusic()
        {
            LoadGenres();
            LoadArtists();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoadMusic(CreateMusicModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadGenres();
                LoadArtists();

                return View(model);
            }

            Track track = new()
            {
                Name = model.Name,
                GenreId = model.GenreId,
                ArtistId = model.ArtistId
            };

            if (model.Audio != null && model.Audio.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/file/audio/", model.Audio.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Audio.CopyTo(stream);
                }
                track.Audio = model.Audio.FileName;
            }
            if (model.Img != null && model.Img.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/file/img/", model.Img.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Img.CopyTo(stream);
                }
                track.Img = model.Img.FileName;
            }

            musicApp.Add(track);
            await musicApp.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Search(string name)
        {
            var item = musicApp.Tracks;
            LoadArtists();
            LoadGenres();
            List<Track> tracks = new List<Track>();

            foreach (var i in item)
            {
                if (i.Name.Contains(name))
                {
                    tracks.Add(i);
                }
            }

            if (tracks.Count() == 0)
            {
                return NotFound();
            }
            return View(tracks);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddToFavorite(int musicId, string username)
        {
            if (!ModelState.IsValid)
            {
                LoadArtists();
                LoadGenres();

                return RedirectToAction("Index", "Home");
            }

            Favorite com = new Favorite()
            {
                UserName = username,
                TrackId = musicId
            };

            foreach (var item in musicApp.Favorites.Select(i => i).Where(i => i.UserName == username))
            {
                if (item.TrackId == musicId)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            musicApp.Favorites.Add(com);
            musicApp.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult DeleteFavorite(int id)
        {
            var item = musicApp.Favorites.Find(id);

            if (item == null)
            {
                return NotFound();
            }
            musicApp.Favorites.Remove(item);
            musicApp.SaveChanges();
            return RedirectToAction("Favorites");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Favorites()
        {
            var item = musicApp.Favorites.Where(i => i.UserName == User.Identity.Name);

            dynamic model = new ExpandoObject();

            model.Favorites = item.ToList();

            LoadArtists();
            LoadGenres();
            LoadTracks();

            if (item.Count() == 0)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
