using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NotesWebApp.Models;
using System;
using System.Linq;

namespace NotesWebApp.Controllers
{
    [Route("notes")]
    public class NotesController : Controller
    {
        /* dependency injection
         * ---------------------------------------------------------------------*/
        private readonly NotesDataContext _db; // instance of NotesDataContext class - dependency injection

        public NotesController(NotesDataContext db) // injecting an instance of NotesDataContext class in constructor of NotesController
        {
            _db = db;
        }

        /* get all notes
         * ---------------------------------------------------------------------*/
        [HttpGet("")]
        public IActionResult Index()
        {
            if (_db.Notices.Any<Note>())
            {
                Note[] notes = _db.Notices.ToArray();
                Array.Sort(notes, new NoteComparer());
                Array.Reverse(notes);
                return View(notes);
            }
            return View();
        }

        /* get one note by id
         * ---------------------------------------------------------------------*/
        [HttpGet("{id}")]
        public IActionResult Note(long id)
        {
            if (_db.Notices.Any(x => x.Id == id))
            {
                Note note = _db.Notices.FirstOrDefault(x => x.Id == id);
                return View(note);
            }
            return RedirectToAction("Index", "Notes");
        }

        /* create a note
         * ---------------------------------------------------------------------*/
        [HttpGet("create")] // display notes create view
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")] // post note-data from create-view as Note-object to CreateAction of NotesController
        public IActionResult Create(Note note)
        {
            if (!ModelState.IsValid) return View();
            note.Posted = DateTime.Now;

            _db.Notices.Add(note); // tell what it shall do
            _db.SaveChanges(); // execute it

            return RedirectToAction("Index", "Notes");
        }

        /* update a note
         * ---------------------------------------------------------------------*/
        [HttpGet("Update")] // opens update view and fills in data by id from query
        public IActionResult Update([FromQuery]long id)
        {
            Note note = _db.Notices.FirstOrDefault(x => x.Id == id);
            return View("Update", note);
        }

        [HttpPost("UpdateNote")] // update record in db with new data of form
        public ActionResult UpdateNote(long id, [FromForm] Note note)
        {
            if (ModelState.IsValid)
            {
                Note dbReturn = _db.Notices.FirstOrDefault(x => x.Id == id);
                dbReturn.Id = note.Id;
                dbReturn.Subject = note.Subject;
                dbReturn.Notice = note.Notice;
                dbReturn.Posted = DateTime.Now;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        /* delete a note
         * ---------------------------------------------------------------------*/
        [HttpPost] // Send id from view to controller-action
        public IActionResult Delete(long id)
        {
            var success = DeleteNote(id);
            return RedirectToAction("Index");
        }

        [HttpDelete("DeleteNote/{id}")] // delete record in sql db 
        public ActionResult<Note> DeleteNote(long id)
        {
            if (_db.Notices.Any(x => x.Id == id))
            {
                Note note = _db.Notices.FirstOrDefault(x => x.Id == id);
                EntityEntry<Note> dbNoteReturn = _db.Notices.Remove(note);
                _db.SaveChanges();
                return dbNoteReturn.Entity;
            }
            return View("Index");
        }
    }
}
