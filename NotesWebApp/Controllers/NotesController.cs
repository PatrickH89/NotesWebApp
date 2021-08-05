using Microsoft.AspNetCore.Mvc;
using NotesWebApp.Models;
using System;
using System.Collections.Generic;

namespace NotesWebApp.Controllers
{
    [Route("notes")]
    public class NotesController : Controller
    {
        // get all notes
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Note> posts = new List<Note>();
            for (int i = 0; i < 5; i++)
            {
                Note post = new Note();
                post.Id = i;
                post.Subject = "Note " + i;
                post.Notice = "Hello " + i;
                post.Posted = new DateTime(1111, 1, 1, 1, 1, i);
                posts.Add(post);
            }
            return View(posts);
        }
    }
}
