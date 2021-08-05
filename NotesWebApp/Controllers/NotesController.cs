using Microsoft.AspNetCore.Mvc;
using NotesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesWebApp.Controllers
{
    [Route("notes")]
    public class NotesController : Controller
    {
        // instance of NotesDataContext class - dependency injection
        private readonly NotesDataContext _db;

        // injecting an instance of NotesDataContext class in constructor of NotesController
        public NotesController(NotesDataContext db)
        {
            _db = db;
        }

        // get all notes
        [HttpGet("")]
        public IActionResult Index()
        {
            Note[] posts = _db.Notices.ToArray();
            return View(posts);
        }

        // display notes create view
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // post note-data from create-view as Note-object to CreateAction of NotesController
        [HttpPost("create")]
        public IActionResult Create(Note note)
        {
            if (!ModelState.IsValid) return View();
            note.Posted = DateTime.Now;

            _db.Notices.Add(note); // tell what it shall do
            _db.SaveChanges(); // execute it

            return RedirectToAction("Index", "Notes");
        }
    }
}
