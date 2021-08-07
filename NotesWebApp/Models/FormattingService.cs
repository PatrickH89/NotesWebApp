using System;

namespace NotesWebApp.Models
{
    public class FormattingService
    {
        public string AsReadableDate(DateTime date)
        {
            return date.ToString("U");
        }
    }
}
