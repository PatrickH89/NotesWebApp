using System.Collections;

namespace NotesWebApp.Models
{
    public class NoteComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(((Note)x).Posted, ((Note)y).Posted);
        }
    }
}
