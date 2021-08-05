using System;
using System.ComponentModel.DataAnnotations;

namespace NotesWebApp.Models
{
    public class Note
    {
        public long Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Notice { get; set; }
        public DateTime Posted { get; set; }
    }
}
