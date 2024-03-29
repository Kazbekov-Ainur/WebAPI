using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteContext noteContext;

        public NotesController(NoteContext noteContext)
        {
            this.noteContext = noteContext;
        }

        [HttpGet]
        [Route("GetNotes")]
        public List<Note> GetNotes()
        {
            return noteContext.Notes.ToList();
        }

        [HttpPost]
        [Route("AddNote")]
        public string AddNote(Note note)
        {
            string response = string.Empty;
            noteContext.Notes.Add(note);
            noteContext.SaveChanges();

            return "Note added";
        }

        [HttpPut]
        [Route("EditNote")]
        public string EditNote(Note note) 
        {
            noteContext.Entry(note).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            noteContext.SaveChanges();

            return "Note Edited";
        }

        [HttpDelete]
        [Route("DeleteNote")]
        public string DeleteNote(int id)
        {
            Note note = noteContext.Notes.Where(x => x.Id == id).FirstOrDefault();
            if(note != null)
            {
                noteContext.Notes.Remove(note);
                noteContext.SaveChanges();
                return "Note deleted";
            }
            else
            {
                return "Note Not Found";
            }

        }

    }
}
