using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        
        //TEST>>

        //return partial view
        //ok
        public ActionResult KendoGrid()
        {
            return View();
        }

        //read
        public ActionResult Read()
        {
            return Json(GetAlbums(), JsonRequestBehavior.AllowGet);
        }


        public IEnumerable<MvcMusicStore.Models.Album> GetAlbums()
        {
            var albums = db.Albums.Select(a => new Album
            {
                AlbumId = a.AlbumId,
                Artist = a.Artist,
                Genre = a.Genre,
                Title = a.Title,
                Price = a.Price,
                //AlbumArtUrl = a.AlbumArtUrl

        }).ToList();

        return (albums);

        }
            
    
        //<<TEST








        //
        // GET: /StoreManager/

        public ViewResult Index()
        {
            var albums = db.Albums.Include(a => a.Genre).Include(a => a.Artist);
            return View(albums.ToList());
        }       


        //
        // GET: /StoreManager/Details/5

        public ViewResult Details(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            return View();
        } 

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Album album = db.Albums.Find(id);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // GET: /StoreManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}